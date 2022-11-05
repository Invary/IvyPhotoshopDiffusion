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
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;






namespace Invary.IvyPhotoshopDiffusion
{
	// Automatic1111 need command line option '--api', otherwise 404 error


	//TODO: custom prompt selection, combobox
	//TODO: save last setting
	//TODO: dupe exec check?

	//TODO: read png metadata
	//TODO: NAI infotext import
	//TODO: ctrl+↑↓ at prompt inputing




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

			numericUpDownClipSkip.Value = XmlSetting.Current.LastClipSkip;
			numericUpDownENSD.Value = XmlSetting.Current.LastENSD;



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


			labelSubseedStrength.Text = $"{(double)trackBarSubseedStrength100.Value / 100:0.##}";
			trackBarSubseedStrength100.ValueChanged += delegate
			{
				labelSubseedStrength.Text = $"{(double)trackBarSubseedStrength100.Value / 100:0.##}";
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


			//subseedW/H with "-1" and "0"
			comboBoxSubseedW.Items.Add("-1");
			comboBoxSubseedH.Items.Add("-1");
			for (int n = 0; n <= 2048; n += 64)
			{
				comboBoxSubseedW.Items.Add($"{n}");
				comboBoxSubseedH.Items.Add($"{n}");
			}
			comboBoxSubseedW.SelectedItem = "0";
			comboBoxSubseedH.SelectedItem = "0";


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

			toolStripProgressBar1.Visible = false;
			toolStripProgressBar1.Step = 1;
			toolStripProgressBar1.Minimum = 0;
			toolStripProgressBar1.Maximum = 100;

			toolStripStatusLabel.Text = "";



			//transparent color button, set right click menu
			{
				ContextMenu menu = new ContextMenu();

				menu.MenuItems.Add(new MenuItem("Get from Photoshop foreground color", delegate
				{
					dynamic appRef = Photoshop.CreateInstance();
					var color = Photoshop.GetForegroundColor(appRef);
					buttonSetTransparentColor.BackColor = color;
					XmlSetting.Current.TransparentColor = color;
				}));
				menu.MenuItems.Add(new MenuItem("Get from Photoshop background color", delegate
				{
					dynamic appRef = Photoshop.CreateInstance();
					var color = Photoshop.GetBackgroundColor(appRef);
					buttonSetTransparentColor.BackColor = color;
					XmlSetting.Current.TransparentColor = color;
				}));

				menu.MenuItems.Add(new MenuItem("-"));

				menu.MenuItems.Add(new MenuItem("Set to Photoshop foreground color", delegate
				{
					dynamic appRef = Photoshop.CreateInstance();
					Photoshop.SetForegroundColor(appRef, XmlSetting.Current.TransparentColor);
				}));
				menu.MenuItems.Add(new MenuItem("Set to Photoshop background color", delegate
				{
					dynamic appRef = Photoshop.CreateInstance();
					Photoshop.SetBackgroundColor(appRef, XmlSetting.Current.TransparentColor);
				}));

				buttonSetTransparentColor.ContextMenu = menu;
			}



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

				XmlSetting.Current.LastClipSkip = (int)numericUpDownClipSkip.Value;
				XmlSetting.Current.LastENSD = (int)numericUpDownENSD.Value;


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
				StartProgress();

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

							request.restore_faces = checkBoxRestoreFace.Checked;
							request.tiling = checkBoxTiling.Checked;

							request.override_settings = new Override_Settings();
							request.override_settings.CLIP_stop_at_last_layers = (int)numericUpDownClipSkip.Value;
							request.override_settings.eta_noise_seed_delta = (int)numericUpDownENSD.Value;

							request.subseed = numericUpDownSubseed.Value;
							request.subseed_strength = (float)trackBarSubseedStrength100.Value / 100;
							request.seed_resize_from_w = int.Parse((string)comboBoxSubseedW.SelectedItem);
							request.seed_resize_from_h = int.Parse((string)comboBoxSubseedH.SelectedItem);


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

							try
							{
								Invoke((MethodInvoker)delegate
								{
									toolStripStatusLabel.Text = $"batch: {i + 1}/{nBatchCount}";
								});
							}
							catch(Exception)
							{
							}

							if (i > 0)
								request.seed += request.batch_size;
							if (nBatchCount > 1)
							{
								LogMessage.WriteLine($"Start batch {i + 1} / {nBatchCount}");
							}

							var responseObj = Automatic1111.Send(request);
							int index = 0;
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

									ParametersBase param = null;
									if (responseObj.GetType() == typeof(JsonResponseImg2Img))
										param = (responseObj as JsonResponseImg2Img).parameters;
									else if (responseObj.GetType() == typeof(JsonResponseTxt2Img))
										param = (responseObj as JsonResponseTxt2Img).parameters;
									if (param != null)
									{
										name = name.Replace(@"@seed", $"{responseObj.Info.all_seeds[index]}");
										name = name.Replace(@"@prompt", $"{param.prompt}");
										name = name.Replace(@"@negative", $"{param.negative_prompt}");
										name = name.Replace(@"@cfg", $"{param.cfg_scale}");
										name = name.Replace(@"@steps", $"{param.steps}");
										name = name.Replace(@"@strength", $"{param.denoising_strength}");
										name = name.Replace(@"@sampler", $"{param.sampler_index}");
										name = name.Replace(@"@subseedstrength", $"{param.subseed_strength}");
										name = name.Replace(@"@subseed", $"{responseObj.Info.all_subseeds[index]}");
										name = name.Replace(@"@clip", $"{responseObj.Info.clip_skip}");
									}
									appRef.ActiveDocument.ActiveLayer.name = name;
									LogMessage.WriteLine($"layer: {name}");
								}

