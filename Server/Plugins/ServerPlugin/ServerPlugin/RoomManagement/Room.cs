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
			UpdateRoomStateNotification();
		}

		public void SendRoomCreationNotification()
		{
			using (DarkRiftWriter playerWriter = DarkRiftWriter.Create())
			{
				playerWriter.Write(ID);
				playerWriter.Write(players.Values.ToArray());

				foreach (var kvp in players)
				{
					using (Message playerMessage = Message.Create(Tags.Tags.CreateRoomResponseSucess, playerWriter))
					{
						kvp.Key.SendMessage(playerMessage, SendMode.Reliable);
					}
				}
			}
		}

		public void LeaveRoom(IClient client)
		{
			players.Remove(client);
			UpdateRoomStateNotification();
		}

		public bool IsEmpty()
		{
			return players.Count == 0;
		}

		private void UpdateRoomStateNotification()
		{
			using (DarkRiftWriter playerWriter = DarkRiftWriter.Create())
			{
				foreach (var kvp in players)
				{
					playerWriter.Write(ID);
					playerWriter.Write(players.Values.ToArray());
					using (Message playerMessage = Message.Create(Tags.Tags.UpdateRoomState, playerWriter))
					{
						kvp.Key.SendMessage(playerMessage, SendMode.Reliable);
					}
				}
			}
		}

		private string GenerateRandomId()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Random random = new Random();

			char letter;

			//add check if this ID already exists
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
