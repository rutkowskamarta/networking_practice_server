using DarkRift;
using DarkRift.Server;
using System;
using System.Collections.Generic;

namespace ServerPlugin.PlayerManagement
{
	public class PlayerManager : Plugin
	{
		Dictionary<IClient, Player> players;

		public override bool ThreadSafe => false;
		public override Version Version => new Version(0, 0, 1);

		public PlayerManager(PluginLoadData pluginLoadData) : base(pluginLoadData)
		{
			players = new Dictionary<IClient, Player>();
		}

		public void ConnectPlayer(object sender, ClientConnectedEventArgs e)
		{
			players.Add(e.Client, new Player());
		}

		public void SetupPlayerData(MessageReceivedEventArgs messageEvent)
		{
			using (DarkRiftReader reader = messageEvent.GetMessage().GetReader())
			{
				Player player = players[messageEvent.Client];

				int playerId = reader.ReadInt32();
				string playerName = reader.ReadString();
				int playerPicture = reader.ReadInt32();
				
				Logger.Log($"Player of playerID{playerId} and name {playerName} logged in".ToString(), LogType.Info);

				player.SetData(playerId, playerName, playerPicture);
			}
		}

	}
}
