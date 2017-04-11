using System;
using Microsoft.Win32;

namespace NetShare.Host
{
	public class Configuration
	{
		public bool IsAutostart { get; set; }

		public Guid SharedConnection { get; set; }

		public Configuration(bool isAutostart, Guid sharedConnection)
		{
			IsAutostart = isAutostart;
			SharedConnection = sharedConnection;
		}

		public Configuration()
		{
		}

		public void Write()
		{
			var q = Registry.LocalMachine.OpenSubKey("SOFTWARE");
			var w = q.GetSubKeyNames();
			var key = Registry.LocalMachine.OpenSubKey("SOFTWARE", true).CreateSubKey("NetShare");
			key.SetValue("Autostart", IsAutostart.ToString());
			key.SetValue("SharedConnectionGuid", SharedConnection);
			key.Close();
		}

		public void Read()
		{
			IsAutostart = bool.Parse(Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("NetShare").GetValue("Autostart", false).ToString());
			SharedConnection = Guid.Parse(Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("NetShare").GetValue("SharedConnectionGuid", Guid.Empty).ToString());
		}
	}
}