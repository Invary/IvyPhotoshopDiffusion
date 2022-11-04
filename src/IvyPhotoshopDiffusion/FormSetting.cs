using Invary.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Invary.IvyPhotoshopDiffusion
{
	public partial class FormSetting : Form
	{
		public FormSetting()
		{
			InitializeComponent();

			textBoxAutomatic1111Url.Text = XmlSetting.Current.Automatic1111ApiUrl;

			checkBoxCheckUpdate.Checked = XmlSetting.Current.IsCheckAutoUpdate;

			checkBoxSaveLogFile.Checked = XmlSetting.Current.IsSaveLogFile;

			checkBoxEnableWildcards.Checked = XmlSetting.Current.IsEnableWildcards;
			checkBoxEnableDynamicPrompts.Checked = XmlSetting.Current.IsEnableDynamicPrompts;



			labelVersion.Text = $"IvyPhotoshopDiffusion {XmlSetting.strVersion}";

			linkLabelProjectURL.Click += delegate
			{
				Uty.OpenURL("https://github.com/Invary/IvyPhotoshopDiffusion");
			};

			pictureBoxDonationKofi.Click += delegate
			{
				Uty.OpenURL("https://ko-fi.com/E1E7AC6QH");
			};

			linkLabelDonationQR1.Click += delegate
			{
				Clipboard.SetText("0xCbd4355d13CEA25D87F324E9f35A075adce6507c");
			};
			pictureBoxDonationQR1.Click += delegate
			{
				Clipboard.SetText("0xCbd4355d13CEA25D87F324E9f35A075adce6507c");
			};

			linkLabelDonationQR2.Click += delegate
			{
				Clipboard.SetText("1FvzxYriyNDdeA12eaUGXTGSJxkzpQdxPd");
			};
			pictureBoxDonationQR2.Click += delegate
			{
				Clipboard.SetText("1FvzxYriyNDdeA12eaUGXTGSJxkzpQdxPd");
			};
		}






		private void buttonOK_Click(object sender, EventArgs e)
		{
			// URL is not end with '/'
			XmlSetting.Current.Automatic1111ApiUrl = textBoxAutomatic1111Url.Text;
			while (XmlSetting.Current.Automatic1111ApiUrl.EndsWith("/"))
			{
				// remove '/'
				XmlSetting.Current.Automatic1111ApiUrl = XmlSetting.Current.Automatic1111ApiUrl.Substring(0, XmlSetting.Current.Automatic1111ApiUrl.Length - 1);
			}

			XmlSetting.Current.IsCheckAutoUpdate = checkBoxCheckUpdate.Checked;
			XmlSetting.Current.IsSaveLogFile = checkBoxSaveLogFile.Checked;

			XmlSetting.Current.IsEnableWildcards = checkBoxEnableWildcards.Checked;
			XmlSetting.Current.IsEnableDynamicPrompts = checkBoxEnableDynamicPrompts.Checked;



			XmlSetting.Current.Save();
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

	}
}
