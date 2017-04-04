using System;
using System.Runtime.Serialization;
using NetShare.ICS;

namespace NetShare.Host
{
	[DataContract]
	public class SharableConnection : IComparable<SharableConnection>
	{
		public SharableConnection() { }

		public SharableConnection(Ics connection)
		{
			Name = connection.Name;
			DeviceName = connection.DeviceName;
			Guid = connection.Guid;
		}

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public string DeviceName { get; set; }

		[DataMember]
		public Guid Guid { get; set; }

		public override string ToString()
		{
			return Name;
		}

		public int CompareTo(SharableConnection other)
		{
			return Guid.CompareTo(other.Guid);
		}
	}
}