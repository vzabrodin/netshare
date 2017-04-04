using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
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
			Configuration conf = new Configuration();
			conf.Read();
			if (!conf.IsAutostart)
			{
				Stop();
				return;
			}

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
				Stop();
			}
		}

		protected override void OnStop()
		{
			_netshHost.Stop();
		}
	}
}