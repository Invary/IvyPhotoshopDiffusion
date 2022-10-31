using Invary.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;






namespace Invary.IvyPhotoshopDiffusion
{
	// Automatic1111 need command line option '--api', otherwise 404 error


	//TODO: custom prompt selection, combobox
	//TODO: save last setting
	//TODO: dupe exec check?





	public partial class FormMain : Form
	{
		public static FormMain MainWnd { set; get; }







		public FormMain()
		{
			MainWnd = this;

			InitializeComponent();

			XmlSetting.Load();
			XmlRecent.Load();

			Text = $"IvyPhotoshopDiffusion {XmlSetting.strVersion}";
			LogMessage.WriteLine($"IvyPhotoshopDiffusion {XmlSetting.strVersion}");



			textBoxPrompt.Text = XmlSetting.Current.LastPrompt;
			textBoxNegativePrompt.Text = XmlSetting.Current.LastNegativePrompt;
			textBoxLayerName.Text = XmlSetting.Current.LastLayerName;


			labelNoiseScale100.Text = $"{(double)trackBarNoiseScale100.Value / 100:0.##}";
			trackBarNoiseScale100.ValueChanged += delegate
			{
				labelNoiseScale100.Text = $"{(double)trackBarNoiseScale100.Value / 100:0.##}";
			};

			labelMaskBlur.Text = $"{trackBarMaskBlur.Value}";
			trackBarMaskBlur.ValueChanged += delegate
			{
				labelMaskBlur.Text = $"{trackBarMaskBlur.Value}";
			};

			labelCfgScale100.Text = $"{(double)trackBarCfgScale100.Value / 100:0.##}";
			trackBarCfgScale100.ValueChanged += delegate
			{
				labelCfgScale100.Text = $"{(double)trackBarCfgScale100.Value / 100:0.##}";
			};

			labelStep.Text = $"{trackBarStep.Value}";
			trackBarStep.ValueChanged += delegate
			{
				labelStep.Text = $"{trackBarStep.Value}";
			};

			labelBatchCount.Text = $"{trackBarBatchCount.Value}";
			trackBarBatchCount.ValueChanged += delegate
			{
				labelBatchCount.Text = $"{trackBarBatchCount.Value}";
			};

			labelBatchSize.Text = $"{trackBarBatchSize.Value}";
			trackBarBatchSize.ValueChanged += delegate
			{
				labelBatchSize.Text = $"{trackBarBatchSize.Value}";
			};




			buttonSelectionRestore.Enabled = false;
			buttonClearMask.Enabled = false;


			RefreshRecentCombobox();


			comboBoxRecentPrompt.SelectedIndexChanged += delegate
			{
				var item = comboBoxRecentPrompt.SelectedItem as RecentItem;
				if (item == null || string.IsNullOrEmpty(item.Raw))
					return;

				if (Control.ModifierKeys.HasFlag(Keys.Shift))
				{
					//add, if shift key pressed
					textBoxPrompt.Text += "\r\n" + item.Raw;
				}
				else
					textBoxPrompt.Text = item.Raw;
			};

			comboBoxRecentNegativePrompt.SelectedIndexChanged += delegate
			{
				var item = comboBoxRecentNegativePrompt.SelectedItem as RecentItem;
				if (item == null || string.IsNullOrEmpty(item.Raw))
					return;

				if (Control.ModifierKeys.HasFlag(Keys.Shift))
				{
					//add, if shift key pressed
					textBoxNegativePrompt.Text += "\r\n" + item.Raw;
				}
				else
					textBoxNegativePrompt.Text = item.Raw;
			};

			for (int n = 64; n <= 2048; n += 64)
			{
				comboBoxWidth.Items.Add($"{n}");
				comboBoxHeight.Items.Add($"{n}");
			}
			comboBoxWidth.SelectedItem = "512";
			comboBoxHeight.SelectedItem = "512";


			buttonSetTransparentColor.BackColor = XmlSetting.Current.TransparentColor;


			if (XmlSetting.Current.ListSampler.Count == 0)
			{
				XmlSetting.Current.ListSampler = new List<string>()
					{
						"Euler",
						"Euler a",
						"LMS",
						"Heun",
						"DPM2",
						"DPM2 a",
						"DPM fast",
						"DPM adaptive",
						"LMS Karras",
						"DPM2 Karras",
						"DPM2 a Karras",
						"DDIM"
					};
			}

			foreach (var item in XmlSetting.Current.ListSampler)
			{
				comboBoxSampler.Items.Add(item);
			}
			comboBoxSampler.SelectedItem = "Euler";


			buttonAbort.Enabled = false;
			buttonAbortForced.Enabled = false;






			if (XmlSetting.Current.IsCheckAutoUpdate)
			{
				TimeSpan span = DateTime.Now - XmlSetting.Current.LastCheckAutoUpdateDate;
				if (span.Days > 1.0)
				{
					XmlSetting.Current.LastCheckAutoUpdateDate = DateTime.Now;
					XmlSetting.Current.Save();

					using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
					{
						UpdateStatus.CheckUpdate((sender, e) =>
						{
							if (e.IsNewVersiionExists)
							{
								try
								{
									Invoke((MethodInvoker)delegate
									{
										pictureBoxNewVersionExists.Visible = true;
										pictureBoxNewVersionExists.Click += delegate
										{
											Uty.OpenURL(XmlSetting.DownloadUrl);
										};
									});
								}
								catch (Exception)
								{
								}
							}
						}
						//TODO: timeout 30sec 
						, 30, cancellationTokenSource.Token);
					}
				}
			}



			FormClosing += delegate
			{
				XmlSetting.Current.LastPrompt = textBoxPrompt.Text;
				XmlSetting.Current.LastNegativePrompt = textBoxNegativePrompt.Text;
				XmlSetting.Current.LastLayerName = textBoxLayerName.Text;

				XmlSetting.Current.Save();
				TaskManager.AbortAll();
			};

		}


