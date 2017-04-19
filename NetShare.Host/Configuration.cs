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
			var key = Registry.LocalMachine.CreateSubKey("SOFTWARE").CreateSubKey("NetShare");
			key.SetValue("Autostart", IsAutostart.ToString());
			key.SetValue("SharedConnectionGuid", SharedConnection);
			key.Close();
		}

		public void Read()
		{
			var key = Registry.LocalMachine.CreateSubKey("SOFTWARE").CreateSubKey("NetShare");
			IsAutostart = bool.Parse(key.GetValue("Autostart", false).ToString());
			SharedConnection = Guid.Parse(key.GetValue("SharedConnectionGuid", Guid.Empty).ToString());
		}
	}
}