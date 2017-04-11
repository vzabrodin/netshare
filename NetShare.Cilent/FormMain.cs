using System;
using System.Windows.Forms;
using System.Linq;
using System.ServiceProcess;
using NetShare.Host;

namespace NetShare.Cilent
{
	public partial class FormMain : Form
	{
		private NetShareHost _netsh;
		private ServiceController _service;

		public FormMain()
		{
			InitializeComponent();
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			_netsh = new NetShareHost();
			try
			{
				_service = new ServiceController("NetShareService");
				if (_service.Status != ServiceControllerStatus.Running)
				{
					_service.Start();
				}
			}
			catch (InvalidOperationException)
			{
				MessageBox.Show("Service NetShareService was not found on computer.\nPlease reinstall program", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Close();
			}

			Configuration conf = new Configuration();
			conf.Read();

			txtSSID.Text = _netsh.GetConnectionSettings().SSID;
			txtPassword.Text = _netsh.GetPassword();
			foreach (var item in _netsh.GetSharableConnections())
			{
				cmbShareAdapter.Items.Add(item);
			}

			var conns = _netsh.GetSharableConnections();
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

			panSettingsInput.Enabled = !_netsh.IsStarted;
			lblIsStarted.Text = _netsh.IsStarted ? "Started" : "Stopped";
			btnStart.Visible = !_netsh.IsStarted;
			btnStop.Visible = _netsh.IsStarted;
			if (_netsh.IsStarted)
			{
				UpdateConnectionsListChangedSafe();
			}

			_netsh.ConnectionsListChanged += _netsh_OnConnectionsListChanged;
			_netsh.HostedNetworkStarted += _netsh_HostedNetworkStarted;
			_netsh.HostedNetworkStopped += _netsh_HostedNetworkStopped;
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
				catch { }
			}
			else
			{
				if (isStart)
				{
					lblIsStarted.Text = "Started";
					btnStop.Visible = true;
					btnStart.Visible = false;
					panSettingsInput.Enabled = false;
				}
				else
				{
					lblIsStarted.Text = "Stopped";
					btnStart.Visible = true;
					btnStop.Visible = false;
					panSettingsInput.Enabled = true;
					dataGridView1.Rows.Clear();
				}
			}
		}

		private delegate void delegateVoid();
		private void UpdateConnectionsListChangedSafe()
		{
			if (dataGridView1.InvokeRequired)
			{
				try
				{
					Invoke(new delegateVoid(UpdateConnectionsListChangedSafe));
				}
				catch { }
			}
			else
			{
				dataGridView1.Rows.Clear();
				foreach (var item in _netsh.GetConnectedPeers())
				{
					var ipInfo = IPInfo.GetIPInfo(item.MacAddress.Replace(':', '-').ToLowerInvariant());
					dataGridView1.Rows.Add(ipInfo.HostName, ipInfo.IPAddress, ipInfo.MacAddress);
				}
			}
		}

		private void _netsh_OnConnectionsListChanged(NetShareHost hostedNetworkManager)
		{
			UpdateConnectionsListChangedSafe();
		}

		private void chckPasswordHide_CheckedChanged(object sender, EventArgs e)
		{
			txtPassword.UseSystemPasswordChar = !chckPasswordHide.Checked;
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			ConfigSave();
			_service.ExecuteCommand(200);
			lblIsStarted.Text = "Waiting...";
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
			_service.ExecuteCommand(201);
			lblIsStarted.Text = "Waiting...";
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			ConfigSave();
		}

		private void ConfigSave()
		{
			if (txtSSID.Text == "")
			{
				MessageBox.Show("SSID can not be empty");
				return;
			}

			if (txtPassword.Text.Length < 8)
			{
				MessageBox.Show("Password must be 8 or greater characters");
				return;
			}

			_netsh.SetConnectionSettings(txtSSID.Text, 10);
			_netsh.SetPassword(txtPassword.Text);

			new Configuration(chckIsAutostart.Checked, (cmbShareAdapter.SelectedItem as SharableConnection).Guid).Write();
		}

		private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			ConfigSave();
		}
	}
}