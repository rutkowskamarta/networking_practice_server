using DarkRift;
using DarkRift.Server;
using ServerPlugin.PlayerManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServerPlugin.RoomManagement
{
	public class Room
	{
		public string ID { get; private set; }

		private Dictionary<IClient, Player> players;

		public Room(string roomID)
		{
			this.ID = roomID;
			players = new Dictionary<IClient, Player>();
		}

		public void JoinRoom(IClient client, Player player)
		{
			if (!players.ContainsKey(client))
			{
				players.Add(client, player);
			}
			UpdateRoomStateNotification(Tags.Tags.UpdateRoomState);
		}

		public void LeaveRoom(IClient client)
		{
			players.Remove(client);
			UpdateRoomStateNotification(Tags.Tags.UpdateRoomState);
		}

		public void SendRoomCreationNotification()
		{
			UpdateRoomStateNotification(Tags.Tags.CreateRoomResponseSucess);
		}

		private void UpdateRoomStateNotification(ushort tag)
		{
			using (DarkRiftWriter playerWriter = DarkRiftWriter.Create())
			{
				playerWriter.Write(ID);
				playerWriter.Write(players.Values.ToArray());

				foreach (var kvp in players)
				{
					using (Message playerMessage = Message.Create(tag, playerWriter))
					{
						kvp.Key.SendMessage(playerMessage, SendMode.Reliable);
					}
				}
			}
		}

		public bool IsEmpty()
		{
			return players.Count == 0;
		}
	}
}
