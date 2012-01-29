namespace GinnayGUI
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.startListenerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stopGinnayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.proxyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.refreshProxiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.reValidateProxiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pACToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.generatePACFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setIEPACToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
			this.label3 = new System.Windows.Forms.Label();
			this.lblProxyColor = new System.Windows.Forms.Label();
			this.lblProxyAddress = new System.Windows.Forms.Label();
			this.lblProxySource = new System.Windows.Forms.Label();
			this.lblProxyLatency = new System.Windows.Forms.Label();
			this.button7 = new System.Windows.Forms.Button();
			this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.txtPort = new System.Windows.Forms.TextBox();
			this.lblStatus = new System.Windows.Forms.Label();
			this.flowLayoutPanel8 = new System.Windows.Forms.FlowLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.btnGenerate = new System.Windows.Forms.Button();
			this.btnSetIEPac = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.txtLog = new System.Windows.Forms.TextBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnDownloadProxies = new System.Windows.Forms.Button();
			this.btnStopDownloadProxies = new System.Windows.Forms.Button();
			this.btnValidateProxies = new System.Windows.Forms.Button();
			this.btnStopValidateProxies = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.dgProxyList = new System.Windows.Forms.DataGridView();
			this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Latency = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Location = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.label10 = new System.Windows.Forms.Label();
			this.lblProxyStatusWorkingProxy = new System.Windows.Forms.Label();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.progressBarProxyManager = new System.Windows.Forms.ProgressBar();
			this.lblProxyManagerStatus = new System.Windows.Forms.Label();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.dgURLPattern = new System.Windows.Forms.DataGridView();
			this.CheckBoxes = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.NeedValidation = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.Classification = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.URLPattern = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.URLPatternNecessaryKeywords = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.URLPatternForbiddenKeywords = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
			this.button3 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.dgProxyValidateCondition = new System.Windows.Forms.DataGridView();
			this.TargetURL = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Keywords = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ForbiddenKeywords = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.dgProxyProvider = new System.Windows.Forms.DataGridView();
			this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.button8 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.btnStop = new System.Windows.Forms.Button();
			this.btnStart = new System.Windows.Forms.Button();
			this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
			this.menuStrip1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tableLayoutPanel7.SuspendLayout();
			this.flowLayoutPanel4.SuspendLayout();
			this.flowLayoutPanel3.SuspendLayout();
			this.flowLayoutPanel8.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tableLayoutPanel5.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgProxyList)).BeginInit();
			this.flowLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgURLPattern)).BeginInit();
			this.flowLayoutPanel7.SuspendLayout();
			this.tabPage5.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgProxyValidateCondition)).BeginInit();
			this.flowLayoutPanel6.SuspendLayout();
			this.tabPage4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgProxyProvider)).BeginInit();
			this.flowLayoutPanel5.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.proxyToolStripMenuItem,
            this.pACToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(697, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startListenerToolStripMenuItem,
            this.stopGinnayToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
			this.openToolStripMenuItem.Text = "Server";
			// 
			// startListenerToolStripMenuItem
			// 
			this.startListenerToolStripMenuItem.Name = "startListenerToolStripMenuItem";
			this.startListenerToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
			this.startListenerToolStripMenuItem.Text = "Start Ginnay";
			// 
			// stopGinnayToolStripMenuItem
			// 
			this.stopGinnayToolStripMenuItem.Name = "stopGinnayToolStripMenuItem";
			this.stopGinnayToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
			this.stopGinnayToolStripMenuItem.Text = "Stop Ginnay";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(135, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// proxyToolStripMenuItem
			// 
			this.proxyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshProxiesToolStripMenuItem,
            this.reValidateProxiesToolStripMenuItem});
			this.proxyToolStripMenuItem.Name = "proxyToolStripMenuItem";
			this.proxyToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
			this.proxyToolStripMenuItem.Text = "Proxy";
			// 
			// refreshProxiesToolStripMenuItem
			// 
			this.refreshProxiesToolStripMenuItem.Name = "refreshProxiesToolStripMenuItem";
			this.refreshProxiesToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.refreshProxiesToolStripMenuItem.Text = "Re-Download Proxies";
			// 
			// reValidateProxiesToolStripMenuItem
			// 
			this.reValidateProxiesToolStripMenuItem.Name = "reValidateProxiesToolStripMenuItem";
			this.reValidateProxiesToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.reValidateProxiesToolStripMenuItem.Text = "Re-Validate Proxies";
			// 
			// pACToolStripMenuItem
			// 
			this.pACToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generatePACFileToolStripMenuItem,
            this.setIEPACToolStripMenuItem});
			this.pACToolStripMenuItem.Name = "pACToolStripMenuItem";
			this.pACToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
			this.pACToolStripMenuItem.Text = "PAC";
			// 
			// generatePACFileToolStripMenuItem
			// 
			this.generatePACFileToolStripMenuItem.Name = "generatePACFileToolStripMenuItem";
			this.generatePACFileToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.generatePACFileToolStripMenuItem.Text = "Generate PAC File";
			this.generatePACFileToolStripMenuItem.Click += new System.EventHandler(this.generatePACFileToolStripMenuItem_Click);
			// 
			// setIEPACToolStripMenuItem
			// 
			this.setIEPACToolStripMenuItem.Name = "setIEPACToolStripMenuItem";
			this.setIEPACToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.setIEPACToolStripMenuItem.Text = "Set IE PAC";
			this.setIEPACToolStripMenuItem.Click += new System.EventHandler(this.setIEPACToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentToolStripMenuItem,
            this.toolStripSeparator4,
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// contentToolStripMenuItem
			// 
			this.contentToolStripMenuItem.Name = "contentToolStripMenuItem";
			this.contentToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.contentToolStripMenuItem.Text = "Content";
			this.contentToolStripMenuItem.Click += new System.EventHandler(this.contentToolStripMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(149, 6);
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.aboutToolStripMenuItem.Text = "About Ginnay";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage5);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 24);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(697, 367);
			this.tabControl1.TabIndex = 1;
			this.tabControl1.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Deselecting);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.panel1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(689, 341);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Ginnay";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.tableLayoutPanel1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(683, 335);
			this.panel1.TabIndex = 0;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel5, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(683, 335);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.AutoSize = true;
			this.groupBox1.Controls.Add(this.tableLayoutPanel7);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(3, 38);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(677, 121);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Ginnay Status";
			// 
			// tableLayoutPanel7
			// 
			this.tableLayoutPanel7.AutoSize = true;
			this.tableLayoutPanel7.ColumnCount = 1;
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel7.Controls.Add(this.flowLayoutPanel4, 0, 1);
			this.tableLayoutPanel7.Controls.Add(this.flowLayoutPanel3, 0, 0);
			this.tableLayoutPanel7.Controls.Add(this.flowLayoutPanel8, 0, 2);
			this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 16);
			this.tableLayoutPanel7.Name = "tableLayoutPanel7";
			this.tableLayoutPanel7.RowCount = 3;
			this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel7.Size = new System.Drawing.Size(671, 102);
			this.tableLayoutPanel7.TabIndex = 0;
			// 
			// flowLayoutPanel4
			// 
			this.flowLayoutPanel4.AutoSize = true;
			this.flowLayoutPanel4.Controls.Add(this.label3);
			this.flowLayoutPanel4.Controls.Add(this.lblProxyColor);
			this.flowLayoutPanel4.Controls.Add(this.lblProxyAddress);
			this.flowLayoutPanel4.Controls.Add(this.lblProxySource);
			this.flowLayoutPanel4.Controls.Add(this.lblProxyLatency);
			this.flowLayoutPanel4.Controls.Add(this.button7);
			this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel4.Location = new System.Drawing.Point(3, 35);
			this.flowLayoutPanel4.Name = "flowLayoutPanel4";
			this.flowLayoutPanel4.Size = new System.Drawing.Size(665, 29);
			this.flowLayoutPanel4.TabIndex = 12;
			// 
			// label3
			// 
			this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(79, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Ginnay Status: ";
			// 
			// lblProxyColor
			// 
			this.lblProxyColor.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblProxyColor.BackColor = System.Drawing.Color.Red;
			this.lblProxyColor.Location = new System.Drawing.Point(88, 9);
			this.lblProxyColor.Name = "lblProxyColor";
			this.lblProxyColor.Size = new System.Drawing.Size(10, 10);
			this.lblProxyColor.TabIndex = 1;
			// 
			// lblProxyAddress
			// 
			this.lblProxyAddress.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblProxyAddress.AutoSize = true;
			this.lblProxyAddress.Location = new System.Drawing.Point(104, 8);
			this.lblProxyAddress.Name = "lblProxyAddress";
			this.lblProxyAddress.Size = new System.Drawing.Size(139, 13);
			this.lblProxyAddress.TabIndex = 2;
			this.lblProxyAddress.Text = "Proxy currently not available";
			// 
			// lblProxySource
			// 
			this.lblProxySource.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblProxySource.AutoSize = true;
			this.lblProxySource.Location = new System.Drawing.Point(249, 8);
			this.lblProxySource.Name = "lblProxySource";
			this.lblProxySource.Size = new System.Drawing.Size(27, 13);
			this.lblProxySource.TabIndex = 3;
			this.lblProxySource.Text = "N/A";
			// 
			// lblProxyLatency
			// 
			this.lblProxyLatency.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblProxyLatency.AutoSize = true;
			this.lblProxyLatency.Location = new System.Drawing.Point(282, 8);
			this.lblProxyLatency.Name = "lblProxyLatency";
			this.lblProxyLatency.Size = new System.Drawing.Size(27, 13);
			this.lblProxyLatency.TabIndex = 4;
			this.lblProxyLatency.Text = "N/A";
			// 
			// button7
			// 
			this.button7.Location = new System.Drawing.Point(315, 3);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(101, 23);
			this.button7.TabIndex = 9;
			this.button7.Text = "Change Proxy";
			this.button7.UseVisualStyleBackColor = true;
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// flowLayoutPanel3
			// 
			this.flowLayoutPanel3.AutoSize = true;
			this.flowLayoutPanel3.Controls.Add(this.label2);
			this.flowLayoutPanel3.Controls.Add(this.txtPort);
			this.flowLayoutPanel3.Controls.Add(this.lblStatus);
			this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel3.Name = "flowLayoutPanel3";
			this.flowLayoutPanel3.Size = new System.Drawing.Size(665, 26);
			this.flowLayoutPanel3.TabIndex = 10;
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 6);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(71, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Listen to port:";
			// 
			// txtPort
			// 
			this.txtPort.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.txtPort.Location = new System.Drawing.Point(80, 3);
			this.txtPort.MaxLength = 5;
			this.txtPort.Name = "txtPort";
			this.txtPort.Size = new System.Drawing.Size(44, 20);
			this.txtPort.TabIndex = 1;
			this.txtPort.Text = "9297";
			// 
			// lblStatus
			// 
			this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblStatus.AutoSize = true;
			this.lblStatus.Location = new System.Drawing.Point(130, 6);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(109, 13);
			this.lblStatus.TabIndex = 2;
			this.lblStatus.Text = "Ginnay is not running.";
			// 
			// flowLayoutPanel8
			// 
			this.flowLayoutPanel8.AutoSize = true;
			this.flowLayoutPanel8.Controls.Add(this.label1);
			this.flowLayoutPanel8.Controls.Add(this.textBox1);
			this.flowLayoutPanel8.Controls.Add(this.btnGenerate);
			this.flowLayoutPanel8.Controls.Add(this.btnSetIEPac);
			this.flowLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel8.Location = new System.Drawing.Point(3, 70);
			this.flowLayoutPanel8.Name = "flowLayoutPanel8";
			this.flowLayoutPanel8.Size = new System.Drawing.Size(665, 29);
			this.flowLayoutPanel8.TabIndex = 13;
			this.flowLayoutPanel8.WrapContents = false;
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(75, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "PAC File Path:";
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(84, 4);
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(373, 20);
			this.textBox1.TabIndex = 1;
			// 
			// btnGenerate
			// 
			this.btnGenerate.Location = new System.Drawing.Point(463, 3);
			this.btnGenerate.Name = "btnGenerate";
			this.btnGenerate.Size = new System.Drawing.Size(112, 23);
			this.btnGenerate.TabIndex = 3;
			this.btnGenerate.Text = "Generate PAC File";
			this.btnGenerate.UseVisualStyleBackColor = true;
			this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
			// 
			// btnSetIEPac
			// 
			this.btnSetIEPac.Location = new System.Drawing.Point(581, 3);
			this.btnSetIEPac.Name = "btnSetIEPac";
			this.btnSetIEPac.Size = new System.Drawing.Size(75, 23);
			this.btnSetIEPac.TabIndex = 2;
			this.btnSetIEPac.Text = "Set IE PAC";
			this.btnSetIEPac.UseVisualStyleBackColor = true;
			this.btnSetIEPac.Click += new System.EventHandler(this.btnSetIEPac_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.txtLog);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(3, 165);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(677, 167);
			this.groupBox2.TabIndex = 10;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Log Messages";
			// 
			// txtLog
			// 
			this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtLog.Location = new System.Drawing.Point(3, 16);
			this.txtLog.MaxLength = 50;
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.ReadOnly = true;
			this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtLog.Size = new System.Drawing.Size(671, 148);
			this.txtLog.TabIndex = 0;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.tableLayoutPanel5);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(689, 341);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Proxies Status";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel5
			// 
			this.tableLayoutPanel5.ColumnCount = 1;
			this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel5.Controls.Add(this.flowLayoutPanel1, 0, 0);
			this.tableLayoutPanel5.Controls.Add(this.dgProxyList, 0, 1);
			this.tableLayoutPanel5.Controls.Add(this.flowLayoutPanel2, 0, 2);
			this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel2, 0, 3);
			this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel5.Name = "tableLayoutPanel5";
			this.tableLayoutPanel5.RowCount = 4;
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel5.Size = new System.Drawing.Size(683, 335);
			this.tableLayoutPanel5.TabIndex = 0;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.Controls.Add(this.btnDownloadProxies);
			this.flowLayoutPanel1.Controls.Add(this.btnStopDownloadProxies);
			this.flowLayoutPanel1.Controls.Add(this.btnValidateProxies);
			this.flowLayoutPanel1.Controls.Add(this.btnStopValidateProxies);
			this.flowLayoutPanel1.Controls.Add(this.button6);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(677, 29);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// btnDownloadProxies
			// 
			this.btnDownloadProxies.Location = new System.Drawing.Point(3, 3);
			this.btnDownloadProxies.Name = "btnDownloadProxies";
			this.btnDownloadProxies.Size = new System.Drawing.Size(75, 23);
			this.btnDownloadProxies.TabIndex = 4;
			this.btnDownloadProxies.Text = "Download";
			this.btnDownloadProxies.UseVisualStyleBackColor = true;
			this.btnDownloadProxies.Click += new System.EventHandler(this.button1_Click);
			// 
			// btnStopDownloadProxies
			// 
			this.btnStopDownloadProxies.Location = new System.Drawing.Point(84, 3);
			this.btnStopDownloadProxies.Name = "btnStopDownloadProxies";
			this.btnStopDownloadProxies.Size = new System.Drawing.Size(93, 23);
			this.btnStopDownloadProxies.TabIndex = 5;
			this.btnStopDownloadProxies.Text = "Stop Download";
			this.btnStopDownloadProxies.UseVisualStyleBackColor = true;
			this.btnStopDownloadProxies.Click += new System.EventHandler(this.btnStopDownloadProxies_Click);
			// 
			// btnValidateProxies
			// 
			this.btnValidateProxies.Location = new System.Drawing.Point(183, 3);
			this.btnValidateProxies.Name = "btnValidateProxies";
			this.btnValidateProxies.Size = new System.Drawing.Size(75, 23);
			this.btnValidateProxies.TabIndex = 6;
			this.btnValidateProxies.Text = "Validate";
			this.btnValidateProxies.UseVisualStyleBackColor = true;
			this.btnValidateProxies.Click += new System.EventHandler(this.btnValidateProxies_Click);
			// 
			// btnStopValidateProxies
			// 
			this.btnStopValidateProxies.Location = new System.Drawing.Point(264, 3);
			this.btnStopValidateProxies.Name = "btnStopValidateProxies";
			this.btnStopValidateProxies.Size = new System.Drawing.Size(90, 23);
			this.btnStopValidateProxies.TabIndex = 7;
			this.btnStopValidateProxies.Text = "Stop Validate";
			this.btnStopValidateProxies.UseVisualStyleBackColor = true;
			this.btnStopValidateProxies.Click += new System.EventHandler(this.btnStopValidateProxies_Click);
			// 
			// button6
			// 
			this.button6.Location = new System.Drawing.Point(360, 3);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(101, 23);
			this.button6.TabIndex = 8;
			this.button6.Text = "Change Proxy";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// dgProxyList
			// 
			this.dgProxyList.AllowUserToAddRows = false;
			this.dgProxyList.AllowUserToDeleteRows = false;
			this.dgProxyList.AllowUserToOrderColumns = true;
			this.dgProxyList.AllowUserToResizeRows = false;
			this.dgProxyList.BorderStyle = System.Windows.Forms.BorderStyle.None;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgProxyList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dgProxyList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgProxyList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Address,
            this.Latency,
            this.Location});
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgProxyList.DefaultCellStyle = dataGridViewCellStyle2;
			this.dgProxyList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgProxyList.Location = new System.Drawing.Point(3, 38);
			this.dgProxyList.Name = "dgProxyList";
			this.dgProxyList.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgProxyList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dgProxyList.RowHeadersVisible = false;
			this.dgProxyList.Size = new System.Drawing.Size(677, 251);
			this.dgProxyList.TabIndex = 1;
			// 
			// Address
			// 
			this.Address.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Address.DataPropertyName = "Address";
			this.Address.FillWeight = 200F;
			this.Address.HeaderText = "Address";
			this.Address.MinimumWidth = 100;
			this.Address.Name = "Address";
			this.Address.ReadOnly = true;
			// 
			// Latency
			// 
			this.Latency.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Latency.DataPropertyName = "Latency";
			this.Latency.HeaderText = "Latency";
			this.Latency.MinimumWidth = 100;
			this.Latency.Name = "Latency";
			this.Latency.ReadOnly = true;
			// 
			// Location
			// 
			this.Location.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Location.DataPropertyName = "Location";
			this.Location.FillWeight = 250F;
			this.Location.HeaderText = "Location";
			this.Location.MinimumWidth = 100;
			this.Location.Name = "Location";
			this.Location.ReadOnly = true;
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.AutoSize = true;
			this.flowLayoutPanel2.Controls.Add(this.label10);
			this.flowLayoutPanel2.Controls.Add(this.lblProxyStatusWorkingProxy);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 295);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(677, 13);
			this.flowLayoutPanel2.TabIndex = 2;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(3, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(112, 13);
			this.label10.TabIndex = 0;
			this.label10.Text = "Current working proxy:";
			// 
			// lblProxyStatusWorkingProxy
			// 
			this.lblProxyStatusWorkingProxy.AutoSize = true;
			this.lblProxyStatusWorkingProxy.Location = new System.Drawing.Point(121, 0);
			this.lblProxyStatusWorkingProxy.Name = "lblProxyStatusWorkingProxy";
			this.lblProxyStatusWorkingProxy.Size = new System.Drawing.Size(33, 13);
			this.lblProxyStatusWorkingProxy.TabIndex = 1;
			this.lblProxyStatusWorkingProxy.Text = "None";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.AutoSize = true;
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Controls.Add(this.progressBarProxyManager, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.lblProxyManagerStatus, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 314);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(677, 18);
			this.tableLayoutPanel2.TabIndex = 3;
			// 
			// progressBarProxyManager
			// 
			this.progressBarProxyManager.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBarProxyManager.Location = new System.Drawing.Point(341, 3);
			this.progressBarProxyManager.Name = "progressBarProxyManager";
			this.progressBarProxyManager.Size = new System.Drawing.Size(333, 12);
			this.progressBarProxyManager.Step = 1;
			this.progressBarProxyManager.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progressBarProxyManager.TabIndex = 2;
			// 
			// lblProxyManagerStatus
			// 
			this.lblProxyManagerStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.lblProxyManagerStatus.AutoSize = true;
			this.lblProxyManagerStatus.Location = new System.Drawing.Point(3, 2);
			this.lblProxyManagerStatus.Name = "lblProxyManagerStatus";
			this.lblProxyManagerStatus.Size = new System.Drawing.Size(332, 13);
			this.lblProxyManagerStatus.TabIndex = 3;
			this.lblProxyManagerStatus.Text = "Ready.";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.tableLayoutPanel4);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(689, 341);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "URL Patterns";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.ColumnCount = 1;
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.Controls.Add(this.dgURLPattern, 0, 1);
			this.tableLayoutPanel4.Controls.Add(this.flowLayoutPanel7, 0, 0);
			this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 2;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.Size = new System.Drawing.Size(683, 335);
			this.tableLayoutPanel4.TabIndex = 0;
			// 
			// dgURLPattern
			// 
			this.dgURLPattern.AllowUserToOrderColumns = true;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgURLPattern.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.dgURLPattern.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgURLPattern.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CheckBoxes,
            this.NeedValidation,
            this.Classification,
            this.URLPattern,
            this.URLPatternNecessaryKeywords,
            this.URLPatternForbiddenKeywords});
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgURLPattern.DefaultCellStyle = dataGridViewCellStyle5;
			this.dgURLPattern.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgURLPattern.Location = new System.Drawing.Point(3, 38);
			this.dgURLPattern.Name = "dgURLPattern";
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgURLPattern.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.dgURLPattern.RowHeadersVisible = false;
			this.dgURLPattern.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgURLPattern.Size = new System.Drawing.Size(677, 294);
			this.dgURLPattern.TabIndex = 1;
			this.dgURLPattern.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgURLPattern_CellEndEdit);
			this.dgURLPattern.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgURLPattern_UserDeletedRow);
			// 
			// CheckBoxes
			// 
			this.CheckBoxes.DataPropertyName = "Enabled";
			this.CheckBoxes.FillWeight = 60F;
			this.CheckBoxes.Frozen = true;
			this.CheckBoxes.HeaderText = "Enabled";
			this.CheckBoxes.MinimumWidth = 60;
			this.CheckBoxes.Name = "CheckBoxes";
			this.CheckBoxes.Width = 60;
			// 
			// NeedValidation
			// 
			this.NeedValidation.DataPropertyName = "NeedValidation";
			this.NeedValidation.FillWeight = 90F;
			this.NeedValidation.Frozen = true;
			this.NeedValidation.HeaderText = "Need Validation";
			this.NeedValidation.MinimumWidth = 90;
			this.NeedValidation.Name = "NeedValidation";
			this.NeedValidation.Width = 90;
			// 
			// Classification
			// 
			this.Classification.DataPropertyName = "Classification";
			this.Classification.FillWeight = 120F;
			this.Classification.HeaderText = "Classification";
			this.Classification.MinimumWidth = 30;
			this.Classification.Name = "Classification";
			this.Classification.Width = 120;
			// 
			// URLPattern
			// 
			this.URLPattern.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.URLPattern.DataPropertyName = "URLPattern";
			this.URLPattern.FillWeight = 300F;
			this.URLPattern.HeaderText = "URLPattern";
			this.URLPattern.MinimumWidth = 50;
			this.URLPattern.Name = "URLPattern";
			// 
			// URLPatternNecessaryKeywords
			// 
			this.URLPatternNecessaryKeywords.DataPropertyName = "NecessaryKeywords";
			this.URLPatternNecessaryKeywords.HeaderText = "NecessaryKeywords";
			this.URLPatternNecessaryKeywords.Name = "URLPatternNecessaryKeywords";
			// 
			// URLPatternForbiddenKeywords
			// 
			this.URLPatternForbiddenKeywords.DataPropertyName = "ForbiddenKeywords";
			this.URLPatternForbiddenKeywords.HeaderText = "ForbiddenKeywords";
			this.URLPatternForbiddenKeywords.Name = "URLPatternForbiddenKeywords";
			// 
			// flowLayoutPanel7
			// 
			this.flowLayoutPanel7.AutoSize = true;
			this.flowLayoutPanel7.Controls.Add(this.button3);
			this.flowLayoutPanel7.Controls.Add(this.button5);
			this.flowLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel7.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel7.Name = "flowLayoutPanel7";
			this.flowLayoutPanel7.Size = new System.Drawing.Size(677, 29);
			this.flowLayoutPanel7.TabIndex = 2;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(3, 3);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 0;
			this.button3.Text = "Apply";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(84, 3);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(75, 23);
			this.button5.TabIndex = 1;
			this.button5.Text = "Reset";
			this.button5.UseVisualStyleBackColor = true;
			// 
			// tabPage5
			// 
			this.tabPage5.Controls.Add(this.tableLayoutPanel3);
			this.tabPage5.Location = new System.Drawing.Point(4, 22);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage5.Size = new System.Drawing.Size(689, 341);
			this.tabPage5.TabIndex = 4;
			this.tabPage5.Text = "Validator Settings";
			this.tabPage5.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Controls.Add(this.dgProxyValidateCondition, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel6, 0, 0);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 2;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(683, 335);
			this.tableLayoutPanel3.TabIndex = 0;
			// 
			// dgProxyValidateCondition
			// 
			this.dgProxyValidateCondition.AllowUserToOrderColumns = true;
			this.dgProxyValidateCondition.BorderStyle = System.Windows.Forms.BorderStyle.None;
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgProxyValidateCondition.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
			this.dgProxyValidateCondition.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgProxyValidateCondition.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TargetURL,
            this.Keywords,
            this.ForbiddenKeywords});
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgProxyValidateCondition.DefaultCellStyle = dataGridViewCellStyle8;
			this.dgProxyValidateCondition.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgProxyValidateCondition.Location = new System.Drawing.Point(3, 38);
			this.dgProxyValidateCondition.Name = "dgProxyValidateCondition";
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgProxyValidateCondition.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
			this.dgProxyValidateCondition.RowHeadersVisible = false;
			this.dgProxyValidateCondition.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgProxyValidateCondition.Size = new System.Drawing.Size(677, 294);
			this.dgProxyValidateCondition.TabIndex = 1;
			this.dgProxyValidateCondition.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgProxyValidateCondition_CellEndEdit);
			this.dgProxyValidateCondition.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgProxyValidateCondition_UserDeletedRow);
			// 
			// TargetURL
			// 
			this.TargetURL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.TargetURL.DataPropertyName = "TargetURL";
			this.TargetURL.FillWeight = 300F;
			this.TargetURL.HeaderText = "Target URL";
			this.TargetURL.MinimumWidth = 100;
			this.TargetURL.Name = "TargetURL";
			// 
			// Keywords
			// 
			this.Keywords.DataPropertyName = "Keywords";
			this.Keywords.FillWeight = 150F;
			this.Keywords.HeaderText = "Keywords";
			this.Keywords.MinimumWidth = 50;
			this.Keywords.Name = "Keywords";
			this.Keywords.Width = 150;
			// 
			// ForbiddenKeywords
			// 
			this.ForbiddenKeywords.DataPropertyName = "ForbiddenKeywords";
			this.ForbiddenKeywords.FillWeight = 150F;
			this.ForbiddenKeywords.HeaderText = "Forbidden Keywords";
			this.ForbiddenKeywords.MinimumWidth = 50;
			this.ForbiddenKeywords.Name = "ForbiddenKeywords";
			this.ForbiddenKeywords.Width = 150;
			// 
			// flowLayoutPanel6
			// 
			this.flowLayoutPanel6.AutoSize = true;
			this.flowLayoutPanel6.Controls.Add(this.button1);
			this.flowLayoutPanel6.Controls.Add(this.button2);
			this.flowLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel6.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel6.Name = "flowLayoutPanel6";
			this.flowLayoutPanel6.Size = new System.Drawing.Size(677, 29);
			this.flowLayoutPanel6.TabIndex = 2;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(3, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = "Apply";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(84, 3);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 4;
			this.button2.Text = "Reset";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.dgProxyProvider);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(689, 341);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Proxy Sources";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// dgProxyProvider
			// 
			this.dgProxyProvider.AllowUserToOrderColumns = true;
			dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgProxyProvider.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
			this.dgProxyProvider.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgProxyProvider.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn1});
			dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgProxyProvider.DefaultCellStyle = dataGridViewCellStyle11;
			this.dgProxyProvider.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgProxyProvider.Location = new System.Drawing.Point(3, 3);
			this.dgProxyProvider.Name = "dgProxyProvider";
			this.dgProxyProvider.ReadOnly = true;
			dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgProxyProvider.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
			this.dgProxyProvider.RowHeadersVisible = false;
			this.dgProxyProvider.Size = new System.Drawing.Size(683, 335);
			this.dgProxyProvider.TabIndex = 1;
			// 
			// dataGridViewCheckBoxColumn1
			// 
			this.dataGridViewCheckBoxColumn1.DataPropertyName = "Enabled";
			this.dataGridViewCheckBoxColumn1.FillWeight = 60F;
			this.dataGridViewCheckBoxColumn1.Frozen = true;
			this.dataGridViewCheckBoxColumn1.HeaderText = "Enabled";
			this.dataGridViewCheckBoxColumn1.MinimumWidth = 60;
			this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
			this.dataGridViewCheckBoxColumn1.ReadOnly = true;
			this.dataGridViewCheckBoxColumn1.Width = 60;
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn1.DataPropertyName = "Type";
			this.dataGridViewTextBoxColumn1.FillWeight = 150F;
			this.dataGridViewTextBoxColumn1.HeaderText = "Type";
			this.dataGridViewTextBoxColumn1.MinimumWidth = 30;
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "Ginnay";
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
			// 
			// button8
			// 
			this.button8.Location = new System.Drawing.Point(246, 3);
			this.button8.Name = "button8";
			this.button8.Size = new System.Drawing.Size(201, 23);
			this.button8.TabIndex = 3;
			this.button8.Text = "Tell me the website you cannot visit";
			this.button8.UseVisualStyleBackColor = true;
			this.button8.Click += new System.EventHandler(this.button8_Click);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(165, 3);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(75, 23);
			this.button4.TabIndex = 2;
			this.button4.Text = "Exit";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// btnStop
			// 
			this.btnStop.Location = new System.Drawing.Point(84, 3);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(75, 23);
			this.btnStop.TabIndex = 1;
			this.btnStop.Text = "Stop";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(3, 3);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(75, 23);
			this.btnStart.TabIndex = 0;
			this.btnStart.Text = "Start Fetching Proxies";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// flowLayoutPanel5
			// 
			this.flowLayoutPanel5.AutoSize = true;
			this.flowLayoutPanel5.Controls.Add(this.btnStart);
			this.flowLayoutPanel5.Controls.Add(this.btnStop);
			this.flowLayoutPanel5.Controls.Add(this.button4);
			this.flowLayoutPanel5.Controls.Add(this.button8);
			this.flowLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel5.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel5.Name = "flowLayoutPanel5";
			this.flowLayoutPanel5.Size = new System.Drawing.Size(677, 29);
			this.flowLayoutPanel5.TabIndex = 8;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
			this.ClientSize = new System.Drawing.Size(697, 391);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(713, 429);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Ginnay ";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.tableLayoutPanel7.ResumeLayout(false);
			this.tableLayoutPanel7.PerformLayout();
			this.flowLayoutPanel4.ResumeLayout(false);
			this.flowLayoutPanel4.PerformLayout();
			this.flowLayoutPanel3.ResumeLayout(false);
			this.flowLayoutPanel3.PerformLayout();
			this.flowLayoutPanel8.ResumeLayout(false);
			this.flowLayoutPanel8.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tableLayoutPanel5.ResumeLayout(false);
			this.tableLayoutPanel5.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgProxyList)).EndInit();
			this.flowLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel2.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgURLPattern)).EndInit();
			this.flowLayoutPanel7.ResumeLayout(false);
			this.tabPage5.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgProxyValidateCondition)).EndInit();
			this.flowLayoutPanel6.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgProxyProvider)).EndInit();
			this.flowLayoutPanel5.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem startListenerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem stopGinnayToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem proxyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem refreshProxiesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem reValidateProxiesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pACToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem setIEPACToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem contentToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.DataGridView dgProxyList;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.Label lblProxyStatusWorkingProxy;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.DataGridView dgProxyProvider;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.Button btnDownloadProxies;
		private System.Windows.Forms.Button btnStopDownloadProxies;
		private System.Windows.Forms.ProgressBar progressBarProxyManager;
		private System.Windows.Forms.Button btnValidateProxies;
		private System.Windows.Forms.Button btnStopValidateProxies;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Label lblProxyManagerStatus;
		private System.Windows.Forms.DataGridViewTextBoxColumn Address;
		private System.Windows.Forms.DataGridViewTextBoxColumn Latency;
		private System.Windows.Forms.DataGridViewTextBoxColumn Location;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.DataGridView dgProxyValidateCondition;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.DataGridViewTextBoxColumn TargetURL;
		private System.Windows.Forms.DataGridViewTextBoxColumn Keywords;
		private System.Windows.Forms.DataGridViewTextBoxColumn ForbiddenKeywords;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private System.Windows.Forms.DataGridView dgURLPattern;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel7;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.DataGridViewCheckBoxColumn CheckBoxes;
		private System.Windows.Forms.DataGridViewCheckBoxColumn NeedValidation;
		private System.Windows.Forms.DataGridViewTextBoxColumn Classification;
		private System.Windows.Forms.DataGridViewTextBoxColumn URLPattern;
		private System.Windows.Forms.DataGridViewTextBoxColumn URLPatternNecessaryKeywords;
		private System.Windows.Forms.DataGridViewTextBoxColumn URLPatternForbiddenKeywords;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lblProxyColor;
		private System.Windows.Forms.Label lblProxyAddress;
		private System.Windows.Forms.Label lblProxySource;
		private System.Windows.Forms.Label lblProxyLatency;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtPort;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel8;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button btnSetIEPac;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox txtLog;
		private System.Windows.Forms.Button btnGenerate;
		private System.Windows.Forms.ToolStripMenuItem generatePACFileToolStripMenuItem;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button8;
	}
}