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
	[Serializable]
	public class XmlSetting
	{
		[XmlIgnore]
		public static XmlSetting Current { get; set; } = new XmlSetting();


		[XmlIgnore]
		public static int nVersion { get; } = 106;

		[XmlIgnore]
		public static string strVersion { get; } = $"Ver{nVersion}";


		[XmlIgnore]
		public static string ProductGuid { get; } = "{D9B3019D-287B-48A3-87F0-A5AB8B9258B9}";

		[XmlIgnore]
		public static string UpdateCheckUrl { get; } = @"https://raw.githubusercontent.com/Invary/Status/main/status.json";


		[XmlIgnore]
		public static string DownloadUrl { get; } = @"https://github.com/Invary/IvyPhotoshopDiffusion/Releases";


		[XmlIgnore]
		public static DateTime StartUpDate { set; get; } = DateTime.MinValue;



		public bool IsCheckAutoUpdate { set; get; } = true;
		public DateTime LastCheckAutoUpdateDate { set; get; } = DateTime.MinValue;





		public bool IsSaveLogFile { set; get; } = true;




		public string LastPrompt { set; get; } = "";
		public string LastNegativePrompt { set; get; } = "lowres, bad anatomy, bad hands, text, error, missing fingers, extra digit, fewer digits, cropped, worst quality, low quality, normal quality, jpeg artifacts, signature, watermark, username, blurry, \n";

		public string LastLayerName { set; get; } = "@date, seed=@seed, strength=@strength, cfg=@cfg, steps=@steps, @sampler, @prompt";

		public int LastClipSkip { set; get; } = 1;
		public int LastENSD { set; get; } = 0;



		/// <summary>
		/// Automatic1111 API URL
		/// 
		/// end character is not '/'
		/// </summary>
		public string Automatic1111ApiUrl { set; get; } = "http://127.0.0.1:7860";


		public List<string> ListSampler { set; get; } = new List<string>();





		//Color cannot serialize
		[XmlIgnore]
		public Color TransparentColor { set; get; } = Color.White;

		[Browsable(false)]
		[XmlElement("colorTransparent")]
		public string TransparentColor_
		{
			get { return ColorTranslator.ToHtml(TransparentColor); }
			set { TransparentColor = ColorTranslator.FromHtml(value); }
		}



		public int RecentMaxCount { set; get; } = 20;




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
				return Path.Combine(ExeFolder, "setting.xml");
			}
		}


		[XmlIgnore]
		public static string TempFolder
		{
			get
			{
				var folder = Path.Combine(ExeFolder, "temp");
				if (Directory.Exists(folder) == false)
				{
					Directory.CreateDirectory(folder);
				}
				return folder;
			}
		}


		[XmlIgnore]
		public static string LogFilePath
		{
			get
			{
				var folder = Path.Combine(ExeFolder, "log");
				if (Directory.Exists(folder) == false)
				{
					Directory.CreateDirectory(folder);
				}
				return Path.Combine(folder, $"{StartUpDate.ToString("yyyyMMdd_HHmmss")}.txt");
			}
		}







		public XmlSetting Clone()
		{
			var serializer = new XmlSerializer(typeof(XmlSetting));
			using (var ms = new MemoryStream())
			{
				serializer.Serialize(ms, this);

				ms.Seek(0, SeekOrigin.Begin);
				var load = (XmlSetting)serializer.Deserialize(ms);
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
				XmlSerializer serializer = new XmlSerializer(typeof(XmlSetting));
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
				XmlSerializer serializer = new XmlSerializer(typeof(XmlSetting));

				using (FileStream fs = new FileStream(file, FileMode.Open))
				{
					
					var load = (XmlSetting)serializer.Deserialize(fs);
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