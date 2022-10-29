using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;


namespace Invary.IvyPhotoshopDiffusion
{
	public class RecentItem
	{
		public string Raw
		{
			get => _raw;

			set
			{
				_raw = value;
				_raw = _raw.Replace("\r", "");
				_raw = _raw.Replace("\n", "\r\n");
			}
		}
		string _raw { set; get; } = "";

		public string Display { get; set; } = "";



		public override string ToString()
		{
			return Display;
		}

		public void Set(string text)
		{
			Raw = text;

			Display = text.Replace("\n", " ");
			Display = Display.Replace("\r", " ");
			if (Display.Length > 30)
				Display = Display.Substring(0, 30);
		}

		public RecentItem()
		{
		}

		public RecentItem(string text)
		{
			Set(text);
		}
	}



	[Serializable]
	public class XmlRecent
	{
		[XmlIgnore]
		public static XmlRecent Current { get; set; } = new XmlRecent();




		public List<RecentItem> ListPrompt { get; set; } = new List<RecentItem>();
		public List<RecentItem> ListNegativePrompt { get; set; } = new List<RecentItem>();



		/// <summary>
		/// Add recently prompt
		/// 
		/// with file save
		/// </summary>
		/// <returns>true = history is modified</returns>
		public bool Add(string prompt, string negativePrompt)
		{
			int find;
			bool dirty = false;


			// add to list, if not exists
			find = ListPrompt.FindIndex(a => a.Raw.Replace("\r", "").Replace("\n", "") == prompt.Replace("\r", "").Replace("\n", ""));
			if (find == 0)
			{
				//find at head. do nothing
			}
			else if (find > 0)
			{
				ListPrompt.RemoveAt(find);
				ListPrompt.Insert(0, new RecentItem(prompt));
				dirty = true;
			}
			else
			{
				ListPrompt.Insert(0, new RecentItem(prompt));
				dirty = true;
			}
			if (ListPrompt.Count > XmlSetting.Current.RecentMaxCount)
			{
				ListPrompt.RemoveRange(XmlSetting.Current.RecentMaxCount, ListPrompt.Count - XmlSetting.Current.RecentMaxCount);
				dirty = true;
			}


			// add to list, if not exists
			find = ListNegativePrompt.FindIndex(a => a.Raw.Replace("\r", "").Replace("\n", "") == negativePrompt.Replace("\r", "").Replace("\n", ""));
			if (find == 0)
			{
				//find at head. do nothing
			}
			else if (find > 0)
			{
				ListNegativePrompt.RemoveAt(find);
				ListNegativePrompt.Insert(0, new RecentItem(negativePrompt));
				dirty = true;
			}
			else
			{
				ListNegativePrompt.Insert(0, new RecentItem(negativePrompt));
				dirty = true;
			}
			if (ListNegativePrompt.Count > XmlSetting.Current.RecentMaxCount)
			{
				ListNegativePrompt.RemoveRange(XmlSetting.Current.RecentMaxCount, ListNegativePrompt.Count - XmlSetting.Current.RecentMaxCount);
				dirty = true;
			}

			if (dirty)
				Save();

			return dirty;
		}















		[XmlIgnore]
		public static string ExeFolder
		{
			get
			{
				var exe = Application.ExecutablePath;
				return Path.GetDirectoryName(exe);
			}
		}


		[XmlIgnore]
		public static string SaveFilePath
		{
			get
			{
				return Path.Combine(ExeFolder, "recent.xml");
			}
		}




		public XmlRecent Clone()
		{
			var serializer = new XmlSerializer(typeof(XmlRecent));
			using (var ms = new MemoryStream())
			{
				serializer.Serialize(ms, this);

				ms.Seek(0, SeekOrigin.Begin);
				var load = (XmlRecent)serializer.Deserialize(ms);
				if (load == null)
					throw new Exception();

				return load;
			}
		}

		public bool Save()
		{
			string file = SaveFilePath;

			try
			{
				XmlSerializer serializer = new XmlSerializer(typeof(XmlRecent));
				using (FileStream fs = new FileStream(file, FileMode.Create))
				{
					serializer.Serialize(fs, this);
					fs.Close();
				}
			}
			catch (Exception)
			{
				return false;
			}

			return true;
		}




		public static bool Load()
		{
			string file = SaveFilePath;

			if (File.Exists(file) == false)
				return Current.Save();

			try
			{
				XmlSerializer serializer = new XmlSerializer(typeof(XmlRecent));

				using (FileStream fs = new FileStream(file, FileMode.Open))
				{
					var load = (XmlRecent)serializer.Deserialize(fs);
					fs.Close();

					if (load == null)
						return false;

					Current = load;
				}

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}