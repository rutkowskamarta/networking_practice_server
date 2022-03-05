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

		public Dictionary<IClient, Player> Players { get; private set; }

		public Room(string roomID)
		{
			ID = roomID;
			Players = new Dictionary<IClient, Player>();
		}

		public void JoinRoom(IClient client, Player player)
		{
			if (!Players.ContainsKey(client))
			{
				Players.Add(client, player);
			}
			UpdateRoomStateNotification(Tags.Tags.UpdateRoomStateNotification);
		}

		public void LeaveRoom(IClient client)
		{
			Players.Remove(client);
			UpdateRoomStateNotification(Tags.Tags.UpdateRoomStateNotification);
		}

		public void SendRoomCreationNotification()
		{
			UpdateRoomStateNotification(Tags.Tags.CreateRoomResponseSucess);
		}

		private void UpdateRoomStateNotification(ushort tag)
		{
			if(IsEmpty())
			{
				return;
			}

			var firstPlayer = Players.First();

			foreach (var kvp in Players)
			{
				using (DarkRiftWriter writer = DarkRiftWriter.Create())
				{
					bool isAdministrator = firstPlayer.Value == kvp.Value;
					writer.Write(isAdministrator);
					writer.Write(ID);
					writer.Write(Players.Values.ToArray());

					using (Message message = Message.Create(tag, writer))
					{
						kvp.Key.SendMessage(message, SendMode.Reliable);
					}
				}
			}
		}

		public bool IsEmpty()
		{
			return Players.Count == 0;
		}
	}
}