								Photoshop.SetSelection(appRef, curSelection);
								index++;
							}
							foreach (var item in responseObj.Info.infotexts)
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
						_bAbortProgress = true;

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



		private void buttonReadInfoText_Click(object sender, EventArgs e)
		{
			ImportAutomatic1111InfotextToUI(textBoxPrompt.Text);
		}



		void ImportAutomatic1111InfotextToUI(string text)
		{
			/*
			masterpiece, best quality, 
			girl
			Negative prompt: lowres, bad anatomy, bad hands
			man
			Steps: 23, Sampler: Euler a, CFG scale: 8, Seed: 1574434520, Size: 768x1024, Model hash: 81761151, Model: sdv1-5-pruned-emaonly, Variation seed: 123, Variation seed strength: 0.17, Seed resize from: 320x384, ENSD: 31337
			*/
			text = text.Replace("\r", "");
			var lines = text.Split('\n');
			var prompt = "";
			var negative = "";

			int i = 0;
			for (; i < lines.Length; i++)
			{
				if (lines[i].StartsWith("Negative prompt: "))
					break;
				if (lines[i].StartsWith("Steps: "))
					break;
				if (lines[i].StartsWith("Sampler: "))
					break;
				if (lines[i].StartsWith("Seed: "))
					break;
				if (lines[i].StartsWith("Size: "))
					break;

				prompt += lines[i];
				prompt += "\n";
			}
			for (; i < lines.Length; i++)
			{
				if (lines[i].StartsWith("Negative prompt: "))
				{
					negative = lines[i].Substring("Negative prompt: ".Length, lines[i].Length - "Negative prompt: ".Length);
					negative += "\n";
					continue;
				}
				if (lines[i].StartsWith("Steps: "))
					break;
				if (lines[i].StartsWith("Sampler: "))
					break;
				if (lines[i].StartsWith("Seed: "))
					break;
				if (lines[i].StartsWith("Size: "))
					break;

				negative += lines[i];
				negative += "\n";
			}

			for (; i < lines.Length; i++)
			{
				var options = lines[i].Split(',');
				foreach (var item in options)
				{
					//case sensitive
					// DO NOT ignore upper/lower character

					var option = item.Trim(' ');

					SetOptionValue("Steps: ", option, trackBarStep);
					SetOptionValue("Sampler: ", option, comboBoxSampler);
					SetOptionValue("CFG scale: ", option, trackBarCfgScale100, 100);
					SetOptionValue("Seed: ", option, numericUpDownSeed);
					SetOptionValue("Size: ", option, comboBoxWidth, comboBoxHeight);

					SetOptionValue("Variation seed: ", option, numericUpDownSubseed);
					SetOptionValue("Variation seed strength: ", option, trackBarSubseedStrength100, 100);
					SetOptionValue("Seed resize from: ", option, comboBoxSubseedW, comboBoxSubseedH);

					SetOptionValue("ENSD: ", option, numericUpDownENSD);
					SetOptionValue("Clip skip: ", option, numericUpDownClipSkip);

					SetOptionValue("Denoising strength: ", option, trackBarCfgScale100, 100);
					SetOptionValue("Mask blur: ", option, trackBarMaskBlur);
				}
			}

			textBoxPrompt.Text = prompt.Replace("\n", "\r\n");
			textBoxNegativePrompt.Text = negative.Replace("\n", "\r\n");
		}



		/// <summary>
		/// "ENSD: 31337" to NumericUpDown
		/// </summary>
		/// <param name="header">"ENSD: "</param>
		/// <param name="text">"ENSD: 31337"</param>
		/// <param name="numericUpDown"></param>
		/// <returns></returns>
		bool SetOptionValue(string header, string text, NumericUpDown numericUpDown)
		{
			if (text.StartsWith(header) == false)
				return false;
			text = text.Substring(header.Length, text.Length - header.Length);

			try
			{
				decimal value = decimal.Parse(text);
				numericUpDown.Value = value;
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}


		/// <summary>
		/// "Sampler: Euler a" to combobox
		/// </summary>
		/// <param name="header">"Sampler: "</param>
		/// <param name="text">"Sampler: Euler a"</param>
		bool SetOptionValue(string header, string text, ComboBox comboBox)
		{
			if (text.StartsWith(header) == false)
				return false;
			text = text.Substring(header.Length, text.Length - header.Length);

			try
			{
				comboBox.SelectedItem = text;
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}



		/// <summary>
		/// "Variation seed strength: 0.17" to trackbar with multiplied value
		/// </summary>
		/// <param name="header">"Variation seed strength: "</param>
		/// <param name="text">Variation seed strength: 0.17</param>
		/// <param name="trackbar"></param>
		/// <param name="multiply">if 100, value 0.17x100=17 set to trackbar</param>
		/// <returns></returns>
		bool SetOptionValue(string header, string text, TrackBar trackbar, int multiply = 1)
		{
			if (text.StartsWith(header) == false)
				return false;
			text = text.Substring(header.Length, text.Length - header.Length);

			try
			{
				float value = float.Parse(text);
				value *= multiply;
				trackbar.Value = (int)value;
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}



		/// <summary>
		/// "Size: 512x512" to two combobox
		/// </summary>
		/// <param name="header">"Size: "</param>
		/// <param name="text">"Size: 512x512"</param>
		bool SetOptionValue(string header, string text, ComboBox comboBox1, ComboBox comboBox2)
		{
			if (text.StartsWith(header) == false)
				return false;
			text = text.Substring(header.Length, text.Length - header.Length);
			var data = text.Split('x');
			if (data.Length != 2 || data[0].Length == 0 || data[1].Length == 0)
				return false;

			try
			{
				comboBox1.SelectedItem = data[0];
				comboBox2.SelectedItem = data[1];
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		private void buttonNovelAIto1111Conv_Click(object sender, EventArgs e)
		{
			textBoxPrompt.Text = NovelAI.ConvertNAIto1111(textBoxPrompt.Text);
		}



		bool _bProgress = false;
		bool _bAbortProgress = false;


		void StartProgress()
		{
			if (_bProgress)
				return;
			_bProgress = true;
			_bAbortProgress = false;
			toolStripProgressBar1.Value = 0;
			toolStripProgressBar1.Visible = true;
			toolStripStatusLabel.Text = "";

			TaskManager.StartSTATask(() =>
			{
				try
				{
					while (_bAbortProgress == false)
					{
						var response = Automatic1111.SendGetProgress();

						if (_bAbortProgress)
							break;

						Invoke((MethodInvoker)delegate
						{
							toolStripProgressBar1.Value = (int)(response.progress * 100.0);
						});

						Thread.Sleep(500);
					}

					Invoke((MethodInvoker)delegate
					{
						toolStripProgressBar1.Visible = false;
						toolStripProgressBar1.Value = 0;
						toolStripStatusLabel.Text = "";
					});
				}
				catch (Exception ex)
				{
					LogMessage.WriteLine(ex.Message);
					LogMessage.WriteLine("error: get progress failed");
				}
				finally
				{
					_bProgress = false;
				}
			});
		}





	}
}
