﻿using System;
using System.Collections.Generic;
using System.Linq;
using NETCONLib;

namespace NetShare.ICS
{
	public class IcsManager
	{
		protected INetSharingManager _NSManager;

		public IcsManager()
		{
			this.Init();
		}

		public void Init()
		{
			_NSManager = new NetSharingManager();
		}

		public void EnableIcs(Guid publicGuid, Guid privateGuid)
		{
			if (!SharingInstalled)
			{
				throw new Exception("Internet Connection Sharing NOT Installed");
			}

			var connections = Connections;

			IcsConnection publicConn = (from c in connections
										where c.IsMatch(publicGuid)
										select c).First();

			IcsConnection privateConn = (from c in connections
										 where c.IsMatch(privateGuid)
										 select c).First();

			this.DisableIcsOnAll();

			publicConn.EnableAsPublic();
			privateConn.EnableAsPrivate();
		}

		public void DisableIcsOnAll()
		{
			foreach (var conn in Connections)
			{
				conn.DisableSharing();
				/*
				if (conn.IsSupported)
				{
					conn.DisableSharing();
				}
				*/
			}
		}

		public List<IcsConnection> Connections
		{
			get
			{
				var list = new List<IcsConnection>();

				foreach (INetConnection conn in _NSManager.EnumEveryConnection)
				{
					list.Add(new IcsConnection(_NSManager, conn));
				}

				return list;
			}
		}

		public bool SharingInstalled
		{
			get
			{
				return _NSManager.SharingInstalled;
			}
		}
	}
}