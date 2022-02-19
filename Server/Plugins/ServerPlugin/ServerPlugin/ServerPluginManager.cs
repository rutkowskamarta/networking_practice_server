using DarkRift.Server;
using ServerPlugin.PlayerManagement;
using ServerPlugin.RoomManagement;
using ServerPlugin.Tags;
using System;

namespace ServerPlugin
{
	public class ServerPluginManager : Plugin
	{
		public override bool ThreadSafe => false;

		public override Version Version => new Version(0, 0, 1);

		private PlayerManager playerManager; 
		private RoomManager roomManager; 

		public ServerPluginManager(PluginLoadData pluginLoadData) : base(pluginLoadData)
		{
			playerManager = new PlayerManager(pluginLoadData);
			roomManager = new RoomManager();

			ClientManager.ClientConnected += OnClientConnected;
			ClientManager.ClientDisconnected += OnClientDisonnected;
		}

		private void OnClientConnected(object sender, ClientConnectedEventArgs e)
		{
			Logger.Log("client connected " + e.Client.ID, DarkRift.LogType.Info);
			playerManager.ConnectPlayer(sender, e);
			e.Client.MessageReceived += Client_MessageReceived;
		}

		private void OnClientDisonnected(object sender, ClientDisconnectedEventArgs e)
		{
			Logger.Log("client disconnected " + e.Client.ID, DarkRift.LogType.Info);
		}

		private void Client_MessageReceived(object sender, MessageReceivedEventArgs eventMessage)
		{
			Logger.Log(eventMessage.GetMessage().ToString(), DarkRift.LogType.Info);

			if (eventMessage.Tag == Tags.Tags.PlayerData)
			{
				playerManager.SetupPlayerData(eventMessage);
			}
		}
	}
}