		void RefreshRecentCombobox()
		{
			comboBoxRecentPrompt.Items.Clear();
			foreach (var item in XmlRecent.Current.ListPrompt)
			{
				comboBoxRecentPrompt.Items.Add(item);
			}


			comboBoxRecentNegativePrompt.Items.Clear();
			foreach (var item in XmlRecent.Current.ListNegativePrompt)
			{
				comboBoxRecentNegativePrompt.Items.Add(item);
			}
		}





		bool _bAbort { set; get; } = false;

		private void buttonGenerate_Click(object sender, EventArgs e)
		{
			_bAbort = false;
			try
			{
				buttonGenerate.Enabled = false;
				buttonAbort.Enabled = true;
				buttonAbortForced.Enabled = true;

				LogMessage.WriteLine("start generate...");

				int width = int.Parse((string)comboBoxWidth.SelectedItem);
				int height = int.Parse((string)comboBoxHeight.SelectedItem);

				{
					var dirty = XmlRecent.Current.Add(textBoxPrompt.Text, textBoxNegativePrompt.Text);
					if (dirty)
						RefreshRecentCombobox();
				}

				TaskManager.StartSTATask(() =>
				{
					try
					{
						JsonRequestBase request;

						// text2image, if [shift] key pressed
						if (Control.ModifierKeys.HasFlag(Keys.Shift))
						{
							LogMessage.WriteLine("start Text2Image");
							request = new JsonRequestTxt2Img();
						}
						else
							request = new JsonRequestImg2Img();

						int nBatchCount = 1;
						string strLayerNameTemplate = "";

						Invoke((MethodInvoker)delegate
						{
							request.prompt = textBoxPrompt.Text;
							request.negative_prompt = textBoxNegativePrompt.Text;

							request.denoising_strength = (float)trackBarNoiseScale100.Value / 100;
							request.cfg_scale = (float)trackBarCfgScale100.Value / 100;
							request.steps = trackBarStep.Value;
							request.sampler_index = (string)comboBoxSampler.SelectedItem;
							if (numericUpDownSeed.Value >= 0)
								request.seed = numericUpDownSeed.Value;

							request.batch_size = trackBarBatchSize.Value;
							nBatchCount = trackBarBatchCount.Value;

							strLayerNameTemplate = textBoxLayerName.Text;

							request.width = int.Parse((string)comboBoxWidth.SelectedItem);
							request.height = int.Parse((string)comboBoxHeight.SelectedItem);


							if (request.GetType() == typeof(JsonRequestImg2Img))
							{
								var reqImg2Img = request as JsonRequestImg2Img;
								reqImg2Img.mask_blur = trackBarMaskBlur.Value;
								reqImg2Img.inpainting_mask_invert = (checkBoxInpainting_mask_invert.Checked) ? 1 : 0;
								reqImg2Img.init_images = new string[1];
							}


							if (request.GetType() == typeof(JsonRequestTxt2Img))
							{
								var reqTxt2Img = request as JsonRequestTxt2Img;
								//TODO: enable_hr
							}
						});


						if (_bAbort)
							return;

						dynamic appRef = Photoshop.CreateInstance();

						//appRef.Preferences.ExportClipboard = true;
						//var documentsOpen = appRef.Documents.Count;

						if (_bAbort)
							return;

						Photoshop.SetUnit(appRef);


						RectangleDouble curSelection = Photoshop.GetCurrentSelection(appRef);

						if (curSelection == null)
						{
							LogMessage.WriteLine("error: generate failed");
							LogMessage.WriteLine("You must select image in photoshop before generate");
							return;
						}
						if (curSelection.Width != width || curSelection.Height != height)
						{
							LogMessage.WriteLine("error: generate failed");
							LogMessage.WriteLine($"Selection size in photoshop is invalid. Size must be {width}x{height}");
							return;
						}


						if (request.GetType() == typeof(JsonRequestImg2Img))
						{
							var reqImg2Img = request as JsonRequestImg2Img;

							Photoshop.Copy(appRef, true);

							if (Clipboard.ContainsImage())
							{
								using (Image img = Clipboard.GetImage())
								{
									//img.Save(@"g:\desktop\14214.png");

									reqImg2Img.init_images[0] = Automatic1111.Image2String(img);

									if (img.Width != width || img.Height != height)
									{
										LogMessage.WriteLine("error: generate failed");
										LogMessage.WriteLine($"Maybe selection location in photoshop is invalid. Selection area is out of image.");
										return;
									}

									Invoke((MethodInvoker)delegate
									{
										var mask = pictureBoxMask.Image;
										if (mask != null && checkBoxAutoMask.Checked == false)
										{
											reqImg2Img.mask = Automatic1111.Image2String(mask);

											if (mask.Width != width || mask.Height != height)
											{
												LogMessage.WriteLine("error: generate failed");
												LogMessage.WriteLine($"Mask image size is invalid. Need to clear/set mask.");
												return;
											}
										}
										else if (checkBoxAutoMask.Checked)
										{
											using (var bmpMask = CreateMask(img))
											{
												reqImg2Img.mask = Automatic1111.Image2String(bmpMask);
											}
										}
									});
								}
							}
							else
							{
								LogMessage.WriteLine("error: generate failed");
								LogMessage.WriteLine("Failed to obtain image from photoshop");
								return;
							}
						}
						if (_bAbort)
							return;


						for (int i = 0; i < nBatchCount; i++)
						{
							if (_bAbort)
								return;

							if (i > 0)
								request.seed += request.batch_size;
							if (nBatchCount > 1)
							{
								LogMessage.WriteLine($"Start batch {i + 1} / {nBatchCount}");
							}

							var responseObj = Automatic1111.Send(request);
							foreach (var encodedimg in responseObj.images)
							{
								if (_bAbort)
									return;

								string file = Path.Combine(XmlSetting.TempFolder, "output.png");

								Automatic1111.SaveBase64EncodingData(file, encodedimg);

								appRef.Open(file);
								appRef.ActiveDocument.Selection.SelectAll();
								appRef.ActiveDocument.Selection.Copy();
								appRef.ActiveDocument.Close();
								appRef.ActiveDocument.Paste();

								//set layer name
								if (string.IsNullOrEmpty(strLayerNameTemplate) == false)
								{
									var name = strLayerNameTemplate;
									//"@seed, @prompt, @negative, @cfg, @steps, @clip, @strength, @sampler, @subseed, @subseedstrength";
									name = name.Replace(@"@date", $"{DateTime.Now.ToString("yyyyMMdd_HHmmss")}");
									name = name.Replace(@"@seed", $"{responseObj.info.seed}");
									name = name.Replace(@"@prompt", $"{responseObj.info.prompt}");
									name = name.Replace(@"@negative", $"{responseObj.info.negative_prompt}");
									name = name.Replace(@"@cfg", $"{responseObj.info.cfg_scale}");
									name = name.Replace(@"@steps", $"{responseObj.info.steps}");
									name = name.Replace(@"@clip", $"{responseObj.info.clip_skip}");
									name = name.Replace(@"@strength", $"{responseObj.info.denoising_strength}");
									name = name.Replace(@"@sampler", $"{responseObj.info.sampler}");
									name = name.Replace(@"@subseedstrength", $"{responseObj.info.subseed_strength}");
									name = name.Replace(@"@subseed", $"{responseObj.info.subseed}");
									appRef.ActiveDocument.ActiveLayer.name = name;
									LogMessage.WriteLine($"layer: {name}");
								}

								Photoshop.SetSelection(appRef, curSelection);
							}
							foreach (var item in responseObj.info.infotexts)
							{
								LogMessage.WriteLine(item);
							}
						}
						if (_bAbort)
							return;

						LogMessage.WriteLine("ok: generate maybe succeeded");
						return;
					}
					catch (Exception ex)
					{
						//this exception message is ignore, why this error???
						// "General Photoshop error occurred. This functionality may not be available in this version of Photoshop."
						if (ex.Message.IndexOf("This functionality may not") < 0)
							LogMessage.WriteLine(ex.Message);
						LogMessage.WriteLine("error: generate failed. try again");
						return;
					}
					finally
					{
						if (_bAbort)
						{
							LogMessage.WriteLine("abort: generate aborted");
							_bAbort = false;
						}

						Invoke((MethodInvoker)delegate
						{
							buttonGenerate.Enabled = true;
							buttonAbort.Enabled = false;
							buttonAbortForced.Enabled = false;
						});
					}
				});
			}
			catch (Exception ex)
			{
				//this exception message is ignore, why this error???
				// "General Photoshop error occurred. This functionality may not be available in this version of Photoshop."
				if (ex.Message.IndexOf("This functionality may not") < 0)
					LogMessage.WriteLine(ex.Message);
				LogMessage.WriteLine("error: generate failed. try again");
				return;
			}
		}


