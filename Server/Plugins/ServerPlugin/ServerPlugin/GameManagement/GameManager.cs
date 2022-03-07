using DarkRift;
using DarkRift.Server;
using ServerPlugin.RoomManagement;
using System;
using System.Collections.Generic;

namespace ServerPlugin.GameManagement
{
	public class GameManager : Plugin
	{
		public override bool ThreadSafe => false;

		public override Version Version => PluginVersion.Version;

		private RoomManager roomManager;
		private Dictionary<string, Game> games;

		public GameManager(PluginLoadData pluginLoadData) : base(pluginLoadData)
		{
			games = new Dictionary<string, Game>();
		}

		public void InjectDependecies(RoomManager roomManager)
		{
			this.roomManager = roomManager;
		}

		public void StartGame(MessageReceivedEventArgs messageEvent)
		{
			using (DarkRiftReader reader = messageEvent.GetMessage().GetReader())
			{
				string roomID = reader.ReadString();
				var room = roomManager.GetRoomOfID(roomID);
				var newGame = new Game(room);
				games.Add(roomID, newGame);
				newGame.SendGameStartedNotification();
			}
		}

		public void AddGameCategory(MessageReceivedEventArgs messageEvent)
		{
			using (DarkRiftReader reader = messageEvent.GetMessage().GetReader())
			{
				string roomID = reader.ReadString();
				string category = reader.ReadString();
				var game = GetGameOfRoomID(roomID);
				game.AddCategory(category);
				Logger.Log($"Category of name {category} added to room {roomID}", LogType.Info);
			}
		}

		public void RemoveGameCategory(MessageReceivedEventArgs messageEvent)
		{
			using (DarkRiftReader reader = messageEvent.GetMessage().GetReader())
			{
				string roomID = reader.ReadString();
				string category = reader.ReadString();
				var game = GetGameOfRoomID(roomID);
				game.RemoveCetegory(category);
				Logger.Log($"Category of name {category} removed from room {roomID}", LogType.Info);
			}
		}

		public void ModifyRounds(MessageReceivedEventArgs messageEvent)
		{
			using (DarkRiftReader reader = messageEvent.GetMessage().GetReader())
			{
				string roomID = reader.ReadString();
				int rounds = reader.ReadInt32();
				var game = GetGameOfRoomID(roomID);
				game.ModifiyRoundsNumber(rounds);
				Logger.Log($"Rounds changed to {rounds} for room {roomID}", LogType.Info);
			}
		}

		public void PlayerReadyUp(MessageReceivedEventArgs messageEvent)
		{
			using (DarkRiftReader reader = messageEvent.GetMessage().GetReader())
			{
				string roomID = reader.ReadString();
				var game = GetGameOfRoomID(roomID);
				game.ReadyUpPlayer(messageEvent.Client);
				Logger.Log($"Player ready up for room {roomID}", LogType.Info);
			}
		}

		public void PlayerUnready(MessageReceivedEventArgs messageEvent)
		{
			using (DarkRiftReader reader = messageEvent.GetMessage().GetReader())
			{
				string roomID = reader.ReadString();
				var game = GetGameOfRoomID(roomID);
				game.UnreadyPlayer(messageEvent.Client);
				Logger.Log($"Player unready for room {roomID}", LogType.Info);
			}
		}

		public void GenerateLetter(MessageReceivedEventArgs messageEvent)
		{
			using (DarkRiftReader reader = messageEvent.GetMessage().GetReader())
			{
				string roomID = reader.ReadString();
				var game = GetGameOfRoomID(roomID);
				game.GenerateRandomLetter();
				Logger.Log($"Generates letter for room {roomID}", LogType.Info);
			}
		}

		private Game GetGameOfRoomID(string id)
		{
			return games[id];
		}
	}
}
