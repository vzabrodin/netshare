using System;
using System.ServiceProcess;
using System.Diagnostics;
using NetShare.Host;

namespace NetShare.Service
{
	public partial class NetShareService : ServiceBase
	{
		private bool _stoppedSuspend;
		private NetShareHost _netshHost = null;

		public NetShareService()
		{
			_netshHost = new NetShareHost();
			InitializeComponent();
		}		

		protected override void OnStart(string[] args)
		{
			if (args.Length == 1 && args[0] == "/noautostart") return;

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

		protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
		{
			switch (powerStatus)
			{
				case PowerBroadcastStatus.Suspend:
					_stoppedSuspend = _netshHost.IsStarted;
					StopAP();
					break;
				case PowerBroadcastStatus.ResumeSuspend:
					if (_stoppedSuspend)
					{
						StartAP();
					}
					break;
			}
			return base.OnPowerEvent(powerStatus);
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
			if (!_netshHost.Start(conf.SharedConnection))
			{
				throw new Exception("Failed to stop hosted network\n" + _netshHost.GetLastError());
			}
			else
			{
				WriteLog("Hosted network started successfully\n");
			}
		}

		private void StopAP()
		{
			if (!_netshHost.Stop())
			{
				throw new Exception("Failed to stop hosted network\n" + _netshHost.GetLastError());
			}
			else
			{
				WriteLog("Hosted network stopped successfully\n");
			}
		}

		private void WriteLog(string message)
		{
			EventLog.WriteEntry(Properties.Resources.ServiceName, message, EventLogEntryType.Information);
		}
	}
}