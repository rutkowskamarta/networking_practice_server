using DarkRift.Server;
using ServerPlugin.GameManagement;
using ServerPlugin.PlayerManagement;
using ServerPlugin.RoomManagement;
using System;

namespace ServerPlugin
{
	public class ServerPluginManager : Plugin
	{
		public override bool ThreadSafe => false;

		public override Version Version => PluginVersion.Version;

		private PlayerManager playerManager; 
		private RoomManager roomManager;
		private GameManager gameManager;

		public ServerPluginManager(PluginLoadData pluginLoadData) : base(pluginLoadData)
		{
			playerManager = new PlayerManager(pluginLoadData);
			roomManager = new RoomManager(pluginLoadData);
			roomManager.InjectDependecies(playerManager);
			gameManager = new GameManager(pluginLoadData);
			gameManager.InjectDependecies(roomManager);

			ClientManager.ClientConnected += OnClientConnected;
			ClientManager.ClientDisconnected += OnClientDisonnected;
		}

		private void OnClientConnected(object sender, ClientConnectedEventArgs e)
		{
			Logger.Log("Client connected " + e.Client.ID, DarkRift.LogType.Info);
			playerManager.ConnectPlayer(sender, e);
			e.Client.MessageReceived += Client_MessageReceived;
		}

		private void OnClientDisonnected(object sender, ClientDisconnectedEventArgs e)
		{
			Logger.Log("Client disconnected " + e.Client.ID, DarkRift.LogType.Info);
		}

		private void Client_MessageReceived(object sender, MessageReceivedEventArgs eventMessage)
		{
			Logger.Log(eventMessage.GetMessage().ToString(), DarkRift.LogType.Info);

			if (eventMessage.Tag == Tags.Tags.PlayerDataRequest)
			{
				playerManager.SetupPlayerData(eventMessage);
			}
			else if(eventMessage.Tag == Tags.Tags.CreateRoomRequest)
			{
				roomManager.CreateRoom(eventMessage);
			}
			else if(eventMessage.Tag == Tags.Tags.JoinRoomRequest)
			{
				roomManager.JoinRoom(eventMessage);
			}
			else if(eventMessage.Tag == Tags.Tags.LeaveRoomRequest)
			{
				roomManager.LeaveRoom(eventMessage);
			}
			else if(eventMessage.Tag == Tags.Tags.StartGameRequest)
            {
				gameManager.StartGame(eventMessage);
			}
		}
	}
}
