using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Invary.IvyPhotoshopDiffusion
{
	internal class NovelAI
	{
		// /////////////////
		//
		// NovelAI
		//

		// Strengthening & Weakening Vectors
		// https://docs.novelai.net/image/strengthening-weakening.html
		//
		//	{ }		= x 1.05
		//	[ ]		= / 1.05
		//
		//	{{ }}	= x 1.1025
		//	[[ ]]	= / 1.1025
		//

		//Prompt Mixing
		//https://docs.novelai.net/image/promptmixing.html
		//
		// cat | dog		= mix cat & dog
		// cat | dog:0.2	= mix cat & 0.2 dog
		// cat | dog:-1.0	= mix cat & negative dog
		// cat:0.5 | dog:0.3= mix 0.5 cat & 0.3 dog
		//



		// /////////////////////
		//
		// Automatic1111
		//

		// Attention/emphasis
		// https://github.com/AUTOMATIC1111/stable-diffusion-webui/wiki/Features#attentionemphasis
		//
		// ( )		= x 1.10
		// [ ]		= / 1.10
		//
		// NAI {xxx}	= Automatic1111 (xxx:1.05)
		// NAI {{xxx}}	= Automatic1111 (xxx:1.1025)
		//
		// NAI [xxx]	= Automatic1111 (xxx:0.952)		= 1/1.05
		// NAI [[xxx]]	= Automatic1111 (xxx:0.907)		= 1/1.05/1.05
		//

		// Prompt editing
		//https://github.com/AUTOMATIC1111/stable-diffusion-webui/wiki/Features#prompt-editing
		//
		// [from:to:when]
		// [to:when]

		// Alternating Words
		//https://github.com/AUTOMATIC1111/stable-diffusion-webui/wiki/Features#alternating-words
		//
		// [cow|horse]
		// [cow|cow|horse|man|siberian tiger|ox|man]

		// Escape
		// \( \)




		public static string ConvertNAIto1111(string prompt)
		{
			//prompt = "{{}}, {xxxx}, {{yyy}}, [aaa], [[bbb]]";
			//prompt = "[{xx{{}}}, [{xxxx}, {{yyy}},] [aaa], [[bbb]]]";

			// '()' is not attention/emphasis, so espace
			prompt = prompt.Replace("(", "\\(");
			prompt = prompt.Replace(")", "\\)");

			List<string> list = new List<string>();

			//to list
			for (int i = 0; i < prompt.Length; i++)
			{
				if (prompt[i] == '{')
				{
					if (list.Count > 0 && list[list.Count - 1].Length > 0 && list[list.Count - 1][0] == '{')
						list[list.Count - 1] += '{';
					else
						list.Add("{");
					continue;
				}
				if (prompt[i] == '}')
				{
					if (list.Count > 0 && list[list.Count - 1].Length > 0 && list[list.Count - 1][0] == '}')
						list[list.Count - 1] += '}';
					else
						list.Add("}");
					continue;
				}
				if (prompt[i] == '[')
				{
					if (list.Count > 0 && list[list.Count - 1].Length > 0 && list[list.Count - 1][0] == '[')
						list[list.Count - 1] += '[';
					else
						list.Add("[");
					continue;
				}
				if (prompt[i] == ']')
				{
					if (list.Count > 0 && list[list.Count - 1].Length > 0 && list[list.Count - 1][0] == ']')
						list[list.Count - 1] += ']';
					else
						list.Add("]");
					continue;
				}

				for (int j = i; j <= prompt.Length; j++)
				{
					if (j == prompt.Length)
					{
						list.Add(prompt.Substring(i, j - i));
						i = j;
						break;
					}
					if (prompt[j] != '{' && prompt[j] != '}' && prompt[j] != '[' && prompt[j] != ']')
						continue;

					list.Add(prompt.Substring(i, j - i));
					i = j - 1;
					break;
				}
			}

			//remove empty elements from list
			for (int j = list.Count - 1; j >= 0; j--)
			{
				if (list[j].Length > 0)
					continue;

				list.RemoveAt(j);
			}


			Stack<string> stack = new Stack<string>();

			//to stack and convert
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i][0] == '}')
				{
					Stack<string> stack2 = new Stack<string>();
					while(stack.Count > 0)
					{
						string start = stack.Pop();
						if (start[0] == '{')
						{
							int count = 0;
							if (start.Length == list[i].Length)
							{
								//same count
								//ex. {{xxx}}
								count = start.Length;
								start = "";
								list[i] = "";
							}
							else if (start.Length < list[i].Length)
							{
								//end is larger than start
								//ex. {xxx}} 
								count = start.Length;
								list[i] = list[i].Substring(0, list[i].Length - start.Length);
								start = "";
							}
							else //if (str.Length > list[i].Length)
							{
								//start is larger than end
								//ex. {{xxx}
								count = list[i].Length;
								start = start.Substring(0, start.Length - list[i].Length);
								list[i] = "";
							}

							string text = "(";
							while (stack2.Count > 0)
							{
								text += stack2.Pop();
							}
							text += $":{Math.Pow(1.05, count):0.####})";

							if (start.Length > 0)
								stack.Push(start);
							stack.Push(text);
							//stack.Push(list[i]);
							break;
						}
						stack2.Push(start);
					}

					while (stack2.Count > 0)
					{
						stack.Push(stack2.Pop());
					}

					if (list[i].Length > 0)
					{
						//loop again
						i--;
						continue;
					}
				}


				if (list[i].Length > 0 && list[i][0] == ']')
				{
					Stack<string> stack2 = new Stack<string>();
					while (stack.Count > 0)
					{
						string start = stack.Pop();
						if (start[0] == '[')
						{
							int count = 0;
							if (start.Length == list[i].Length)
							{
								//same count
								//ex. [[xxx]]
								count = start.Length;
								start = "";
								list[i] = "";
							}
							else if (start.Length < list[i].Length)
							{
								//end is larger than start
								//ex. [xxx]]
								count = start.Length;
								list[i] = list[i].Substring(0, list[i].Length - start.Length);
								start = "";
							}
							else //if (str.Length > list[i].Length)
							{
								//start is larger than end
								//ex. [[xxx]
								count = list[i].Length;
								start = start.Substring(0, start.Length - list[i].Length);
								list[i] = "";
							}

							string text = "(";
							while (stack2.Count > 0)
							{
								text += stack2.Pop();
							}
							text += $":{Math.Pow(1.0/1.05, count):0.####})";

							if (start.Length > 0)
								stack.Push(start);
							stack.Push(text);
							//stack.Push(list[i]);
							break;
						}
						stack2.Push(start);
					}

					while (stack2.Count > 0)
					{
						stack.Push(stack2.Pop());
					}

					if (list[i].Length > 0)
					{
						//loop again
						i--;
						continue;
					}
				}




				if (list[i].Length > 0)
					stack.Push(list[i]);
			}

			{
				//reverse
				//CBA->ABC
				Stack<string> stack3 = new Stack<string>();
				while (stack.Count > 0)
				{
					stack3.Push(stack.Pop());
				}

				//stack to string
				prompt = "";
				while (stack3.Count > 0)
				{
					prompt += stack3.Pop();
				}
			}


			return prompt;
		}

	}
}
