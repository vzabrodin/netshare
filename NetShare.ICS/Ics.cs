using System;
using NETCONLib;

namespace NetShare.ICS
{
	public class Ics
	{
		private INetSharingManager _NSManager;
		private INetSharingConfiguration _config = null;
		private INetConnectionProps _props = null;

		public Ics(INetSharingManager pIcsMgr, INetConnection pNetConnection)
		{
			INetConnection = pNetConnection;
			_NSManager = pIcsMgr;
		}

		public INetConnection INetConnection { get; private set; }

		public INetSharingConfiguration Config
		{
			get
			{
				if (_config == null)
				{
					_config = _NSManager.get_INetSharingConfigurationForINetConnection(INetConnection);
				}
				return _config;
			}
		}

		public INetConnectionProps Props
		{
			get
			{
				if (_props == null)
				{
					_props = _NSManager.get_NetConnectionProps(INetConnection);
				}
				return _props;
			}
		}

		public bool IsSupported
		{
			get
			{
				var props = Props;

				return ((
						props.MediaType == tagNETCON_MEDIATYPE.NCM_LAN
						|| props.MediaType == tagNETCON_MEDIATYPE.NCM_DIRECT
						|| props.MediaType == tagNETCON_MEDIATYPE.NCM_ISDN
						|| props.MediaType == tagNETCON_MEDIATYPE.NCM_PHONE
						|| props.MediaType == tagNETCON_MEDIATYPE.NCM_PPPOE
						|| props.MediaType == tagNETCON_MEDIATYPE.NCM_TUNNEL
						|| props.MediaType == tagNETCON_MEDIATYPE.NCM_BRIDGE
					) && (
						props.Status != tagNETCON_STATUS.NCS_DISCONNECTED
					));
			}
		}

		public bool SharingEnabled
		{
			get
			{
				return Config.SharingEnabled;
			}
		}

		public bool IsPublic
		{
			get
			{
				return (Config.SharingConnectionType == tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PUBLIC);
			}
		}

		public bool IsPrivate
		{
			get
			{
				return (Config.SharingConnectionType == tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PRIVATE);
			}
		}

		public bool IsConnected
		{
			get
			{
				return (Props.Status == tagNETCON_STATUS.NCS_CONNECTED);
			}
		}

		public string DeviceName
		{
			get
			{
				return Props.DeviceName;
			}
		}

		public string Name
		{
			get
			{
				return Props.Name;
			}
		}

		public Guid Guid
		{
			get
			{
				return new Guid(Props.Guid);
			}
		}

		public void DisableSharing()
		{
			var config = Config;
			if (config.SharingEnabled)
			{
				config.DisableSharing();
			}
		}

		public void EnableAsPublic()
		{
			var config = Config;

			config.DisableSharing();

			config.EnableSharing(tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PUBLIC);
		}

		public void EnableAsPrivate()
		{
			var config = Config;

			config.DisableSharing();

			config.EnableSharing(tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PRIVATE);
		}

		public bool IsMatch(Guid guid)
		{
			return ((new Guid(Props.Guid)).ToString().ToLowerInvariant() == guid.ToString().ToLowerInvariant());
		}
	}
}