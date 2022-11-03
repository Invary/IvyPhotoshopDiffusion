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
		// wildcards prompt
		//
		// Replace '__word__' in the prompt with the line in the 'wildcards/word.txt'

		// in current version, '__word__' and '____word________' means same

		// in 'wildcards/word.txt'
		//		lines starting with '#' are treated as comments and ignore
		//		empty lines will be ignored

		//TODO: not support wildcard in path. ex. ___color*___, ___color?___
		//TODO: not support directory. ex. __season/spring__, __season\spring__


		public static string Convert(string prompt)
		{
			try
			{
				// get all '___word___' from prompt, and set to list
				List<string> words = new List<string>();
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

					ReadFile(item, replaceto);
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






		/// <param name="name">__name__, ____name____</param>
		static bool ReadFile(string name, List<string> words)
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
