using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Invary.Utility
{
	internal class ClipboardUty
	{


		static void GetTextThread(object param)
		{
			List<string> list = (List<string>)param;
			if (list == null)
				return;

			list.Clear();
			list.Add(Clipboard.GetText());

			//var tt = Data.GetFormats();
			//if (tt != null && tt.Contains("HTML Format"))
			//	list.Add(Data.GetData("HTML Format"));
		}



		public static List<string> GetText()
		{
			List<string> listClipText = new List<string>();

			//Clipboard access may fail if not STA thread.
			Thread thread = new Thread(GetTextThread);
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start(listClipText);
			thread.Join();

			return listClipText;
		}




		static void SetTextThread(object param)
		{
			string text = (string)param;
			if (text == null)
				return;
			Clipboard.SetText(text);
		}


		public static void SetText(string text)
		{
			//Clipboard access may fail if not STA thread.
			Thread thread = new Thread(SetTextThread);
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start(text);
			thread.Join();
		}


	}
}
