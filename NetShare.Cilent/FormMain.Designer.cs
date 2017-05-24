namespace NetShare.Cilent
{
	partial class FormMain
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabSettings = new System.Windows.Forms.TabPage();
			this.panSettingsInput = new System.Windows.Forms.Panel();
			this.txtSSID = new System.Windows.Forms.TextBox();
			this.chckIsAutostart = new System.Windows.Forms.CheckBox();
			this.lblSSID = new System.Windows.Forms.Label();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.lblPassword = new System.Windows.Forms.Label();
			this.chckPasswordHide = new System.Windows.Forms.CheckBox();
			this.lblSharedConnection = new System.Windows.Forms.Label();
			this.cmbShareAdapter = new System.Windows.Forms.ComboBox();
			this.lblIsStarted = new System.Windows.Forms.Label();
			this.btnStop = new System.Windows.Forms.Button();
			this.btnStart = new System.Windows.Forms.Button();
			this.tabConnectedPeers = new System.Windows.Forms.TabPage();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.hostName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ipAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.macAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolstripDatagridUpdate = new System.Windows.Forms.ToolStripMenuItem();
			this.tabAbout = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.logoPictureBox = new System.Windows.Forms.PictureBox();
			this.labelProductName = new System.Windows.Forms.Label();
			this.labelVersion = new System.Windows.Forms.Label();
			this.labelCopyright = new System.Windows.Forms.Label();
			this.labelCompanyName = new System.Windows.Forms.Label();
			this.llChangelog = new System.Windows.Forms.LinkLabel();
			this.tabControl1.SuspendLayout();
			this.tabSettings.SuspendLayout();
			this.panSettingsInput.SuspendLayout();
			this.tabConnectedPeers.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			this.tabAbout.SuspendLayout();
			this.tableLayoutPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabSettings);
			this.tabControl1.Controls.Add(this.tabConnectedPeers);
			this.tabControl1.Controls.Add(this.tabAbout);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Multiline = true;
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(360, 237);
			this.tabControl1.TabIndex = 0;
			// 
			// tabSettings
			// 
			this.tabSettings.Controls.Add(this.panSettingsInput);
			this.tabSettings.Controls.Add(this.lblIsStarted);
			this.tabSettings.Controls.Add(this.btnStop);
			this.tabSettings.Controls.Add(this.btnStart);
			this.tabSettings.Location = new System.Drawing.Point(4, 22);
			this.tabSettings.Name = "tabSettings";
			this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
			this.tabSettings.Size = new System.Drawing.Size(352, 211);
			this.tabSettings.TabIndex = 0;
			this.tabSettings.Text = "Settings";
			this.tabSettings.UseVisualStyleBackColor = true;
			// 
			// panSettingsInput
			// 
			this.panSettingsInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panSettingsInput.Controls.Add(this.txtSSID);
			this.panSettingsInput.Controls.Add(this.chckIsAutostart);
			this.panSettingsInput.Controls.Add(this.lblSSID);
			this.panSettingsInput.Controls.Add(this.txtPassword);
			this.panSettingsInput.Controls.Add(this.lblPassword);
			this.panSettingsInput.Controls.Add(this.chckPasswordHide);
			this.panSettingsInput.Controls.Add(this.lblSharedConnection);
			this.panSettingsInput.Controls.Add(this.cmbShareAdapter);
			this.panSettingsInput.Location = new System.Drawing.Point(6, 6);
			this.panSettingsInput.Name = "panSettingsInput";
			this.panSettingsInput.Size = new System.Drawing.Size(340, 102);
			this.panSettingsInput.TabIndex = 9;
			// 
			// txtSSID
			// 
			this.txtSSID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSSID.Location = new System.Drawing.Point(111, 3);
			this.txtSSID.Name = "txtSSID";
			this.txtSSID.Size = new System.Drawing.Size(226, 20);
			this.txtSSID.TabIndex = 1;
			// 
			// chckIsAutostart
			// 
			this.chckIsAutostart.AutoSize = true;
			this.chckIsAutostart.Location = new System.Drawing.Point(111, 82);
			this.chckIsAutostart.Name = "chckIsAutostart";
			this.chckIsAutostart.Size = new System.Drawing.Size(68, 17);
			this.chckIsAutostart.TabIndex = 5;
			this.chckIsAutostart.Text = "Autostart";
			this.chckIsAutostart.UseVisualStyleBackColor = true;
			// 
			// lblSSID
			// 
			this.lblSSID.AutoSize = true;
			this.lblSSID.Location = new System.Drawing.Point(3, 6);
			this.lblSSID.Name = "lblSSID";
			this.lblSSID.Size = new System.Drawing.Size(35, 13);
			this.lblSSID.TabIndex = 0;
			this.lblSSID.Text = "SSID:";
			// 
			// txtPassword
			// 
			this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPassword.Location = new System.Drawing.Point(111, 29);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.Size = new System.Drawing.Size(205, 20);
			this.txtPassword.TabIndex = 2;
			this.txtPassword.UseSystemPasswordChar = true;
			// 
			// lblPassword
			// 
			this.lblPassword.AutoSize = true;
			this.lblPassword.Location = new System.Drawing.Point(3, 32);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(56, 13);
			this.lblPassword.TabIndex = 2;
			this.lblPassword.Text = "Password:";
			// 
			// chckPasswordHide
			// 
			this.chckPasswordHide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chckPasswordHide.AutoSize = true;
			this.chckPasswordHide.Location = new System.Drawing.Point(322, 32);
			this.chckPasswordHide.Name = "chckPasswordHide";
			this.chckPasswordHide.Size = new System.Drawing.Size(15, 14);
			this.chckPasswordHide.TabIndex = 3;
			this.chckPasswordHide.UseVisualStyleBackColor = true;
			this.chckPasswordHide.CheckedChanged += new System.EventHandler(this.chckPasswordHide_CheckedChanged);
			// 
			// lblSharedConnection
			// 
			this.lblSharedConnection.AutoSize = true;
			this.lblSharedConnection.Location = new System.Drawing.Point(3, 58);
			this.lblSharedConnection.Name = "lblSharedConnection";
			this.lblSharedConnection.Size = new System.Drawing.Size(100, 13);
			this.lblSharedConnection.TabIndex = 2;
			this.lblSharedConnection.Text = "Shared connection:";
			// 
			// cmbShareAdapter
			// 
			this.cmbShareAdapter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cmbShareAdapter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbShareAdapter.FormattingEnabled = true;
			this.cmbShareAdapter.Location = new System.Drawing.Point(111, 55);
			this.cmbShareAdapter.Name = "cmbShareAdapter";
			this.cmbShareAdapter.Size = new System.Drawing.Size(226, 21);
			this.cmbShareAdapter.TabIndex = 4;
			// 
			// lblIsStarted
			// 
			this.lblIsStarted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblIsStarted.Location = new System.Drawing.Point(265, 187);
			this.lblIsStarted.Name = "lblIsStarted";
			this.lblIsStarted.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblIsStarted.Size = new System.Drawing.Size(81, 13);
			this.lblIsStarted.TabIndex = 8;
			this.lblIsStarted.Text = "{ServiceStatus}";
			this.lblIsStarted.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// btnStop
			// 
			this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnStop.Location = new System.Drawing.Point(9, 182);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(75, 23);
			this.btnStop.TabIndex = 7;
			this.btnStop.Text = "Stop";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(9, 182);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(75, 23);
			this.btnStart.TabIndex = 6;
			this.btnStart.Text = "Start";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// tabConnectedPeers
			// 
			this.tabConnectedPeers.Controls.Add(this.dataGridView1);
			this.tabConnectedPeers.Location = new System.Drawing.Point(4, 22);
			this.tabConnectedPeers.Name = "tabConnectedPeers";
			this.tabConnectedPeers.Padding = new System.Windows.Forms.Padding(3);
			this.tabConnectedPeers.Size = new System.Drawing.Size(352, 211);
			this.tabConnectedPeers.TabIndex = 1;
			this.tabConnectedPeers.Text = "Connected peers";
			this.tabConnectedPeers.UseVisualStyleBackColor = true;
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AllowUserToResizeRows = false;
			this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
			this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
			this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2);
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.hostName,
            this.ipAddress,
            this.macAddress});
			this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
			this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dataGridView1.Location = new System.Drawing.Point(6, 6);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.ShowCellErrors = false;
			this.dataGridView1.ShowCellToolTips = false;
			this.dataGridView1.ShowEditingIcon = false;
			this.dataGridView1.ShowRowErrors = false;
			this.dataGridView1.Size = new System.Drawing.Size(340, 199);
			this.dataGridView1.StandardTab = true;
			this.dataGridView1.TabIndex = 0;
			// 
			// hostName
			// 
			this.hostName.HeaderText = "Host Name";
			this.hostName.Name = "hostName";
			this.hostName.ReadOnly = true;
			this.hostName.Width = 112;
			// 
			// ipAddress
			// 
			this.ipAddress.HeaderText = "IP";
			this.ipAddress.Name = "ipAddress";
			this.ipAddress.ReadOnly = true;
			this.ipAddress.Width = 112;
			// 
			// macAddress
			// 
			this.macAddress.HeaderText = "MAC";
			this.macAddress.Name = "macAddress";
			this.macAddress.ReadOnly = true;
			this.macAddress.Width = 113;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolstripDatagridUpdate});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(113, 26);
			// 
			// toolstripDatagridUpdate
			// 
			this.toolstripDatagridUpdate.Name = "toolstripDatagridUpdate";
			this.toolstripDatagridUpdate.Size = new System.Drawing.Size(112, 22);
			this.toolstripDatagridUpdate.Text = "Update";
			this.toolstripDatagridUpdate.Click += new System.EventHandler(this.cmDataGridUpdate_Click);
			// 
			// tabAbout
			// 
			this.tabAbout.Controls.Add(this.tableLayoutPanel);
			this.tabAbout.Location = new System.Drawing.Point(4, 22);
			this.tabAbout.Name = "tabAbout";
			this.tabAbout.Padding = new System.Windows.Forms.Padding(3);
			this.tabAbout.Size = new System.Drawing.Size(352, 211);
			this.tabAbout.TabIndex = 2;
			this.tabAbout.Text = "About";
			this.tabAbout.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67F));
			this.tableLayoutPanel.Controls.Add(this.logoPictureBox, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.labelProductName, 1, 0);
			this.tableLayoutPanel.Controls.Add(this.labelVersion, 1, 1);
			this.tableLayoutPanel.Controls.Add(this.labelCopyright, 1, 2);
			this.tableLayoutPanel.Controls.Add(this.labelCompanyName, 1, 3);
			this.tableLayoutPanel.Controls.Add(this.llChangelog, 2, 5);
			this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 6;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.56311F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.56311F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.56311F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.56311F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.18447F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.56311F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(346, 205);
			this.tableLayoutPanel.TabIndex = 1;
			// 
			// logoPictureBox
			// 
			this.logoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.logoPictureBox.Image = global::NetShare.Cilent.Properties.Resources.icon;
			this.logoPictureBox.Location = new System.Drawing.Point(3, 3);
			this.logoPictureBox.Name = "logoPictureBox";
			this.tableLayoutPanel.SetRowSpan(this.logoPictureBox, 4);
			this.logoPictureBox.Size = new System.Drawing.Size(108, 110);
			this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.logoPictureBox.TabIndex = 12;
			this.logoPictureBox.TabStop = false;
			// 
			// labelProductName
			// 
			this.labelProductName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelProductName.Location = new System.Drawing.Point(120, 0);
			this.labelProductName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
			this.labelProductName.MaximumSize = new System.Drawing.Size(0, 17);
			this.labelProductName.Name = "labelProductName";
			this.labelProductName.Size = new System.Drawing.Size(223, 17);
			this.labelProductName.TabIndex = 19;
			this.labelProductName.Text = "Название продукта";
			this.labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelVersion
			// 
			this.labelVersion.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelVersion.Location = new System.Drawing.Point(120, 29);
			this.labelVersion.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
			this.labelVersion.MaximumSize = new System.Drawing.Size(0, 17);
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Size = new System.Drawing.Size(223, 17);
			this.labelVersion.TabIndex = 0;
			this.labelVersion.Text = "Версия";
			this.labelVersion.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// labelCopyright
			// 
			this.labelCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelCopyright.Location = new System.Drawing.Point(120, 58);
			this.labelCopyright.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
			this.labelCopyright.MaximumSize = new System.Drawing.Size(0, 17);
			this.labelCopyright.Name = "labelCopyright";
			this.labelCopyright.Size = new System.Drawing.Size(223, 17);
			this.labelCopyright.TabIndex = 21;
			this.labelCopyright.Text = "Авторские права";
			this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// labelCompanyName
			// 
			this.labelCompanyName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelCompanyName.Location = new System.Drawing.Point(120, 87);
			this.labelCompanyName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
			this.labelCompanyName.MaximumSize = new System.Drawing.Size(0, 17);
			this.labelCompanyName.Name = "labelCompanyName";
			this.labelCompanyName.Size = new System.Drawing.Size(223, 17);
			this.labelCompanyName.TabIndex = 22;
			this.labelCompanyName.Text = "Название организации";
			this.labelCompanyName.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// llChangelog
			// 
			this.llChangelog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.llChangelog.AutoSize = true;
			this.llChangelog.Location = new System.Drawing.Point(285, 192);
			this.llChangelog.Name = "llChangelog";
			this.llChangelog.Size = new System.Drawing.Size(58, 13);
			this.llChangelog.TabIndex = 23;
			this.llChangelog.TabStop = true;
			this.llChangelog.Text = "Changelog";
			this.llChangelog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.llChangelog.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llChangelog_LinkClicked);
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 261);
			this.Controls.Add(this.tabControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "FormMain";
			this.Text = "NetShare";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabSettings.ResumeLayout(false);
			this.panSettingsInput.ResumeLayout(false);
			this.panSettingsInput.PerformLayout();
			this.tabConnectedPeers.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			this.tabAbout.ResumeLayout(false);
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabSettings;
		private System.Windows.Forms.TabPage tabConnectedPeers;
		private System.Windows.Forms.CheckBox chckPasswordHide;
		private System.Windows.Forms.Label lblPassword;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.TextBox txtSSID;
		private System.Windows.Forms.Label lblSSID;
		private System.Windows.Forms.ComboBox cmbShareAdapter;
		private System.Windows.Forms.Label lblSharedConnection;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Label lblIsStarted;
		private System.Windows.Forms.CheckBox chckIsAutostart;
		private System.Windows.Forms.Panel panSettingsInput;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewTextBoxColumn hostName;
		private System.Windows.Forms.DataGridViewTextBoxColumn ipAddress;
		private System.Windows.Forms.DataGridViewTextBoxColumn macAddress;
		private System.Windows.Forms.TabPage tabAbout;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Label labelProductName;
		private System.Windows.Forms.Label labelVersion;
		private System.Windows.Forms.Label labelCopyright;
		private System.Windows.Forms.Label labelCompanyName;
		private System.Windows.Forms.PictureBox logoPictureBox;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem toolstripDatagridUpdate;
		private System.Windows.Forms.LinkLabel llChangelog;
	}
}

