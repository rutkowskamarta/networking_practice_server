using DarkRift;
using DarkRift.Server;
using ServerPlugin.RoomManagement;
using System.Collections.Generic;
using System.Linq;

namespace ServerPlugin.GameManagement
{
	public class Game
	{
		public const int DefaultRoundsNumber = 3;

		public GameState GameState { get; private set; }
		public List<string> Categories { get; private set; } = new List<string>();

		private Room room;
		private int rounds;
		private List<char> usedLetters;

		public Game(Room room)
		{
			this.room = room;
			rounds = DefaultRoundsNumber;
			usedLetters = new List<char>();
		}

		public void SendGameStartedNotification()
		{
			using (DarkRiftWriter writer = DarkRiftWriter.Create())
			{
				writer.Write(rounds);
				SendMessageToAllPlayers(Tags.Tags.GameStartedResponseSucess, writer);
			}
		}

		public void AddCategory(string category)
		{
			Categories.Add(category);
			SendCategoryUpdateStateNotification(Tags.Tags.GameCategoryAddedNotification, category);
		}

		public void RemoveCetegory(string category)
		{
			Categories.Remove(category);
			SendCategoryUpdateStateNotification(Tags.Tags.GameCategoryRemovedNotification, category);
		}

		public void ModifiyRoundsNumber(int rounds)
		{
			this.rounds = rounds;
			using (DarkRiftWriter writer = DarkRiftWriter.Create())
			{
				writer.Write(rounds);
				SendMessageToAllPlayers(Tags.Tags.RoundsModifiedResponse, writer);
			}
		}

		public void ReadyUpPlayer(IClient client)
		{
			room.Players[client].SetPlayerReadyState(true);
			HandlePlayerReadyStateChanged();
		}

		public void UnreadyPlayer(IClient client)
		{
			room.Players[client].SetPlayerReadyState(false);
			HandlePlayerReadyStateChanged();
		}

		public void SendCategoryUpdateStateNotification(ushort tag, string category)
		{
			using (DarkRiftWriter writer = DarkRiftWriter.Create())
			{
				writer.Write(category);
				SendMessageToAllPlayers(tag, writer);
			}
		}

		public void GenerateRandomLetter()
		{
			using (DarkRiftWriter writer = DarkRiftWriter.Create())
			{
				char randomLetter = RandomLetterGenarator.GetRandomLetter(usedLetters);
				usedLetters.Add(randomLetter);
				writer.Write(randomLetter);
				SendMessageToAllPlayers(Tags.Tags.LetterGeneratedResponse, writer);
			}
		}

		private void SendMessageToAllPlayers(ushort tag, DarkRiftWriter writer)
		{
			foreach (var kvp in room.Players)
			{
				using (Message message = Message.Create(tag, writer))
				{
					kvp.Key.SendMessage(message, SendMode.Reliable);
				}
			}
		}

		private void HandlePlayerReadyStateChanged()
		{
			int readyPlayersNumber = room.Players.Where(player => player.Value.IsPlayerReady).Count();

			using (DarkRiftWriter writer = DarkRiftWriter.Create())
			{
				writer.Write(readyPlayersNumber);
				SendMessageToAllPlayers(Tags.Tags.ReadyStateChangedResponse, writer);
			}
			HandleAllPlayersReady(readyPlayersNumber);
		}

		private void HandleAllPlayersReady(int readyPlayers)
		{
			if (readyPlayers == room.GetPlayersCount())
			{
				using (DarkRiftWriter writer = DarkRiftWriter.Create())
				{
					SendMessageToAllPlayers(Tags.Tags.EveryoneReadyNotification, writer);
				}
			}
		}
	}
}
