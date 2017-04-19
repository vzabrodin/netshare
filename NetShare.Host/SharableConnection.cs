using System;
using System.Runtime.Serialization;
using NetShare.ICS;

namespace NetShare.Host
{
	[DataContract]
	public class SharableConnection : IComparable<SharableConnection>, IEquatable<SharableConnection>, IEquatable<Guid>
	{
		public SharableConnection() { }

		public SharableConnection(ICSConnection connection)
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

		public bool Equals(SharableConnection other)
		{
			return Guid.Equals(other.Guid);
		}

		public bool Equals(Guid other)
		{
			return Guid.Equals(other);
		}
	}
}