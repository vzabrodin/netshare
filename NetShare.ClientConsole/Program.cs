using System;
using System.Linq;

namespace NetShare.ClientConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length > 0 && args[0] == "/?")
			{
				Console.WriteLine("Usage: VirtualRouterHostConsole [SSID] [Passkey]");
				return;
			}

			var netshHost = new NetShare.Host.NetShareHost();

			if (args.Length == 2)
			{
				var strSSID = args[0];
				var strPassKey = args[1];

				netshHost.SetConnectionSettings(strSSID, 10);
				netshHost.SetPassword(strPassKey);

				Console.WriteLine("SSID: " + strSSID);
				Console.WriteLine("Passkey: " + strPassKey);
				Console.WriteLine();
			}

			var conns = netshHost.GetSharableConnections();
			var connToShare = conns.FirstOrDefault();
			if (!netshHost.Start(connToShare))
			{
				Console.WriteLine("ERROR: Virtual Router could not be started. Supported hardware may not have been found.");
				Console.WriteLine();
			}

			Console.WriteLine();

			Console.WriteLine("Virtual Router Service Running... [Press Enter To Stop]");

			Console.ReadLine();

			netshHost.Stop();

			Console.WriteLine("Virtual Router Service Stopped.");
		}
	}
}