		private void buttonAbort_Click(object sender, EventArgs e)
		{
			_bAbort = true;
			LogMessage.WriteLine("aborting...");
		}

		private void buttonAbortForced_Click(object sender, EventArgs e)
		{
			try
			{
				_bAbort = true;
				LogMessage.WriteLine("force aborting...");
				Thread.Sleep(50);
				TaskManager.AbortAll();
			}
			catch (Exception)
			{
			}
		}









		/// <summary>
		/// Create Mask
		/// 
		/// returned bitmap obj is need to dispose
		/// </summary>
		Bitmap CreateMask(Image image)
		{
			Bitmap bmpMask = (Bitmap)image.Clone();
			var ColorMask = XmlSetting.Current.TransparentColor;
			for (int x = 0; x < bmpMask.Width; x++)
			{
				for (int y = 0; y < bmpMask.Height; y++)
				{
					var color = bmpMask.GetPixel(x, y);
					if (color.R == ColorMask.R && color.G == ColorMask.G && color.B == ColorMask.B)
					{
						if (color != Color.White)
							bmpMask.SetPixel(x, y, Color.White);
					}
					else
					{
						if (color != Color.Black)
							bmpMask.SetPixel(x, y, Color.Black);
					}
				}
			}
			return bmpMask;
		}









