﻿using System.Runtime.Serialization;
using NetShare.Wlan;

namespace NetShare.Host
{
	[DataContract]
	public class ConnectedPeer
	{
		public ConnectedPeer()
		{
		}

		public ConnectedPeer(WlanStation peer)
			: this()
		{
			MacAddress = peer.MacAddress;
		}

		[DataMember]
		public string MacAddress { get; set; }
	}
}