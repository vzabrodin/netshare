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
			this.tabControl1.SuspendLayout();
			this.tabSettings.SuspendLayout();
			this.panSettingsInput.SuspendLayout();
			this.tabConnectedPeers.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabSettings);
			this.tabControl1.Controls.Add(this.tabConnectedPeers);
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
			this.panSettingsInput.Size = new System.Drawing.Size(340, 103);
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
			this.dataGridView1.AllowUserToResizeColumns = false;
			this.dataGridView1.AllowUserToResizeRows = false;
			this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
			this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.hostName,
            this.ipAddress,
            this.macAddress});
			this.dataGridView1.Location = new System.Drawing.Point(6, 6);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.ShowCellErrors = false;
			this.dataGridView1.ShowCellToolTips = false;
			this.dataGridView1.ShowEditingIcon = false;
			this.dataGridView1.ShowRowErrors = false;
			this.dataGridView1.Size = new System.Drawing.Size(340, 199);
			this.dataGridView1.TabIndex = 0;
			// 
			// hostName
			// 
			this.hostName.HeaderText = "Host Name";
			this.hostName.Name = "hostName";
			this.hostName.ReadOnly = true;
			// 
			// ipAddress
			// 
			this.ipAddress.HeaderText = "IP";
			this.ipAddress.Name = "ipAddress";
			this.ipAddress.ReadOnly = true;
			// 
			// macAddress
			// 
			this.macAddress.HeaderText = "MAC";
			this.macAddress.Name = "macAddress";
			this.macAddress.ReadOnly = true;
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
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
	}
}

