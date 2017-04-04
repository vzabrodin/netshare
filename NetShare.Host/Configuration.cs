using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace NetShare.Host
{
	[DataContract]
	public class Configuration
	{
		private static string _configPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\NesShare\\config.json";
		private static DataContractJsonSerializer _jsonFormatter = new DataContractJsonSerializer(typeof(Configuration));

		[DataMember]
		public bool IsAutostart { get; set; }

		[DataMember]
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
			new FileInfo(_configPath).Directory.Create();
			using (Stream stream = new FileStream(_configPath, FileMode.Create, FileAccess.Write))
			{
				_jsonFormatter.WriteObject(stream, this);
			}
		}

		public bool Read()
		{
			try
			{
				using (Stream stream = new FileStream(_configPath, FileMode.Open, FileAccess.Read))
				{
					var cfg = (Configuration)_jsonFormatter.ReadObject(stream);
					IsAutostart = cfg.IsAutostart;
					SharedConnection = cfg.SharedConnection;
					return true;
				}
			}
			catch
			{
				return false;
			}
		}
	}
}