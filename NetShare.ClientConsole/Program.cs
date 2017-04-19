using System;
using System.Linq;

namespace NetShare.ClientConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			var netsh = new NetShare.Host.NetShareHost();

			var settings = netsh.GetConnectionSettings();
			Console.WriteLine($"SSID: {settings.SSID}");
			Console.WriteLine($"Max peers count: {settings.MaxPeerCount}");
			Console.WriteLine();

			netsh.SetConnectionSettings("Kupa Keep 2", 20);

			settings = netsh.GetConnectionSettings();
			Console.WriteLine($"SSID: {settings.SSID}");
			Console.WriteLine($"Max peers count: {settings.MaxPeerCount}");
			Console.Read();
		}
	}
}