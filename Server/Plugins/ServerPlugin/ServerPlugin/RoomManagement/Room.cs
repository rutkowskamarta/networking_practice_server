using DarkRift;
using DarkRift.Server;
using ServerPlugin.PlayerManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerPlugin.RoomManagement
{
	public class Room
	{
		private const int RoomIdLenght = 6;
		private Dictionary<IClient, Player> players;

		public string ID { get; private set; }

		public Room()
		{
			ID = GenerateRandomId();
			players = new Dictionary<IClient, Player>();
		}

		public void JoinRoom(IClient client, Player player)
		{
			players.Add(client, player);
			NotifyOtherPlayers();
		}

		public void LeaveRoom(IClient client)
		{
			players.Remove(client);
		}

		private void NotifyOtherPlayers()
		{
			using (DarkRiftWriter playerWriter = DarkRiftWriter.Create())
			{
				foreach (Player player in players.Values)
				{
					playerWriter.Write(player.PlayerId);
					playerWriter.Write(player.PlayerName);
					playerWriter.Write(player.PlayerPicture);
				}

				//using (Message playerMessage = Message.Create(0, playerWriter))
				//	e.Client.SendMessage(playerMessage, SendMode.Reliable);
			}
		}

		private string GenerateRandomId()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Random random = new Random();

			char letter;

			for (int i = 0; i < RoomIdLenght; i++)
			{
				double asciiCode = random.NextDouble();
				int shift = Convert.ToInt32(Math.Floor(25 * asciiCode));
				letter = Convert.ToChar(shift + 65);
				stringBuilder.Append(letter);
			}
			return stringBuilder.ToString();
		}
	}
}
