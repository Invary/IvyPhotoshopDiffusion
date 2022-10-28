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


	public class ArgInfo
	{
		public string strName { set; get; } = "";
		public string strArg { set; get; } = "";


		public ArgInfo Clone()
		{
			var copy = new ArgInfo();
			copy.strName = strName;
			copy.strArg = strArg;
			return copy;
		}


		public override string ToString()
		{
			return strName;
		}
	}







	public class Setting
	{

		[XmlIgnore]
		public static EventHandler OnSettingChange { set; get; } = null;


		[XmlIgnore]
		public static Setting Current { set; get; }

		[XmlIgnore]
		public static int nVersion { get; } = 100;

		[XmlIgnore]
		public static string strVersion { get; } = $"Ver{nVersion}";

		[XmlIgnore]
		public static string strProductGuid { get; } = "{328C07E9-4003-4E0A-8C8E-3481DF1416B4}";

		[XmlIgnore]
		public static string strUpdateCheckUrl { get; } = @"https://raw.githubusercontent.com/Invary/Status/main/status.json";


		[XmlIgnore]
		public static string strDownloadUrl { get; } = @"https://github.com/Invary/IvyMediaDownloader/Releases";


		[XmlIgnore]
		public static string strExeFolder
		{
			get
			{
				var exe = Application.ExecutablePath;
				return Path.GetDirectoryName(exe);
			}
		}


		[XmlIgnore]
		public static string strSettingPath
		{
			get
			{
				if (string.IsNullOrEmpty(_strSettingFolder) == false)
				{
					return Path.Combine(_strSettingFolder, "setting.xml");
				}

				return Path.Combine(strExeFolder, "setting.xml");
			}
		}
		static string _strSettingFolder = "";

		public static void SetSettingFileFolder(string folder)
		{
			if (string.IsNullOrEmpty(folder))
				return;

			_strSettingFolder = folder;
		}









		/// <summary>
		/// If you want to create new setting instance,
		/// must use Setting.CreateNewSetting()
		/// </summary>
		public Setting()
		{
		}



		public static Setting CreateNewSetting()
		{
			var setting = new Setting();

			setting.listYtDlpArg.Add(new ArgInfo() { strName = "Default", strArg = "-f \"best[ext=mp4]\" %URL%" });
			setting.listYtDlpArg.Add(new ArgInfo() { strName = "mp3", strArg = "-f 'ba' -x --audio-format mp3 %URL%" });
			setting.strYtDlpWorkPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

			return setting;
		}


		public static void FireSettingChange()
		{
			OnSettingChange?.Invoke(null, new EventArgs());
		}




		public string GetYtDlpExePath()
		{
			if (string.IsNullOrEmpty(strYtDlpExePath) == false)
				return strYtDlpExePath;

			return Path.Combine(strExeFolder, @"yt-dlp.exe");
		}

		public string GetDBPath()
		{
			if (string.IsNullOrEmpty(strDBPath) == false)
				return strDBPath;

			return Path.Combine(strExeFolder, "userdata.ldb");
		}


		public static string GetArgsName(int nIndex)
		{
			if (nIndex >= Setting.Current.listYtDlpArg.Count)
				nIndex = 0;

			return Setting.Current.listYtDlpArg[nIndex].strName;
		}







		public string strYtDlpExePath { set; get; } = "";
		public string strYtDlpWorkPath { set; get; }

		public string strDBPath { set; get; } = "";


		public List<ArgInfo> listYtDlpArg { set; get; } = new List<ArgInfo>();

		public int nMaxDownloadCount { set; get; } = 2;


		public string strLanguage { set; get; } = "";



		//Color cannot serialize
		[XmlIgnore]
		public Color colorSubscribeBackGround { set; get; } = Color.PaleTurquoise;

		[Browsable(false)]
		[XmlElement("colorSubscribeBackGround")]
		public string _colorSubscribeBackGround
		{
			get { return ColorTranslator.ToHtml(colorSubscribeBackGround); }
			set { colorSubscribeBackGround = ColorTranslator.FromHtml(value); }
		}


		//Color cannot serialize
		[XmlIgnore]
		public Color colorDefaultBackGround { set; get; } = Color.White;

		[Browsable(false)]
		[XmlElement("colorDefaultBackGround")]
		public string _colorDefaultBackGround
		{
			get { return ColorTranslator.ToHtml(colorDefaultBackGround); }
			set { colorDefaultBackGround = ColorTranslator.FromHtml(value); }
		}


		public string strUrlDetectRegExp { set; get; } = "(https?:\\/\\/(?:www\\.youtube\\.com|youtu\\.be)\\/[\\/0-9A-Za-z\\?=\\-_&]+?)[\\r\\n\\s$\"'\\t<>]";


		public bool bUseClipboarAutoImport { set; get; } = false;


		public bool bUpdateCheck { set; get; } = true;
		public DateTime dtLastUpdateCheck { set; get; } = DateTime.MinValue;


		public bool bUpdateCheckYtdlp { set; get; } = true;
		public DateTime dtLastUpdateCheckYtdlp { set; get; } = DateTime.MinValue;





		public bool Save()
		{
			try
			{
				XmlSerializer serializer = new XmlSerializer(typeof(Setting));
				using (FileStream fs = new FileStream(strSettingPath, FileMode.Create))
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



		public static Setting Load()
		{
			try
			{
				XmlSerializer serializer = new XmlSerializer(typeof(Setting));

				using (FileStream fs = new FileStream(strSettingPath, FileMode.Open, FileAccess.Read))
				{
					var load = (Setting)serializer.Deserialize(fs);
					fs.Close();
					return load;
				}
			}
			catch (Exception)
			{
				return null;
			}
		}
	}



}
