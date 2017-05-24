using System;
using System.Windows.Forms;
using System.Linq;
using System.ServiceProcess;
using System.Reflection;
using NetShare.Host;
using System.Threading;
using System.Diagnostics;

namespace NetShare.Cilent
{
	public partial class FormMain : Form
	{
		private NetShareHost _netsh;
		private ServiceController _service;

		public FormMain()
		{
			InitializeComponent();
			About();
		}

		private bool CheckServices()
		{
			try
			{
				// ICS (Internet Connection Sharing)
				using (var svc = new ServiceController("SharedAccess"))
				{
					if (svc.Status != ServiceControllerStatus.Running)
					{
						svc.Start();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			try
			{
				_service = new ServiceController(NetShare.Service.Properties.Resources.ServiceName);
				if (_service.Status != ServiceControllerStatus.Running)
				{
					_service.Start(new string[] { "/noautostart" });
				}
			}
			catch (InvalidOperationException)
			{
				MessageBox.Show("Service NetShareService was not found on computer.\nPlease reinstall program", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			return true;
		}

		private void UpdateControls()
		{
			var started = _netsh.IsStarted;

			txtSSID.ReadOnly = started;
			txtPassword.ReadOnly = started;
			cmbShareAdapter.Enabled = !started;
			lblIsStarted.Text = started ? "Started" : "Stopped";
			btnStart.Visible = !started;
			btnStop.Visible = started;
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			try
			{
				_netsh = new NetShareHost();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "NetShare", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
				return;
			}

			if (!CheckServices())
			{
				Application.Exit();
				return;
			}

			Configuration conf = new Configuration();
			conf.Read();

			txtSSID.Text = _netsh.GetConnectionSettings().SSID;
			txtPassword.Text = _netsh.GetPassword();

			var conns = _netsh.GetSharableConnections();
			foreach (var item in conns)
			{
				cmbShareAdapter.Items.Add(item);
			}

			SharableConnection sharedConnection = null;
			sharedConnection = (from c in conns
								where c.Guid == conf.SharedConnection
								select c).FirstOrDefault();
			if (sharedConnection == null)
			{
				sharedConnection = conns.First();
			}

			for (int i = 0; i < cmbShareAdapter.Items.Count; i++)
			{
				if ((cmbShareAdapter.Items[i] as SharableConnection).Guid == sharedConnection.Guid)
				{
					cmbShareAdapter.SelectedIndex = i;
					break;
				}
			}

			chckIsAutostart.Checked = conf.IsAutostart;
			UpdateControls();
			UpdateConnectionsListSafe();

			_netsh.ConnectionsListChanged += _netsh_OnConnectionsListChanged;
			_netsh.HostedNetworkStarted += _netsh_HostedNetworkStarted;
			_netsh.HostedNetworkStopped += _netsh_HostedNetworkStopped;
		}

		private void cmDataGridUpdate_Click(object sender, EventArgs e)
		{
			UpdateConnectionsListSafe();
		}

		private void _netsh_HostedNetworkStarted(NetShareHost hostedNetworkManager)
		{
			StartStopThreadSafe(true);
		}

		private void _netsh_HostedNetworkStopped(NetShareHost hostedNetworkManager)
		{
			StartStopThreadSafe(false);
		}

		private delegate void StartStopCallback(bool isStart);
		private void StartStopThreadSafe(bool isStart)
		{
			if (lblIsStarted.InvokeRequired)
			{
				try
				{
					Invoke(new StartStopCallback(StartStopThreadSafe), isStart);
				}
				catch
				{
				}
			}
			else
			{
				UpdateControls();
				if (!isStart)
				{
					dataGridView1.Rows.Clear();
				}

				_service.Refresh();
				if (_service.Status != ServiceControllerStatus.Running)
				{
					if (MessageBox.Show("NetShare Service was unexpectedly stopped. Do you want to restart service?", "NetShare", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
					Application.Restart();
				}
			}
		}

		private void DataGridAddConnectionThread(object macAddress)
		{
			string MacAddress = (string)macAddress;
			try
			{
				IPInfo2 ipInfo = null;
				for (int i = 0; i < 10; i++)
				{
					ipInfo = IPInfo2.GetIPInfo(MacAddress.ToLowerInvariant());
					if (ipInfo != null)
					{
						dataGridView1.Rows.Add(ipInfo.HostName, ipInfo.IPAddress, ipInfo.MacAddress);
						return;
					}
					Thread.Sleep(1000);
				}
			}
			catch (ThreadAbortException)
			{
			}
		}

		private delegate void DelegateVoid();
		private void UpdateConnectionsListSafe()
		{
			if (dataGridView1.InvokeRequired)
			{
				try
				{
					Invoke(new DelegateVoid(UpdateConnectionsListSafe));
				}
				catch
				{
				}
			}
			else
			{
				if (_netsh.IsStarted)
				{
					dataGridView1.Rows.Clear();
					foreach (var item in _netsh.GetConnectedPeers())
					{
						Thread thread = new Thread(new ParameterizedThreadStart(DataGridAddConnectionThread));
						thread.Start(item.MacAddress);
					}
				}
			}
		}

		private void _netsh_OnConnectionsListChanged(NetShareHost hostedNetworkManager)
		{
			UpdateConnectionsListSafe();
		}

		private void chckPasswordHide_CheckedChanged(object sender, EventArgs e)
		{
			txtPassword.UseSystemPasswordChar = !chckPasswordHide.Checked;
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			_service.Refresh();
			if (_service.Status != ServiceControllerStatus.Running)
			{
				if (MessageBox.Show("NetShare Service is stopped. Do you want to restart service?", "NetShare", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
				Application.Restart();
			}
			if (!ConfigValidate()) return;
			ConfigSave();
			_service.ExecuteCommand(200);
			lblIsStarted.Text = "Waiting...";
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
			_service.ExecuteCommand(201);
			lblIsStarted.Text = "Waiting...";
		}

		private bool ConfigValidate()
		{
			if (string.IsNullOrEmpty(txtSSID.Text))
			{
				MessageBox.Show("SSID can not be empty");
				return false;
			}
			if (string.IsNullOrEmpty(txtPassword.Text) || txtPassword.Text.Length < 8 || txtPassword.Text.Length > 63)
			{
				MessageBox.Show("Password must be between 8 or 63 characters");
				return false;
			}
			return true;
		}

		private void ConfigSave()
		{
			_netsh.SetConnectionSettings(txtSSID.Text, 10);
			_netsh.SetPassword(txtPassword.Text);
			new Configuration(chckIsAutostart.Checked, (cmbShareAdapter.SelectedItem as SharableConnection).Guid).Write();
		}

		private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (e.CloseReason != CloseReason.ApplicationExitCall)
			{
				ConfigSave();
			}
		}

		#region Методы доступа к атрибутам сборки

		private void llChangelog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				Process.Start("changelog.txt");
			}
			catch
			{
			}
		}

		private void About()
		{
			labelProductName.Text = AssemblyProduct;
			labelVersion.Text = string.Format("Версия {0}", AssemblyVersion);
			labelCopyright.Text = AssemblyCopyright;
			labelCompanyName.Text = AssemblyCompany;
		}

		public string AssemblyTitle
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
				if (attributes.Length > 0)
				{
					AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
					if (titleAttribute.Title != "")
					{
						return titleAttribute.Title;
					}
				}
				return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
			}
		}

		public string AssemblyVersion
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version.ToString();
			}
		}

		public string AssemblyDescription
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
				if (attributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyDescriptionAttribute)attributes[0]).Description;
			}
		}

		public string AssemblyProduct
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
				if (attributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyProductAttribute)attributes[0]).Product;
			}
		}

		public string AssemblyCopyright
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
				if (attributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
			}
		}

		public string AssemblyCompany
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
				if (attributes.Length == 0)
				{
					return "";
				}
				return ((AssemblyCompanyAttribute)attributes[0]).Company;
			}
		}
		#endregion
	}
}