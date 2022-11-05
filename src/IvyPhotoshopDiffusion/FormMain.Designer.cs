namespace Invary.IvyPhotoshopDiffusion
{
	partial class FormMain
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.buttonGenerate = new System.Windows.Forms.Button();
			this.textBoxPrompt = new System.Windows.Forms.TextBox();
			this.textBoxNegativePrompt = new System.Windows.Forms.TextBox();
			this.trackBarNoiseScale100 = new System.Windows.Forms.TrackBar();
			this.trackBarMaskBlur = new System.Windows.Forms.TrackBar();
			this.labelNoiseScale100 = new System.Windows.Forms.Label();
			this.labelMaskBlur = new System.Windows.Forms.Label();
			this.buttonSelectionFit = new System.Windows.Forms.Button();
			this.checkBoxInpainting_mask_invert = new System.Windows.Forms.CheckBox();
			this.buttonSelectionMemory = new System.Windows.Forms.Button();
			this.buttonSelectionRestore = new System.Windows.Forms.Button();
			this.pictureBoxMask = new System.Windows.Forms.PictureBox();
			this.buttonClearMask = new System.Windows.Forms.Button();
			this.buttonSetMask = new System.Windows.Forms.Button();
			this.checkBoxAutoMask = new System.Windows.Forms.CheckBox();
			this.comboBoxRecentPrompt = new System.Windows.Forms.ComboBox();
			this.comboBoxRecentNegativePrompt = new System.Windows.Forms.ComboBox();
			this.textBoxLogMessage = new System.Windows.Forms.TextBox();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.comboBoxHeight = new System.Windows.Forms.ComboBox();
			this.comboBoxWidth = new System.Windows.Forms.ComboBox();
			this.buttonSetTransparentColor = new System.Windows.Forms.Button();
			this.trackBarCfgScale100 = new System.Windows.Forms.TrackBar();
			this.trackBarStep = new System.Windows.Forms.TrackBar();
			this.comboBoxSampler = new System.Windows.Forms.ComboBox();
			this.numericUpDownSeed = new System.Windows.Forms.NumericUpDown();
			this.trackBarBatchSize = new System.Windows.Forms.TrackBar();
			this.trackBarBatchCount = new System.Windows.Forms.TrackBar();
			this.pictureBoxNewVersionExists = new System.Windows.Forms.PictureBox();
			this.textBoxLogWrite = new System.Windows.Forms.TextBox();
			this.textBoxLayerName = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.numericUpDownSubseed = new System.Windows.Forms.NumericUpDown();
			this.trackBarSubseedStrength100 = new System.Windows.Forms.TrackBar();
			this.buttonReadInfoText = new System.Windows.Forms.Button();
			this.comboBoxSubseedH = new System.Windows.Forms.ComboBox();
			this.comboBoxSubseedW = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.labelCfgScale100 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.labelStep = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.buttonSetting = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.labelBatchSize = new System.Windows.Forms.Label();
			this.labelBatchCount = new System.Windows.Forms.Label();
			this.buttonAbort = new System.Windows.Forms.Button();
			this.buttonAbortForced = new System.Windows.Forms.Button();
			this.buttonLogWrite = new System.Windows.Forms.Button();
			this.checkBoxRestoreFace = new System.Windows.Forms.CheckBox();
			this.checkBoxTiling = new System.Windows.Forms.CheckBox();
			this.numericUpDownENSD = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownClipSkip = new System.Windows.Forms.NumericUpDown();
			this.labelSubseedStrength = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.buttonNovelAIto1111Conv = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
			this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			((System.ComponentModel.ISupportInitialize)(this.trackBarNoiseScale100)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarMaskBlur)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxMask)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarCfgScale100)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarStep)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeed)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarBatchSize)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarBatchCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxNewVersionExists)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSubseed)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarSubseedStrength100)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownENSD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownClipSkip)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonGenerate
			// 
			this.buttonGenerate.Location = new System.Drawing.Point(658, 293);
			this.buttonGenerate.Name = "buttonGenerate";
			this.buttonGenerate.Size = new System.Drawing.Size(112, 41);
			this.buttonGenerate.TabIndex = 0;
			this.buttonGenerate.Text = "Generate";
			this.toolTip.SetToolTip(this.buttonGenerate, "Generate image\r\nText2Image, if [shift] key pressed. \r\nImage2Image, if no key pres" +
        "sed");
			this.buttonGenerate.UseVisualStyleBackColor = true;
			this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
			// 
			// textBoxPrompt
			// 
			this.textBoxPrompt.AcceptsReturn = true;
			this.textBoxPrompt.Location = new System.Drawing.Point(34, 38);
			this.textBoxPrompt.Multiline = true;
			this.textBoxPrompt.Name = "textBoxPrompt";
			this.textBoxPrompt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxPrompt.Size = new System.Drawing.Size(439, 75);
			this.textBoxPrompt.TabIndex = 1;
			this.toolTip.SetToolTip(this.textBoxPrompt, "Prompt");
			// 
			// textBoxNegativePrompt
			// 
			this.textBoxNegativePrompt.AcceptsReturn = true;
			this.textBoxNegativePrompt.Location = new System.Drawing.Point(34, 133);
			this.textBoxNegativePrompt.Multiline = true;
			this.textBoxNegativePrompt.Name = "textBoxNegativePrompt";
			this.textBoxNegativePrompt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxNegativePrompt.Size = new System.Drawing.Size(439, 75);
			this.textBoxNegativePrompt.TabIndex = 2;
			this.toolTip.SetToolTip(this.textBoxNegativePrompt, "Negative prompt");
			// 
			// trackBarNoiseScale100
			// 
			this.trackBarNoiseScale100.AutoSize = false;
			this.trackBarNoiseScale100.LargeChange = 1;
			this.trackBarNoiseScale100.Location = new System.Drawing.Point(65, 221);
			this.trackBarNoiseScale100.Maximum = 100;
			this.trackBarNoiseScale100.Name = "trackBarNoiseScale100";
			this.trackBarNoiseScale100.Size = new System.Drawing.Size(408, 23);
			this.trackBarNoiseScale100.TabIndex = 3;
			this.trackBarNoiseScale100.TickStyle = System.Windows.Forms.TickStyle.None;
			this.toolTip.SetToolTip(this.trackBarNoiseScale100, "Denoising strength. default value is 0.75");
			this.trackBarNoiseScale100.Value = 75;
			// 
			// trackBarMaskBlur
			// 
			this.trackBarMaskBlur.AutoSize = false;
			this.trackBarMaskBlur.LargeChange = 1;
			this.trackBarMaskBlur.Location = new System.Drawing.Point(65, 245);
			this.trackBarMaskBlur.Maximum = 512;
			this.trackBarMaskBlur.Name = "trackBarMaskBlur";
			this.trackBarMaskBlur.Size = new System.Drawing.Size(408, 21);
			this.trackBarMaskBlur.TabIndex = 4;
			this.trackBarMaskBlur.TickStyle = System.Windows.Forms.TickStyle.None;
			this.toolTip.SetToolTip(this.trackBarMaskBlur, "Mask blur. default value is 4. Unit: pixel");
			this.trackBarMaskBlur.Value = 4;
			// 
			// labelNoiseScale100
			// 
			this.labelNoiseScale100.AutoSize = true;
			this.labelNoiseScale100.Location = new System.Drawing.Point(479, 232);
			this.labelNoiseScale100.Name = "labelNoiseScale100";
			this.labelNoiseScale100.Size = new System.Drawing.Size(35, 12);
			this.labelNoiseScale100.TabIndex = 5;
			this.labelNoiseScale100.Text = "label1";
			// 
			// labelMaskBlur
			// 
			this.labelMaskBlur.AutoSize = true;
			this.labelMaskBlur.Location = new System.Drawing.Point(479, 254);
			this.labelMaskBlur.Name = "labelMaskBlur";
			this.labelMaskBlur.Size = new System.Drawing.Size(35, 12);
			this.labelMaskBlur.TabIndex = 6;
			this.labelMaskBlur.Text = "label2";
			// 
			// buttonSelectionFit
			// 
			this.buttonSelectionFit.Location = new System.Drawing.Point(21, 84);
			this.buttonSelectionFit.Name = "buttonSelectionFit";
			this.buttonSelectionFit.Size = new System.Drawing.Size(75, 23);
			this.buttonSelectionFit.TabIndex = 7;
			this.buttonSelectionFit.Text = "Fit";
			this.toolTip.SetToolTip(this.buttonSelectionFit, "Change size of selection area in photoshop\r\n");
			this.buttonSelectionFit.UseVisualStyleBackColor = true;
			this.buttonSelectionFit.Click += new System.EventHandler(this.buttonSelectionFit_Click);
			// 
			// checkBoxInpainting_mask_invert
			// 
			this.checkBoxInpainting_mask_invert.AutoSize = true;
			this.checkBoxInpainting_mask_invert.Location = new System.Drawing.Point(53, 464);
			this.checkBoxInpainting_mask_invert.Name = "checkBoxInpainting_mask_invert";
			this.checkBoxInpainting_mask_invert.Size = new System.Drawing.Size(84, 16);
			this.checkBoxInpainting_mask_invert.TabIndex = 8;
			this.checkBoxInpainting_mask_invert.Text = "Invert mask";
			this.toolTip.SetToolTip(this.checkBoxInpainting_mask_invert, "Unchecked = Transparent color area will be painted\r\nChecked = Except transparent " +
        "colore area will be painted");
			this.checkBoxInpainting_mask_invert.UseVisualStyleBackColor = true;
			// 
			// buttonSelectionMemory
			// 
			this.buttonSelectionMemory.Location = new System.Drawing.Point(21, 134);
			this.buttonSelectionMemory.Name = "buttonSelectionMemory";
			this.buttonSelectionMemory.Size = new System.Drawing.Size(75, 23);
			this.buttonSelectionMemory.TabIndex = 9;
			this.buttonSelectionMemory.Text = "Memory";
			this.toolTip.SetToolTip(this.buttonSelectionMemory, "Record selection location and size in photoshop");
			this.buttonSelectionMemory.UseVisualStyleBackColor = true;
			this.buttonSelectionMemory.Click += new System.EventHandler(this.buttonSelectionMemory_Click);
			// 
			// buttonSelectionRestore
			// 
			this.buttonSelectionRestore.Location = new System.Drawing.Point(21, 187);
			this.buttonSelectionRestore.Name = "buttonSelectionRestore";
			this.buttonSelectionRestore.Size = new System.Drawing.Size(75, 23);
			this.buttonSelectionRestore.TabIndex = 10;
			this.buttonSelectionRestore.Text = "Restore";
			this.toolTip.SetToolTip(this.buttonSelectionRestore, "Restore the selection position and size in Photoshop\r\n to the way it was recorded" +
        "");
			this.buttonSelectionRestore.UseVisualStyleBackColor = true;
			this.buttonSelectionRestore.Click += new System.EventHandler(this.buttonSelectionRestore_Click);
			// 
			// pictureBoxMask
			// 
			this.pictureBoxMask.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBoxMask.Location = new System.Drawing.Point(24, 522);
			this.pictureBoxMask.Name = "pictureBoxMask";
			this.pictureBoxMask.Size = new System.Drawing.Size(256, 256);
			this.pictureBoxMask.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBoxMask.TabIndex = 12;
			this.pictureBoxMask.TabStop = false;
			// 
			// buttonClearMask
			// 
			this.buttonClearMask.Location = new System.Drawing.Point(205, 486);
			this.buttonClearMask.Name = "buttonClearMask";
			this.buttonClearMask.Size = new System.Drawing.Size(75, 23);
			this.buttonClearMask.TabIndex = 15;
			this.buttonClearMask.Text = "Clear";
			this.toolTip.SetToolTip(this.buttonClearMask, "Clear current mask image");
			this.buttonClearMask.UseVisualStyleBackColor = true;
			this.buttonClearMask.Click += new System.EventHandler(this.buttonClearMask_Click);
			// 
			// buttonSetMask
			// 
			this.buttonSetMask.Location = new System.Drawing.Point(117, 486);
			this.buttonSetMask.Name = "buttonSetMask";
			this.buttonSetMask.Size = new System.Drawing.Size(75, 23);
			this.buttonSetMask.TabIndex = 16;
			this.buttonSetMask.Text = "Set mask";
			this.toolTip.SetToolTip(this.buttonSetMask, "Obtain image from photoshop, and create mask image.");
			this.buttonSetMask.UseVisualStyleBackColor = true;
			this.buttonSetMask.Click += new System.EventHandler(this.buttonSetMask_Click);
			// 
			// checkBoxAutoMask
			// 
			this.checkBoxAutoMask.AutoSize = true;
			this.checkBoxAutoMask.Location = new System.Drawing.Point(32, 490);
			this.checkBoxAutoMask.Name = "checkBoxAutoMask";
			this.checkBoxAutoMask.Size = new System.Drawing.Size(79, 16);
			this.checkBoxAutoMask.TabIndex = 17;
			this.checkBoxAutoMask.Text = "Auto mask";
			this.toolTip.SetToolTip(this.checkBoxAutoMask, "If checked, the mask is automatically generated\r\nand only places with transparent" +
        " color in photoshop will be painted.");
			this.checkBoxAutoMask.UseVisualStyleBackColor = true;
			// 
			// comboBoxRecentPrompt
			// 
			this.comboBoxRecentPrompt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxRecentPrompt.FormattingEnabled = true;
			this.comboBoxRecentPrompt.Location = new System.Drawing.Point(491, 89);
			this.comboBoxRecentPrompt.Name = "comboBoxRecentPrompt";
			this.comboBoxRecentPrompt.Size = new System.Drawing.Size(121, 20);
			this.comboBoxRecentPrompt.TabIndex = 18;
			this.toolTip.SetToolTip(this.comboBoxRecentPrompt, "Recently prompt\r\nPress [Shift] key, for adding.");
			// 
			// comboBoxRecentNegativePrompt
			// 
			this.comboBoxRecentNegativePrompt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxRecentNegativePrompt.FormattingEnabled = true;
			this.comboBoxRecentNegativePrompt.Location = new System.Drawing.Point(491, 188);
			this.comboBoxRecentNegativePrompt.Name = "comboBoxRecentNegativePrompt";
			this.comboBoxRecentNegativePrompt.Size = new System.Drawing.Size(121, 20);
			this.comboBoxRecentNegativePrompt.TabIndex = 19;
			this.toolTip.SetToolTip(this.comboBoxRecentNegativePrompt, "Recently negative prompt\r\nPress [Shift] key, for adding.\r\n");
			// 
			// textBoxLogMessage
			// 
			this.textBoxLogMessage.AcceptsReturn = true;
			this.textBoxLogMessage.AcceptsTab = true;
			this.textBoxLogMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxLogMessage.Location = new System.Drawing.Point(318, 522);
			this.textBoxLogMessage.Multiline = true;
			this.textBoxLogMessage.Name = "textBoxLogMessage";
			this.textBoxLogMessage.ReadOnly = true;
			this.textBoxLogMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxLogMessage.Size = new System.Drawing.Size(421, 221);
			this.textBoxLogMessage.TabIndex = 20;
			this.textBoxLogMessage.TabStop = false;
			this.toolTip.SetToolTip(this.textBoxLogMessage, "Log message");
			// 
			// comboBoxHeight
			// 
			this.comboBoxHeight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxHeight.FormattingEnabled = true;
			this.comboBoxHeight.Location = new System.Drawing.Point(68, 38);
			this.comboBoxHeight.Name = "comboBoxHeight";
			this.comboBoxHeight.Size = new System.Drawing.Size(47, 20);
			this.comboBoxHeight.TabIndex = 12;
			this.toolTip.SetToolTip(this.comboBoxHeight, "Height, default value is 512. Unit: pixel");
			// 
			// comboBoxWidth
			// 
			this.comboBoxWidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxWidth.FormattingEnabled = true;
			this.comboBoxWidth.Location = new System.Drawing.Point(7, 38);
			this.comboBoxWidth.Name = "comboBoxWidth";
			this.comboBoxWidth.Size = new System.Drawing.Size(47, 20);
			this.comboBoxWidth.TabIndex = 11;
			this.toolTip.SetToolTip(this.comboBoxWidth, "Width, default value is 512. Unit: pixel");
			// 
			// buttonSetTransparentColor
			// 
			this.buttonSetTransparentColor.Location = new System.Drawing.Point(205, 784);
			this.buttonSetTransparentColor.Name = "buttonSetTransparentColor";
			this.buttonSetTransparentColor.Size = new System.Drawing.Size(75, 23);
			this.buttonSetTransparentColor.TabIndex = 22;
			this.toolTip.SetToolTip(this.buttonSetTransparentColor, resources.GetString("buttonSetTransparentColor.ToolTip"));
			this.buttonSetTransparentColor.UseVisualStyleBackColor = true;
			this.buttonSetTransparentColor.Click += new System.EventHandler(this.buttonSetTransparentColor_Click);
			// 
			// trackBarCfgScale100
			// 
			this.trackBarCfgScale100.AutoSize = false;
			this.trackBarCfgScale100.LargeChange = 50;
			this.trackBarCfgScale100.Location = new System.Drawing.Point(65, 271);
			this.trackBarCfgScale100.Maximum = 1500;
			this.trackBarCfgScale100.Name = "trackBarCfgScale100";
			this.trackBarCfgScale100.Size = new System.Drawing.Size(408, 21);
			this.trackBarCfgScale100.TabIndex = 24;
			this.trackBarCfgScale100.TickStyle = System.Windows.Forms.TickStyle.None;
			this.toolTip.SetToolTip(this.trackBarCfgScale100, "CFG scale. default value is 7.0");
			this.trackBarCfgScale100.Value = 700;
			// 
			// trackBarStep
			// 
			this.trackBarStep.AutoSize = false;
			this.trackBarStep.LargeChange = 1;
			this.trackBarStep.Location = new System.Drawing.Point(180, 298);
			this.trackBarStep.Maximum = 200;
			this.trackBarStep.Name = "trackBarStep";
			this.trackBarStep.Size = new System.Drawing.Size(293, 21);
			this.trackBarStep.TabIndex = 29;
			this.trackBarStep.TickStyle = System.Windows.Forms.TickStyle.None;
			this.toolTip.SetToolTip(this.trackBarStep, "Sampling steps. default value is 20");
			this.trackBarStep.Value = 20;
			// 
			// comboBoxSampler
			// 
			this.comboBoxSampler.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxSampler.FormattingEnabled = true;
			this.comboBoxSampler.Location = new System.Drawing.Point(25, 295);
			this.comboBoxSampler.Name = "comboBoxSampler";
			this.comboBoxSampler.Size = new System.Drawing.Size(102, 20);
			this.comboBoxSampler.TabIndex = 32;
			this.toolTip.SetToolTip(this.comboBoxSampler, "Sampling method");
			// 
			// numericUpDownSeed
			// 
			this.numericUpDownSeed.Location = new System.Drawing.Point(471, 350);
			this.numericUpDownSeed.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
			this.numericUpDownSeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
			this.numericUpDownSeed.Name = "numericUpDownSeed";
			this.numericUpDownSeed.Size = new System.Drawing.Size(120, 19);
			this.numericUpDownSeed.TabIndex = 33;
			this.toolTip.SetToolTip(this.numericUpDownSeed, "Seed. -1 mean \'random seed\'. default value is -1");
			this.numericUpDownSeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
			// 
			// trackBarBatchSize
			// 
			this.trackBarBatchSize.AutoSize = false;
			this.trackBarBatchSize.LargeChange = 1;
			this.trackBarBatchSize.Location = new System.Drawing.Point(245, 355);
			this.trackBarBatchSize.Maximum = 48;
			this.trackBarBatchSize.Minimum = 1;
			this.trackBarBatchSize.Name = "trackBarBatchSize";
			this.trackBarBatchSize.Size = new System.Drawing.Size(112, 21);
			this.trackBarBatchSize.TabIndex = 36;
			this.trackBarBatchSize.TickStyle = System.Windows.Forms.TickStyle.None;
			this.toolTip.SetToolTip(this.trackBarBatchSize, "Batch size. default value is 1. The larger the value, the more VRAM is required.");
			this.trackBarBatchSize.Value = 1;
			// 
			// trackBarBatchCount
			// 
			this.trackBarBatchCount.AutoSize = false;
			this.trackBarBatchCount.LargeChange = 1;
			this.trackBarBatchCount.Location = new System.Drawing.Point(245, 328);
			this.trackBarBatchCount.Maximum = 200;
			this.trackBarBatchCount.Minimum = 1;
			this.trackBarBatchCount.Name = "trackBarBatchCount";
			this.trackBarBatchCount.Size = new System.Drawing.Size(112, 21);
			this.trackBarBatchCount.TabIndex = 39;
			this.trackBarBatchCount.TickStyle = System.Windows.Forms.TickStyle.None;
			this.toolTip.SetToolTip(this.trackBarBatchCount, "Batch count. default value is 1");
			this.trackBarBatchCount.Value = 1;
			// 
			// pictureBoxNewVersionExists
			// 
			this.pictureBoxNewVersionExists.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pictureBoxNewVersionExists.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxNewVersionExists.Image")));
			this.pictureBoxNewVersionExists.Location = new System.Drawing.Point(731, 719);
			this.pictureBoxNewVersionExists.Name = "pictureBoxNewVersionExists";
			this.pictureBoxNewVersionExists.Size = new System.Drawing.Size(52, 50);
			this.pictureBoxNewVersionExists.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBoxNewVersionExists.TabIndex = 43;
			this.pictureBoxNewVersionExists.TabStop = false;
			this.toolTip.SetToolTip(this.pictureBoxNewVersionExists, "New version exists");
			this.pictureBoxNewVersionExists.Visible = false;
			// 
			// textBoxLogWrite
			// 
			this.textBoxLogWrite.Location = new System.Drawing.Point(318, 750);
			this.textBoxLogWrite.Name = "textBoxLogWrite";
			this.textBoxLogWrite.Size = new System.Drawing.Size(326, 19);
			this.textBoxLogWrite.TabIndex = 46;
			this.toolTip.SetToolTip(this.textBoxLogWrite, "Text message write to log file.\r\nthis is only for note.");
			// 
			// textBoxLayerName
			// 
			this.textBoxLayerName.Location = new System.Drawing.Point(318, 497);
			this.textBoxLayerName.Name = "textBoxLayerName";
			this.textBoxLayerName.Size = new System.Drawing.Size(403, 19);
			this.textBoxLayerName.TabIndex = 48;
			this.toolTip.SetToolTip(this.textBoxLayerName, resources.GetString("textBoxLayerName.ToolTip"));
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(318, 479);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(64, 12);
			this.label9.TabIndex = 49;
			this.label9.Text = "Layer name";
			this.toolTip.SetToolTip(this.label9, resources.GetString("label9.ToolTip"));
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(24, 404);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(35, 12);
			this.label10.TabIndex = 53;
			this.label10.Text = "ENSD";
			this.toolTip.SetToolTip(this.label10, "Eta noise seed delta\r\n\r\nderault: 1\r\nNovelAI\'s default: 31337");
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(24, 379);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(25, 12);
			this.label11.TabIndex = 55;
			this.label11.Text = "Clip";
			this.toolTip.SetToolTip(this.label11, "Clip skip\r\n\r\ndefault: 1\r\nNovelAI\'s default: 2");
			// 
			// numericUpDownSubseed
			// 
			this.numericUpDownSubseed.Location = new System.Drawing.Point(20, 47);
			this.numericUpDownSubseed.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
			this.numericUpDownSubseed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
			this.numericUpDownSubseed.Name = "numericUpDownSubseed";
			this.numericUpDownSubseed.Size = new System.Drawing.Size(120, 19);
			this.numericUpDownSubseed.TabIndex = 56;
			this.toolTip.SetToolTip(this.numericUpDownSubseed, "Subseed. -1 mean \'random seed\'. default value is -1");
			this.numericUpDownSubseed.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
			// 
			// trackBarSubseedStrength100
			// 
			this.trackBarSubseedStrength100.AutoSize = false;
			this.trackBarSubseedStrength100.LargeChange = 1;
			this.trackBarSubseedStrength100.Location = new System.Drawing.Point(20, 18);
			this.trackBarSubseedStrength100.Maximum = 100;
			this.trackBarSubseedStrength100.Name = "trackBarSubseedStrength100";
			this.trackBarSubseedStrength100.Size = new System.Drawing.Size(208, 23);
			this.trackBarSubseedStrength100.TabIndex = 58;
			this.trackBarSubseedStrength100.TickStyle = System.Windows.Forms.TickStyle.None;
			this.toolTip.SetToolTip(this.trackBarSubseedStrength100, "Subseed strength\r\ndefault: 0.  (means disable subseed)");
			// 
			// buttonReadInfoText
			// 
			this.buttonReadInfoText.Location = new System.Drawing.Point(436, 9);
			this.buttonReadInfoText.Name = "buttonReadInfoText";
			this.buttonReadInfoText.Size = new System.Drawing.Size(37, 23);
			this.buttonReadInfoText.TabIndex = 63;
			this.buttonReadInfoText.Text = "/";
			this.toolTip.SetToolTip(this.buttonReadInfoText, "Import automatic1111 infotext from prompt input box\r\n\r\ninfotext is such as\r\nphoto" +
        " of a girl\r\nNegative prompt: bad anatomy\r\nSteps: 20, Sampler: Euler a, CFG scale" +
        ": 7, Seed: 1574434520, Size: 512x512");
			this.buttonReadInfoText.UseVisualStyleBackColor = true;
			this.buttonReadInfoText.Click += new System.EventHandler(this.buttonReadInfoText_Click);
			// 
			// comboBoxSubseedH
			// 
			this.comboBoxSubseedH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxSubseedH.FormattingEnabled = true;
			this.comboBoxSubseedH.Location = new System.Drawing.Point(223, 46);
			this.comboBoxSubseedH.Name = "comboBoxSubseedH";
			this.comboBoxSubseedH.Size = new System.Drawing.Size(47, 20);
			this.comboBoxSubseedH.TabIndex = 65;
			this.toolTip.SetToolTip(this.comboBoxSubseedH, "Seed resize from H");
			// 
			// comboBoxSubseedW
			// 
			this.comboBoxSubseedW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxSubseedW.FormattingEnabled = true;
			this.comboBoxSubseedW.Location = new System.Drawing.Point(162, 46);
			this.comboBoxSubseedW.Name = "comboBoxSubseedW";
			this.comboBoxSubseedW.Size = new System.Drawing.Size(47, 20);
			this.comboBoxSubseedW.TabIndex = 64;
			this.toolTip.SetToolTip(this.comboBoxSubseedW, "Seed resize from W");
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.comboBoxHeight);
			this.groupBox1.Controls.Add(this.comboBoxWidth);
			this.groupBox1.Controls.Add(this.buttonSelectionFit);
			this.groupBox1.Controls.Add(this.buttonSelectionMemory);
			this.groupBox1.Controls.Add(this.buttonSelectionRestore);
			this.groupBox1.Location = new System.Drawing.Point(643, 22);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(127, 244);
			this.groupBox1.TabIndex = 21;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Selection";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(31, 789);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(164, 12);
			this.label1.TabIndex = 23;
			this.label1.Text = "Transparent color in photoshop";
			// 
			// labelCfgScale100
			// 
			this.labelCfgScale100.AutoSize = true;
			this.labelCfgScale100.Location = new System.Drawing.Point(479, 280);
			this.labelCfgScale100.Name = "labelCfgScale100";
			this.labelCfgScale100.Size = new System.Drawing.Size(35, 12);
			this.labelCfgScale100.TabIndex = 25;
			this.labelCfgScale100.Text = "label3";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(23, 221);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 12);
			this.label2.TabIndex = 26;
			this.label2.Text = "Strength";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(23, 245);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(26, 12);
			this.label3.TabIndex = 27;
			this.label3.Text = "Blur";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(23, 271);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(28, 12);
			this.label4.TabIndex = 28;
			this.label4.Text = "CFG";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(146, 298);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(34, 12);
			this.label5.TabIndex = 31;
			this.label5.Text = "Steps";
			// 
			// labelStep
			// 
			this.labelStep.AutoSize = true;
			this.labelStep.Location = new System.Drawing.Point(479, 307);
			this.labelStep.Name = "labelStep";
			this.labelStep.Size = new System.Drawing.Size(35, 12);
			this.labelStep.TabIndex = 30;
			this.labelStep.Text = "label3";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(434, 357);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(30, 12);
			this.label6.TabIndex = 34;
			this.label6.Text = "Seed";
			// 
			// buttonSetting
			// 
			this.buttonSetting.Image = ((System.Drawing.Image)(resources.GetObject("buttonSetting.Image")));
			this.buttonSetting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonSetting.Location = new System.Drawing.Point(698, 778);
			this.buttonSetting.Name = "buttonSetting";
			this.buttonSetting.Size = new System.Drawing.Size(84, 23);
			this.buttonSetting.TabIndex = 35;
			this.buttonSetting.Text = "   Setting";
			this.buttonSetting.UseVisualStyleBackColor = true;
			this.buttonSetting.Click += new System.EventHandler(this.buttonSetting_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(178, 355);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(59, 12);
			this.label7.TabIndex = 38;
			this.label7.Text = "Batch size";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(178, 328);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(67, 12);
			this.label8.TabIndex = 40;
			this.label8.Text = "Batch count";
			// 
			// labelBatchSize
			// 
			this.labelBatchSize.AutoSize = true;
			this.labelBatchSize.Location = new System.Drawing.Point(354, 355);
			this.labelBatchSize.Name = "labelBatchSize";
			this.labelBatchSize.Size = new System.Drawing.Size(35, 12);
			this.labelBatchSize.TabIndex = 41;
			this.labelBatchSize.Text = "label3";
			// 
			// labelBatchCount
			// 
			this.labelBatchCount.AutoSize = true;
			this.labelBatchCount.Location = new System.Drawing.Point(354, 328);
			this.labelBatchCount.Name = "labelBatchCount";
			this.labelBatchCount.Size = new System.Drawing.Size(35, 12);
			this.labelBatchCount.TabIndex = 42;
			this.labelBatchCount.Text = "label3";
			// 
			// buttonAbort
			// 
			this.buttonAbort.Location = new System.Drawing.Point(657, 335);
			this.buttonAbort.Name = "buttonAbort";
			this.buttonAbort.Size = new System.Drawing.Size(54, 23);
			this.buttonAbort.TabIndex = 44;
			this.buttonAbort.Text = "Abort";
			this.buttonAbort.UseVisualStyleBackColor = true;
			this.buttonAbort.Click += new System.EventHandler(this.buttonAbort_Click);
			// 
			// buttonAbortForced
			// 
			this.buttonAbortForced.Location = new System.Drawing.Point(716, 335);
			this.buttonAbortForced.Name = "buttonAbortForced";
			this.buttonAbortForced.Size = new System.Drawing.Size(54, 23);
			this.buttonAbortForced.TabIndex = 45;
			this.buttonAbortForced.Text = "Forced";
			this.buttonAbortForced.UseVisualStyleBackColor = true;
			this.buttonAbortForced.Click += new System.EventHandler(this.buttonAbortForced_Click);
			// 
			// buttonLogWrite
			// 
			this.buttonLogWrite.Location = new System.Drawing.Point(650, 750);
			this.buttonLogWrite.Name = "buttonLogWrite";
			this.buttonLogWrite.Size = new System.Drawing.Size(75, 23);
			this.buttonLogWrite.TabIndex = 47;
			this.buttonLogWrite.Text = "Log";
			this.buttonLogWrite.UseVisualStyleBackColor = true;
			this.buttonLogWrite.Click += new System.EventHandler(this.buttonLogWrite_Click);
			// 
			// checkBoxRestoreFace
			// 
			this.checkBoxRestoreFace.AutoSize = true;
			this.checkBoxRestoreFace.Location = new System.Drawing.Point(25, 329);
			this.checkBoxRestoreFace.Name = "checkBoxRestoreFace";
			this.checkBoxRestoreFace.Size = new System.Drawing.Size(96, 16);
			this.checkBoxRestoreFace.TabIndex = 50;
			this.checkBoxRestoreFace.Text = "Restore faces";
			this.checkBoxRestoreFace.UseVisualStyleBackColor = true;
			// 
			// checkBoxTiling
			// 
			this.checkBoxTiling.AutoSize = true;
			this.checkBoxTiling.Location = new System.Drawing.Point(24, 351);
			this.checkBoxTiling.Name = "checkBoxTiling";
			this.checkBoxTiling.Size = new System.Drawing.Size(52, 16);
			this.checkBoxTiling.TabIndex = 51;
			this.checkBoxTiling.Text = "Tiling";
			this.checkBoxTiling.UseVisualStyleBackColor = true;
			// 
			// numericUpDownENSD
			// 
			this.numericUpDownENSD.Location = new System.Drawing.Point(65, 400);
			this.numericUpDownENSD.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
			this.numericUpDownENSD.Name = "numericUpDownENSD";
			this.numericUpDownENSD.Size = new System.Drawing.Size(85, 19);
			this.numericUpDownENSD.TabIndex = 52;
			// 
			// numericUpDownClipSkip
			// 
			this.numericUpDownClipSkip.Location = new System.Drawing.Point(65, 375);
			this.numericUpDownClipSkip.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDownClipSkip.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDownClipSkip.Name = "numericUpDownClipSkip";
			this.numericUpDownClipSkip.Size = new System.Drawing.Size(85, 19);
			this.numericUpDownClipSkip.TabIndex = 54;
			this.numericUpDownClipSkip.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// labelSubseedStrength
			// 
			this.labelSubseedStrength.AutoSize = true;
			this.labelSubseedStrength.Location = new System.Drawing.Point(234, 25);
			this.labelSubseedStrength.Name = "labelSubseedStrength";
			this.labelSubseedStrength.Size = new System.Drawing.Size(35, 12);
			this.labelSubseedStrength.TabIndex = 59;
			this.labelSubseedStrength.Text = "label1";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.comboBoxSubseedH);
			this.groupBox2.Controls.Add(this.trackBarSubseedStrength100);
			this.groupBox2.Controls.Add(this.comboBoxSubseedW);
			this.groupBox2.Controls.Add(this.labelSubseedStrength);
			this.groupBox2.Controls.Add(this.numericUpDownSubseed);
			this.groupBox2.Location = new System.Drawing.Point(180, 388);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(309, 85);
			this.groupBox2.TabIndex = 62;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Variation (extra)";
			// 
			// buttonNovelAIto1111Conv
			// 
			this.buttonNovelAIto1111Conv.Location = new System.Drawing.Point(371, 9);
			this.buttonNovelAIto1111Conv.Name = "buttonNovelAIto1111Conv";
			this.buttonNovelAIto1111Conv.Size = new System.Drawing.Size(37, 23);
			this.buttonNovelAIto1111Conv.TabIndex = 64;
			this.buttonNovelAIto1111Conv.Text = "N";
			this.toolTip.SetToolTip(this.buttonNovelAIto1111Conv, "Convert NovelAI prompt to Automatic1111 prompt\r\n\r\nex.\r\n{{xxx}}, [yyy]\r\nto\r\n(xxx:1" +
        ".1025), (yyy:0.9524)");
			this.buttonNovelAIto1111Conv.UseVisualStyleBackColor = true;
			this.buttonNovelAIto1111Conv.Click += new System.EventHandler(this.buttonNovelAIto1111Conv_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripProgressBar1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 813);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(794, 22);
			this.statusStrip1.TabIndex = 66;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripProgressBar1
			// 
			this.toolStripProgressBar1.Name = "toolStripProgressBar1";
			this.toolStripProgressBar1.Size = new System.Drawing.Size(400, 16);
			// 
			// toolStripStatusLabel
			// 
			this.toolStripStatusLabel.AutoSize = false;
			this.toolStripStatusLabel.Name = "toolStripStatusLabel";
			this.toolStripStatusLabel.Size = new System.Drawing.Size(200, 17);
			this.toolStripStatusLabel.Text = "toolStripStatusLabel1";
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(794, 835);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.buttonNovelAIto1111Conv);
			this.Controls.Add(this.buttonReadInfoText);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.numericUpDownClipSkip);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.numericUpDownENSD);
			this.Controls.Add(this.checkBoxTiling);
			this.Controls.Add(this.checkBoxRestoreFace);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.textBoxLayerName);
			this.Controls.Add(this.buttonLogWrite);
			this.Controls.Add(this.textBoxLogWrite);
			this.Controls.Add(this.buttonAbortForced);
			this.Controls.Add(this.buttonAbort);
			this.Controls.Add(this.pictureBoxNewVersionExists);
			this.Controls.Add(this.labelBatchCount);
			this.Controls.Add(this.labelBatchSize);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.trackBarBatchCount);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.trackBarBatchSize);
			this.Controls.Add(this.buttonSetting);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.numericUpDownSeed);
			this.Controls.Add(this.comboBoxSampler);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.labelStep);
			this.Controls.Add(this.trackBarStep);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.labelCfgScale100);
			this.Controls.Add(this.trackBarCfgScale100);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonSetTransparentColor);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.textBoxLogMessage);
			this.Controls.Add(this.comboBoxRecentNegativePrompt);
			this.Controls.Add(this.comboBoxRecentPrompt);
			this.Controls.Add(this.checkBoxAutoMask);
			this.Controls.Add(this.buttonSetMask);
			this.Controls.Add(this.buttonClearMask);
			this.Controls.Add(this.pictureBoxMask);
			this.Controls.Add(this.checkBoxInpainting_mask_invert);
			this.Controls.Add(this.labelMaskBlur);
			this.Controls.Add(this.labelNoiseScale100);
			this.Controls.Add(this.trackBarMaskBlur);
			this.Controls.Add(this.trackBarNoiseScale100);
			this.Controls.Add(this.textBoxNegativePrompt);
			this.Controls.Add(this.textBoxPrompt);
			this.Controls.Add(this.buttonGenerate);
			this.Name = "FormMain";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.trackBarNoiseScale100)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarMaskBlur)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxMask)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarCfgScale100)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarStep)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeed)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarBatchSize)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarBatchCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxNewVersionExists)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSubseed)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarSubseedStrength100)).EndInit();
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownENSD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownClipSkip)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonGenerate;
		private System.Windows.Forms.TextBox textBoxPrompt;
		private System.Windows.Forms.TextBox textBoxNegativePrompt;
		private System.Windows.Forms.TrackBar trackBarNoiseScale100;
		private System.Windows.Forms.TrackBar trackBarMaskBlur;
		private System.Windows.Forms.Label labelNoiseScale100;
		private System.Windows.Forms.Label labelMaskBlur;
		private System.Windows.Forms.Button buttonSelectionFit;
		private System.Windows.Forms.CheckBox checkBoxInpainting_mask_invert;
		private System.Windows.Forms.Button buttonSelectionMemory;
		private System.Windows.Forms.Button buttonSelectionRestore;
		private System.Windows.Forms.PictureBox pictureBoxMask;
		private System.Windows.Forms.Button buttonClearMask;
		private System.Windows.Forms.Button buttonSetMask;
		private System.Windows.Forms.CheckBox checkBoxAutoMask;
		private System.Windows.Forms.ComboBox comboBoxRecentPrompt;
		private System.Windows.Forms.ComboBox comboBoxRecentNegativePrompt;
		public System.Windows.Forms.TextBox textBoxLogMessage;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox comboBoxHeight;
		private System.Windows.Forms.ComboBox comboBoxWidth;
		private System.Windows.Forms.Button buttonSetTransparentColor;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labelCfgScale100;
		private System.Windows.Forms.TrackBar trackBarCfgScale100;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label labelStep;
		private System.Windows.Forms.TrackBar trackBarStep;
		private System.Windows.Forms.ComboBox comboBoxSampler;
		private System.Windows.Forms.NumericUpDown numericUpDownSeed;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button buttonSetting;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TrackBar trackBarBatchSize;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TrackBar trackBarBatchCount;
		private System.Windows.Forms.Label labelBatchSize;
		private System.Windows.Forms.Label labelBatchCount;
		private System.Windows.Forms.PictureBox pictureBoxNewVersionExists;
		private System.Windows.Forms.Button buttonAbort;
		private System.Windows.Forms.Button buttonAbortForced;
		private System.Windows.Forms.TextBox textBoxLogWrite;
		private System.Windows.Forms.Button buttonLogWrite;
		private System.Windows.Forms.TextBox textBoxLayerName;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.CheckBox checkBoxRestoreFace;
		private System.Windows.Forms.CheckBox checkBoxTiling;
		private System.Windows.Forms.NumericUpDown numericUpDownENSD;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.NumericUpDown numericUpDownClipSkip;
		private System.Windows.Forms.NumericUpDown numericUpDownSubseed;
		private System.Windows.Forms.Label labelSubseedStrength;
		private System.Windows.Forms.TrackBar trackBarSubseedStrength100;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button buttonReadInfoText;
		private System.Windows.Forms.ComboBox comboBoxSubseedH;
		private System.Windows.Forms.ComboBox comboBoxSubseedW;
		private System.Windows.Forms.Button buttonNovelAIto1111Conv;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
		private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
	}
}

