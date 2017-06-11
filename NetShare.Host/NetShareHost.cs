using System;
using System.Collections.Generic;
using System.Linq;
using NetShare.ICS;
using NetShare.WLAN;
using System.ServiceModel;

namespace NetShare.Host
{
	/// <summary>
	/// Класс для управления точкой доступа Wi-Fi
	/// </summary>
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class NetShareHost
	{
		private WlanManager _wlanManager;
		private IcsManager _icsManager;

		private SharableConnection _currentSharedConnection;

		private string _lastErrorMessage;

		/// <summary>
		/// Представляет метод, коротый будет обрабатывать событие, не имеющее данных
		/// </summary>
		public delegate void GenericEvent(NetShareHost hostedNetworkManager);

		/// <summary>
		/// Происходит при подключении или отключении клиента от точки доступа
		/// </summary>
		public event GenericEvent ConnectionsListChanged;

		/// <summary>
		/// Происходит при запуске точки доступа
		/// </summary>
		public event GenericEvent HostedNetworkStarted;

		/// <summary>
		/// Происходит при остановке точки доступа
		/// </summary>
		public event GenericEvent HostedNetworkStopped;

		/// <summary>
		/// Инициализирует экземпляр класса NetShareHost
		/// </summary>
		public NetShareHost()
		{
			_wlanManager = new WlanManager();
			_icsManager = new IcsManager();

			_wlanManager.StationJoin += _wlanManager_StationStateChange;
			_wlanManager.StationLeave += _wlanManager_StationStateChange;
			_wlanManager.HostedNetworkStarted += _wlanManager_HostedNetworkStarted;
			_wlanManager.HostedNetworkStopped += _wlanManager_HostedNetworkStopped;
		}

		#region "Event Handlers"

		private void _wlanManager_HostedNetworkStopped(object sender, EventArgs e)
		{
			if (HostedNetworkStopped != null)
			{
				HostedNetworkStopped(this);
			}
		}

		private void _wlanManager_HostedNetworkStarted(object sender, EventArgs e)
		{
			if (HostedNetworkStarted != null)
			{
				HostedNetworkStarted(this);
			}
		}

		private void _wlanManager_StationStateChange(object sender, EventArgs e)
		{
			if (ConnectionsListChanged != null)
			{
				ConnectionsListChanged(this);
			}
		}

		#endregion

		/// <summary>
		/// Возвращает текст последней ошибки
		/// </summary>
		/// <returns></returns>
		public string GetLastError()
		{
			return _lastErrorMessage;
		}

		/// <summary>
		/// Запускает точку доступа с указанным адаптером для ICS
		/// </summary>
		/// <param name="sharedConnectionGuid">GUID адаптера для ICS</param>
		/// <returns>true, если точка доступа успешно запущена; false, если возникла ошибка. Вызовите GetLastError(), чтобы получить результат ошибки</returns>
		public bool Start(Guid sharedConnectionGuid)
		{
			var conns = GetSharableConnections();
			SharableConnection sharedConnection = null;
			sharedConnection = (from c in conns
								where c.Guid == sharedConnectionGuid
								select c).FirstOrDefault();
			if (sharedConnection == null)
			{
				return false;
			}
			return Start(sharedConnection);
		}

		/// <summary>
		/// Запускает точку доступа с указанным адаптером для ICS
		/// </summary>
		/// <param name="sharedConnectionGuid">адаптер для ICS</param>
		/// <returns>true, если точка доступа успешно запущена; false, если возникла ошибка. Вызовите GetLastError(), чтобы получить результат ошибки</returns>
		public bool Start(SharableConnection sharedConnection)
		{
			try
			{
				Stop();

				_wlanManager.StartHostedNetwork();

				System.Threading.Thread.Sleep(1000);

				if (sharedConnection != null)
				{
					if (sharedConnection.Guid != Guid.Empty)
					{
						if (_icsManager.SharingInstalled)
						{
							_icsManager.DisableIcsOnAll();

							var privateConnectionGuid = _wlanManager.HostedNetworkInterfaceGuid;

							if (privateConnectionGuid == Guid.Empty)
							{
								// If the GUID for the Hosted Network Adapter isn't return properly,
								// then retrieve it by the DeviceName.

								privateConnectionGuid = (from c in _icsManager.Connections
														 where c.Props.DeviceName.ToLowerInvariant().Contains("microsoft virtual wifi miniport adapter") // Windows 7
														 || c.Props.DeviceName.ToLowerInvariant().Contains("microsoft hosted network virtual adapter") // Windows 8
														 select c.Guid).FirstOrDefault();
								// Note: For some reason the DeviceName can have different names, currently it checks for the ones that I have identified thus far.

								if (privateConnectionGuid == Guid.Empty)
								{
									// Device still now found, so throw exception so the message gets raised up to the client.
									throw new Exception("Virtual Wifi device not found!\n\nNeither \"Microsoft Hosted Network Virtual Adapter\" or \"Microsoft Virtual Wifi Miniport Adapter\" were found.");
								}
							}

							_icsManager.EnableIcs(sharedConnection.Guid, privateConnectionGuid);

							_currentSharedConnection = sharedConnection;
						}
					}
				}
				else
				{
					_currentSharedConnection = null;
				}

				return true;
			}
			catch (Exception ex)
			{
				_lastErrorMessage = ex.Message;
				return false;
			}
		}

		/// <summary>
		/// Останавливает точку доступа
		/// </summary>
		/// <returns>true, если точка доступа успешно остановлена; false, если возникла ошибка. Вызовите GetLastError(), чтобы получить результат ошибки</returns>
		public bool Stop()
		{
			try
			{
				if (_icsManager.SharingInstalled)
				{
					_icsManager.DisableIcsOnAll();
				}

				_wlanManager.StopHostedNetwork();

				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Задаёт настройки точки доступа
		/// </summary>
		/// <param name="ssid">имя точки доступа</param>
		/// <param name="maxNumberOfPeers">максимальное количество подключенных клиентов</param>
		/// <returns>true, если новые настройки успешно применены; false, если возникла ошибка. Вызовите GetLastError(), чтобы получить результат ошибки</returns>
		public bool SetConnectionSettings(string ssid, int maxNumberOfPeers)
		{
			try
			{
				_wlanManager.SetConnectionSettings(ssid, maxNumberOfPeers);
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Возвращает настройки точки доступа
		/// </summary>
		/// <returns>экземпляр класса ConnectionSettings, коротый содержит имя точки доступа и максимальное количество подключенных клиентов</returns>
		public ConnectionSettings GetConnectionSettings()
		{
			try
			{
				string ssid;
				int maxNumberOfPeers;

				var r = _wlanManager.QueryConnectionSettings(out ssid, out maxNumberOfPeers);

				return new ConnectionSettings()
				{
					SSID = ssid,
					MaxPeerCount = maxNumberOfPeers
				};
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		/// Возвращает список подключенных клиентов
		/// </summary>
		/// <returns>список подключенных клиентов</returns>
		public IEnumerable<SharableConnection> GetSharableConnections()
		{
			List<IcsConnection> connections;
			try
			{
				connections = _icsManager.Connections;
			}
			catch
			{
				connections = new List<IcsConnection>();
			}

			// Empty item to signify No Connection Sharing
			yield return new SharableConnection() { DeviceName = "None", Guid = Guid.Empty, Name = "None" };

			if (connections != null)
			{
				foreach (var conn in connections)
				{
					if (conn.IsSupported)
					{
						yield return new SharableConnection(conn);
					}
				}
			}
		}

		/// <summary>
		/// Задаёт пароль точки доступа
		/// </summary>
		/// <param name="password">пароль</param>
		/// <returns>true, если новый пароль успешно применен; false, если возникла ошибка. Вызовите GetLastError(), чтобы получить результат ошибки</returns>
		public bool SetPassword(string password)
		{
			try
			{
				_wlanManager.SetSecondaryKey(password);
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Возвращает пароль точки доступа
		/// </summary>
		/// <returns>пароль</returns>
		public string GetPassword()
		{
			try
			{
				string passKey = string.Empty;
				bool isPassPhrase;
				bool isPersistent;

				var r = _wlanManager.QuerySecondaryKey(out passKey, out isPassPhrase, out isPersistent);

				return passKey;
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		/// true - если точка доступа запущена, иначе - false
		/// </summary>
		public bool IsStarted
		{
			get
			{
				try
				{
					return _wlanManager.IsHostedNetworkStarted;
				}
				catch
				{
					return false;
				}
			}
		}

		/// <summary>
		/// true - если работа в режиме точки доступа Wi-Fi поддерживается, иначе - false
		/// </summary>
		public bool IsSupported
		{
			get
			{
				return _wlanManager.IsSupported;
			}
		}

		/// <summary>
		/// Возвращает список подключенных к точке доступа клиентов
		/// </summary>
		/// <returns>список подключенных клиентов</returns>
		public IEnumerable<ConnectedPeer> GetConnectedPeers()
		{
			foreach (var v in _wlanManager.Stations)
			{
				yield return new ConnectedPeer(v.Value);
			}
		}

		/// <summary>
		/// Возвращает текущее ICS подключение
		/// </summary>
		/// <returns></returns>
		public SharableConnection GetSharedConnection()
		{
			return _currentSharedConnection;
		}
	}
}