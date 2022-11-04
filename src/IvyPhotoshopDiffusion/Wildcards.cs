using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Invary.IvyPhotoshopDiffusion
{
	internal class Wildcards
	{
		//
		// wildcards prompt
		//
		// Replace '__word__' in the prompt with the line in the 'wildcards/word.txt'

		// in current version, '__word__' and '____word________' means same

		// in 'wildcards/word.txt'
		//		lines starting with '#' are treated as comments and ignore
		//		empty lines will be ignored

		//TODO: not support wildcard in path. ex. ___color*___, ___color?___
		//TODO: not support directory. ex. __season/spring__, __season\spring__


		//
		// dynamic prompts
		//
		// https://github.com/adieyal/sd-dynamic-prompts
		//
		// Replace '{black|white|red|green}' with one of black/white/red/green.
		// Replace '{2$$black|white|red|green}' with two.
		// Replace '{1-2$$black|white|red|green}' with one or two.



		public static string Convert(string prompt)
		{
			if (XmlSetting.Current.IsEnableWildcards)
				prompt = ConvertWildCards(prompt);
			if (XmlSetting.Current.IsEnableDynamicPrompts)
				prompt = ConvertDynamicPrompt(prompt);

			return prompt;
		}




		static string ConvertWildCards(string prompt)
		{
			try
			{
				List<string> words = new List<string>();

				// get all '___word___' from prompt, and set to list
				{
					var collection = Regex.Matches(prompt, @"(_+?[^_\s,\{\}\[\]\|\t\n\r]+?_+)");

					foreach (Match match in collection)
					{
						// '_' is more than two
						var word = match.Groups[0].Value;
						if (word.StartsWith("__") == false || word.EndsWith("__") == false)
							continue;

						words.Add(word);
					}
				}


				Random rnd = new Random();

				foreach (var item in words)
				{
					List<string> replaceto = new List<string>();

					NameToWordsList(item, replaceto);
					if (replaceto.Count == 0)
						continue;

					while (true)
					{
						int find = prompt.IndexOf(item);
						if (find < 0)
							break;

						int index = rnd.Next(0, replaceto.Count);
						var word = replaceto[index];

						//recursive wildcards
						//call Convert(), not ConvertWildCards()
						word = Convert(word);

						prompt = prompt.Substring(0, find) + word + prompt.Substring(find + item.Length, prompt.Length - (find + item.Length));
					}
				}
			}
			catch(Exception ex)
			{
				LogMessage.WriteLine(ex.Message);
				LogMessage.WriteLine($"error: wildcards  failed");
			}

			return prompt;
		}









		static string ConvertDynamicPrompt(string prompt)
		{
			try
			{
				List<string> words = new List<string>();

				// get all '{2$$aaa|bbb|ccc}' from prompt, and set to list
				{
					var collection = Regex.Matches(prompt, @"(\{[^\{\}\n\r]+?\})");

					foreach (Match match in collection)
					{
						var word = match.Groups[0].Value;
						words.Add(word);
					}
				}


				Random rnd = new Random();

				foreach (var item in words)
				{
					List<string> replaceto = new List<string>();

					int pickCountMin;
					int pickCountMax;

					NameToWordsList(item, replaceto, out pickCountMin, out pickCountMax);
					if (replaceto.Count == 0)
						continue;

					while (true)
					{
						int find = prompt.IndexOf(item);
						if (find < 0)
							break;

						int pickCount = rnd.Next(pickCountMin, pickCountMax + 1);
						string word = "";

						if (pickCount >= replaceto.Count)
						{
							//word list count is too small to replace, so all words use
							for (int i = 0; i < replaceto.Count; i++)
							{
								if (word != "")
									word += ", ";
								word += replaceto[i];
							}
						}
						else
						{
							List<int> used = new List<int>();

							while (used.Count != pickCount)
							{
								int index = rnd.Next(0, replaceto.Count);
								if (used.Contains(index))
									continue;

								if (word != "")
									word += ", ";
								word += replaceto[index];
								used.Add(index);
							}
						}

						//recursive wildcards
						//call Convert(), not ConvertDynamicPrompt()
						word = Convert(word);

						prompt = prompt.Substring(0, find) + word + prompt.Substring(find + item.Length, prompt.Length - (find + item.Length));
					}
				}
			}
			catch (Exception ex)
			{
				LogMessage.WriteLine(ex.Message);
				LogMessage.WriteLine($"error: wildcards(dynamic) failed");
			}

			return prompt;
		}







		/// <param name="name">{aaa|bbb|ccc}, {2$$aaa|bbb|ccc}, {0-2$$aaa|bbb|ccc}</param>
		static bool NameToWordsList(string name, List<string> words, out int pickCountMin, out int pickCountMax)
		{
			pickCountMin = 1;
			pickCountMax = 1;
			words.Clear();
			var org = name;

			try
			{
				name = name.TrimStart('{');
				name = name.TrimEnd('}');

				var data = name.Split('|');
				if (data.Length == 0)
					return true;

				{
					var match = Regex.Match(data[0], @"([0-9]+?)-*([0-9]*?)\$\$(.+?)$");

					if (match.Success)
					{
						var min = match.Groups[1].Value;
						var max = match.Groups[2].Value;
						try
						{
							pickCountMin = int.Parse(min);
							pickCountMax = pickCountMin;
							if (string.IsNullOrEmpty(max) == false)
								pickCountMax = int.Parse(max);
						}
						catch (Exception)
						{
							pickCountMin = 1;
							pickCountMax = 1;
						}
						if (pickCountMin > pickCountMax)
						{
							//swap
							var tmp = pickCountMin;
							pickCountMin = pickCountMax;
							pickCountMax = tmp;
						}

						var word = match.Groups[3].Value;
						words.Add(word);
					}
					else
						words.Add(data[0]);
				}

				for (int i = 1; i < data.Length; i++)
				{
					words.Add(data[i]);
				}

				return true;
			}
			catch (Exception ex)
			{
				pickCountMin = 0;
				pickCountMax = 0;
				words.Clear();
				LogMessage.WriteLine(ex.Message);
				LogMessage.WriteLine($"error: wildcards for '{org}' failed");
				return false;
			}
		}




		/// <param name="name">__name__, ____name____</param>
		static bool NameToWordsList(string name, List<string> words)
		{
			words.Clear();

			var file = NameToFilePath(name);
			if (string.IsNullOrEmpty(file))
				return false;

			try
			{
				using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				using (StreamReader sr = new StreamReader(fs))
				{
					while (sr.EndOfStream == false)
					{
						var line = sr.ReadLine();
						if (string.IsNullOrEmpty(line))
							continue;

						// '#' start line is comment
						if (line.StartsWith("#"))
							continue;

						words.Add(line);
					}
				}

				return true;
			}
			catch(Exception ex)
			{
				LogMessage.WriteLine(ex.Message);
				LogMessage.WriteLine($"error: wildcards for '{name}' failed");
				return false;
			}
		}



		/// <param name="name">__name__, ____name____</param>
		static string NameToFilePath(string name)
		{
			name = name.Trim(' ');
			name = name.Trim('_');
			name += ".txt";
			name = Path.Combine(Path.Combine(XmlSetting.ExeFolder, "wildcards"), name);

			if (File.Exists(name))
				return name;

			return "";
		}


	}
}
