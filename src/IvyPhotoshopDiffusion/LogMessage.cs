using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Invary.IvyPhotoshopDiffusion
{

	static class LogMessage
	{
		static List<string> ListLog { set; get; } = new List<string>();

		public static void WriteLine(string text)
		{
			try
			{
				if (FormMain.MainWnd == null || FormMain.MainWnd.IsDisposed)
					return;

				text = text + "\n";
				text = text.Replace("\n", "\r\n");
				ListLog.Insert(0, text);

				if (ListLog.Count > 50)
					ListLog.RemoveAt(ListLog.Count - 1);


				if (XmlSetting.Current.IsSaveLogFile)
				{
					using (FileStream fs = new FileStream(XmlSetting.LogFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
					using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
					{
						sw.BaseStream.Seek(0, SeekOrigin.End);
						sw.Write(text);
					}
				}


				string message = "";
				foreach(var item in ListLog)
				{
					message += item;
				}

				try
				{
					if (FormMain.MainWnd.Handle == null)
						FormMain.MainWnd.textBoxLogMessage.Text = message;
				}
				catch (Exception)
				{
					FormMain.MainWnd.Invoke((MethodInvoker)delegate
					{
						FormMain.MainWnd.textBoxLogMessage.Text = message;
					});
				}
			}
			catch (Exception)
			{
			}
		}
	}
}