		/// <summary>
		/// Photoshop selection size, force to set a multiplyer of 64.
		/// </summary>
		private void buttonSelectionFit_Click(object sender, EventArgs e)
		{
			try
			{
				int width = int.Parse((string)comboBoxWidth.SelectedItem);
				int height = int.Parse((string)comboBoxHeight.SelectedItem);

				TaskManager.StartSTATask(() =>
				{
					try
					{
						var appRef = Photoshop.CreateInstance();

						var curSelection = Photoshop.GetCurrentSelection(appRef);
						if (curSelection == null)
							Photoshop.SetSelection(appRef, 0, 0, width, height);

						else if (curSelection.Width != width || curSelection.Height != height)
						{
							Photoshop.SetSelection(appRef, curSelection.X, curSelection.Y, width, height);
						}
						curSelection = Photoshop.GetCurrentSelection(appRef);
						//TODO: check

						LogMessage.WriteLine($"ok: select area size fit to  x,y={(int)curSelection.X},{(int)curSelection.Y} w,h={(int)curSelection.Width}x{(int)curSelection.Height}");
					}
					catch (Exception ex)
					{
						LogMessage.WriteLine(ex.Message);
						LogMessage.WriteLine("error: select area size fit failed");
					}
				});
			}
			catch (Exception ex)
			{
				LogMessage.WriteLine(ex.Message);
				LogMessage.WriteLine("error: select area size fit failed");
			}
		}



		RectangleDouble _rectMemory = null;



