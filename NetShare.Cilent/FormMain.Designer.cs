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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabSettings = new System.Windows.Forms.TabPage();
			this.lblIsStarted = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnStop = new System.Windows.Forms.Button();
			this.btnStart = new System.Windows.Forms.Button();
			this.cmbShareAdapter = new System.Windows.Forms.ComboBox();
			this.chckPasswordHide = new System.Windows.Forms.CheckBox();
			this.lblSharedConnection = new System.Windows.Forms.Label();
			this.lblPassword = new System.Windows.Forms.Label();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.txtSSID = new System.Windows.Forms.TextBox();
			this.lblSSID = new System.Windows.Forms.Label();
			this.tabConnectedPeers = new System.Windows.Forms.TabPage();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.chckIsAutostart = new System.Windows.Forms.CheckBox();
			this.tabControl1.SuspendLayout();
			this.tabSettings.SuspendLayout();
			this.tabConnectedPeers.SuspendLayout();
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
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(360, 237);
			this.tabControl1.TabIndex = 0;
			// 
			// tabSettings
			// 
			this.tabSettings.Controls.Add(this.chckIsAutostart);
			this.tabSettings.Controls.Add(this.lblIsStarted);
			this.tabSettings.Controls.Add(this.btnSave);
			this.tabSettings.Controls.Add(this.btnStop);
			this.tabSettings.Controls.Add(this.btnStart);
			this.tabSettings.Controls.Add(this.cmbShareAdapter);
			this.tabSettings.Controls.Add(this.chckPasswordHide);
			this.tabSettings.Controls.Add(this.lblSharedConnection);
			this.tabSettings.Controls.Add(this.lblPassword);
			this.tabSettings.Controls.Add(this.txtPassword);
			this.tabSettings.Controls.Add(this.txtSSID);
			this.tabSettings.Controls.Add(this.lblSSID);
			this.tabSettings.Location = new System.Drawing.Point(4, 22);
			this.tabSettings.Name = "tabSettings";
			this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
			this.tabSettings.Size = new System.Drawing.Size(352, 211);
			this.tabSettings.TabIndex = 0;
			this.tabSettings.Text = "Settings";
			this.tabSettings.UseVisualStyleBackColor = true;
			// 
			// lblIsStarted
			// 
			this.lblIsStarted.AutoSize = true;
			this.lblIsStarted.Location = new System.Drawing.Point(198, 187);
			this.lblIsStarted.Name = "lblIsStarted";
			this.lblIsStarted.Size = new System.Drawing.Size(35, 13);
			this.lblIsStarted.TabIndex = 8;
			this.lblIsStarted.Text = "label4";
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(271, 182);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 7;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnStop
			// 
			this.btnStop.Location = new System.Drawing.Point(87, 182);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(75, 23);
			this.btnStop.TabIndex = 6;
			this.btnStop.Text = "Stop";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(6, 182);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(75, 23);
			this.btnStart.TabIndex = 5;
			this.btnStart.Text = "Start";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// cmbShareAdapter
			// 
			this.cmbShareAdapter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cmbShareAdapter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbShareAdapter.FormattingEnabled = true;
			this.cmbShareAdapter.Location = new System.Drawing.Point(114, 58);
			this.cmbShareAdapter.Name = "cmbShareAdapter";
			this.cmbShareAdapter.Size = new System.Drawing.Size(232, 21);
			this.cmbShareAdapter.TabIndex = 4;
			// 
			// chckPasswordHide
			// 
			this.chckPasswordHide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chckPasswordHide.AutoSize = true;
			this.chckPasswordHide.Location = new System.Drawing.Point(331, 35);
			this.chckPasswordHide.Name = "chckPasswordHide";
			this.chckPasswordHide.Size = new System.Drawing.Size(15, 14);
			this.chckPasswordHide.TabIndex = 3;
			this.chckPasswordHide.UseVisualStyleBackColor = true;
			this.chckPasswordHide.CheckedChanged += new System.EventHandler(this.chckPasswordHide_CheckedChanged);
			// 
			// lblSharedConnection
			// 
			this.lblSharedConnection.AutoSize = true;
			this.lblSharedConnection.Location = new System.Drawing.Point(6, 61);
			this.lblSharedConnection.Name = "lblSharedConnection";
			this.lblSharedConnection.Size = new System.Drawing.Size(100, 13);
			this.lblSharedConnection.TabIndex = 2;
			this.lblSharedConnection.Text = "Shared connection:";
			// 
			// lblPassword
			// 
			this.lblPassword.AutoSize = true;
			this.lblPassword.Location = new System.Drawing.Point(6, 35);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(56, 13);
			this.lblPassword.TabIndex = 2;
			this.lblPassword.Text = "Password:";
			// 
			// txtPassword
			// 
			this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPassword.Location = new System.Drawing.Point(114, 32);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.Size = new System.Drawing.Size(211, 20);
			this.txtPassword.TabIndex = 1;
			this.txtPassword.UseSystemPasswordChar = true;
			// 
			// txtSSID
			// 
			this.txtSSID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSSID.Location = new System.Drawing.Point(114, 6);
			this.txtSSID.Name = "txtSSID";
			this.txtSSID.Size = new System.Drawing.Size(232, 20);
			this.txtSSID.TabIndex = 1;
			// 
			// lblSSID
			// 
			this.lblSSID.AutoSize = true;
			this.lblSSID.Location = new System.Drawing.Point(6, 9);
			this.lblSSID.Name = "lblSSID";
			this.lblSSID.Size = new System.Drawing.Size(35, 13);
			this.lblSSID.TabIndex = 0;
			this.lblSSID.Text = "SSID:";
			// 
			// tabConnectedPeers
			// 
			this.tabConnectedPeers.Controls.Add(this.listBox1);
			this.tabConnectedPeers.Location = new System.Drawing.Point(4, 22);
			this.tabConnectedPeers.Name = "tabConnectedPeers";
			this.tabConnectedPeers.Padding = new System.Windows.Forms.Padding(3);
			this.tabConnectedPeers.Size = new System.Drawing.Size(352, 211);
			this.tabConnectedPeers.TabIndex = 1;
			this.tabConnectedPeers.Text = "Connected peers";
			this.tabConnectedPeers.UseVisualStyleBackColor = true;
			// 
			// listBox1
			// 
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new System.Drawing.Point(6, 6);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(340, 199);
			this.listBox1.TabIndex = 0;
			// 
			// chckIsAutostart
			// 
			this.chckIsAutostart.AutoSize = true;
			this.chckIsAutostart.Location = new System.Drawing.Point(114, 85);
			this.chckIsAutostart.Name = "chckIsAutostart";
			this.chckIsAutostart.Size = new System.Drawing.Size(68, 17);
			this.chckIsAutostart.TabIndex = 9;
			this.chckIsAutostart.Text = "Autostart";
			this.chckIsAutostart.UseVisualStyleBackColor = true;
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(384, 261);
			this.Controls.Add(this.tabControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "FormMain";
			this.Text = "NetShare";
			this.tabControl1.ResumeLayout(false);
			this.tabSettings.ResumeLayout(false);
			this.tabSettings.PerformLayout();
			this.tabConnectedPeers.ResumeLayout(false);
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
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Label lblIsStarted;
		private System.Windows.Forms.CheckBox chckIsAutostart;
	}
}

