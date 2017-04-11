using System;
using System.Linq;
using System.ServiceProcess;
using System.Diagnostics;
using NetShare.Host;

namespace NetShare.Service
{
	public partial class NetShareService : ServiceBase
	{
		private NetShareHost _netshHost = null;

		public NetShareService()
		{
			_netshHost = new NetShareHost();
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			System.Diagnostics.Debugger.Launch();

			Configuration conf = new Configuration();
			conf.Read();

			if (conf.IsAutostart)
			{
				StartAP();
			}
		}

		protected override void OnStop()
		{
			StartAP();
		}

		protected override void OnCustomCommand(int command)
		{
			switch (command)
			{
				case 200:
					StartAP();
					break;
				case 201:
					StopAP();
					break;
			}
		}

		private void StartAP()
		{
			Configuration conf = new Configuration();
			conf.Read();
			var conns = _netshHost.GetSharableConnections();
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
			if (!_netshHost.Start(sharedConnection))
			{
				WriteLog("Failed to start hosted network\n" + _netshHost.GetLastError());
				throw new Exception("Failed to stop hosted network\n" + _netshHost.GetLastError());
			}
		}

		private void StopAP()
		{
			if (!_netshHost.Stop())
			{
				WriteLog("Failed to stop hosted network\n" + _netshHost.GetLastError());
				throw new Exception("Failed to stop hosted network\n" + _netshHost.GetLastError());
			}
		}

		private void WriteLog(string message)
		{
			EventLog.WriteEntry("NetShare Service", message, EventLogEntryType.Information);
		}
	}
}