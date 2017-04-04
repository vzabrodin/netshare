using System;
using System.Windows.Forms;
using NetShare.Host;
using System.Linq;

namespace NetShare.Cilent
{
	public partial class FormMain : Form
	{
		private NetShareHost _netsh;

		public FormMain()
		{
			Configuration conf = new Configuration();
			conf.Read();

			_netsh = new NetShareHost();

			InitializeComponent();
			txtSSID.Text = _netsh.GetConnectionSettings().SSID;
			txtPassword.Text = _netsh.GetPassword();
			foreach (var item in _netsh.GetSharableConnections())
			{
				cmbShareAdapter.Items.Add(item);
			}

			var conns = _netsh.GetSharableConnections();
			SharableConnection sharedConnection = null;
			if (Guid.Empty != conf.SharedConnection)
			{
				sharedConnection = (from c in conns
									where c.Guid == conf.SharedConnection
									select c).FirstOrDefault();
				if (sharedConnection == null)
				{
					sharedConnection = conns.First();
				}
			}

			cmbShareAdapter.SelectedItem = sharedConnection;
			for (int i = 0; i < cmbShareAdapter.Items.Count; i++)
			{
				if ((cmbShareAdapter.Items[i] as SharableConnection).Guid == sharedConnection.Guid)
				{
					cmbShareAdapter.SelectedIndex = i;
					break;
				}
			}

			chckIsAutostart.Checked = conf.IsAutostart;

			lblIsStarted.Text = _netsh.IsStarted() ? "Started" : "Not started";
			_netsh.OnConnectionsListChanged += _netsh_OnConnectionsListChanged;
		}

		private void _netsh_OnConnectionsListChanged(NetShareHost hostedNetworkManager)
		{
			foreach (var item in _netsh.GetConnectedPeers())
			{
				listBox1.Items.Add(item.MacAddress);
			}
		}

		private void chckPasswordHide_CheckedChanged(object sender, EventArgs e)
		{
			txtPassword.UseSystemPasswordChar = !chckPasswordHide.Checked;
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			lblIsStarted.Text = "Waiting...";
			lblIsStarted.Text = _netsh.Start(cmbShareAdapter.SelectedItem as SharableConnection)? "Started" : "Failed";
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
			lblIsStarted.Text = "Waiting...";
			lblIsStarted.Text = _netsh.Stop() ? "Stopped" : "Failed";
		}

		private void btnSave_Click(object sender, EventArgs e)
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
	}
}