		/// <summary>
		/// Photoshop selection size, memory
		/// </summary>
		private void buttonSelectionMemory_Click(object sender, EventArgs e)
		{
			try
			{
				TaskManager.StartSTATask(() =>
				{
					var appRef = Photoshop.CreateInstance();
					_rectMemory = Photoshop.GetCurrentSelection(appRef);

					Invoke((MethodInvoker)delegate
					{
						buttonSelectionRestore.Enabled = (_rectMemory == null) ? false : true;
					});

					if (_rectMemory == null)
					{
						LogMessage.WriteLine("error: memory selected area failed");
						LogMessage.WriteLine("You must select image in photoshop before memory selected area");
					}
					else
					{
						LogMessage.WriteLine($"ok: memory selected area  x,y={(int)_rectMemory.X},{(int)_rectMemory.Y} w,h={(int)_rectMemory.Width}x{(int)_rectMemory.Height}");
					}
				});
			}
			catch(Exception ex)
			{
				LogMessage.WriteLine(ex.Message);
				LogMessage.WriteLine("error: memory selected area failed");
			}
		}


		/// <summary>
		/// Photoshop selection size, restore
		/// </summary>
		private void buttonSelectionRestore_Click(object sender, EventArgs e)
		{
			TaskManager.StartSTATask(() =>
			{
				if (_rectMemory == null)
					return;

				var appRef = Photoshop.CreateInstance();
				Photoshop.SetSelection(appRef, _rectMemory);
			});
		}






		private void buttonSetMask_Click(object sender, EventArgs e)
		{
			try
			{
				int width = int.Parse((string)comboBoxWidth.SelectedItem);
				int height = int.Parse((string)comboBoxHeight.SelectedItem);

				TaskManager.StartSTATask(() =>
				{
					dynamic appRef = Photoshop.CreateInstance();

					Photoshop.SetUnit(appRef);

					var curSelection = Photoshop.GetCurrentSelection(appRef);

					if (curSelection == null)
					{
						LogMessage.WriteLine("error: set mask failed");
						LogMessage.WriteLine("You must select image in photoshop before set mask");
						return;
					}
					if (curSelection.Width != width || curSelection.Height != height)
					{
						LogMessage.WriteLine("error: set mask failed");
						LogMessage.WriteLine($"Selection size in photoshop is invalid. Size must be {width}x{height}");
						return;
					}


					Photoshop.Copy(appRef, true);

					if (Clipboard.ContainsImage())
					{
						using (Image img = Clipboard.GetImage())
						{
							var bmpMask = CreateMask(img);

							if (bmpMask.Width != width || bmpMask.Height != height)
							{
								LogMessage.WriteLine("error: set mask failed");
								LogMessage.WriteLine($"Maybe selection location in photoshop is invalid. Selection area is out of image.");
								return;
							}

							Invoke((MethodInvoker)delegate
							{
								var old = pictureBoxMask.Image;
								pictureBoxMask.Image = bmpMask;

								if (old != null)
									old.Dispose();

								buttonClearMask.Enabled = true;
							});

							LogMessage.WriteLine("ok: set mask");
							return;
						}
					}
					else
					{
						LogMessage.WriteLine("error: set mask failed");
						LogMessage.WriteLine("Failed to obtain image from photoshop");
					}
				});
			}
			catch(Exception ex)
			{
				LogMessage.WriteLine(ex.Message);
				LogMessage.WriteLine("error: set mask failed");
			}
		}


		private void buttonClearMask_Click(object sender, EventArgs e)
		{
			try
			{
				var old = pictureBoxMask.Image;
				pictureBoxMask.Image = null;
				if (old != null)
					old.Dispose();

				buttonClearMask.Enabled = false;
			}
			catch(Exception ex)
			{
				LogMessage.WriteLine(ex.Message);
				LogMessage.WriteLine("error: clear mask failed");
			}
		}

		private void buttonSetTransparentColor_Click(object sender, EventArgs e)
		{
			using (ColorDialog dlg = new ColorDialog())
			{
				dlg.Color = XmlSetting.Current.TransparentColor;
				var ret = dlg.ShowDialog(this);
				if (ret != DialogResult.OK)
					return;

				if (XmlSetting.Current.TransparentColor == dlg.Color)
					return;

				XmlSetting.Current.TransparentColor = dlg.Color;
				XmlSetting.Current.Save();

				buttonSetTransparentColor.BackColor = XmlSetting.Current.TransparentColor;
			}
		}


		private void buttonSetting_Click(object sender, EventArgs e)
		{
			using (FormSetting dlg = new FormSetting())
			{
				dlg.ShowDialog(this);
			}
		}

		private void buttonLogWrite_Click(object sender, EventArgs e)
		{
			LogMessage.WriteLine($"note: {textBoxLogWrite.Text}");
		}
	}
}
