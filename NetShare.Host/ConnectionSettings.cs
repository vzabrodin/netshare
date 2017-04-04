using System.Runtime.Serialization;

namespace NetShare.Host
{
	[DataContract]
	public class ConnectionSettings
	{
		[DataMember]
		public string SSID { get; set; }

		[DataMember]
		public int MaxPeerCount { get; set; }
	}
}