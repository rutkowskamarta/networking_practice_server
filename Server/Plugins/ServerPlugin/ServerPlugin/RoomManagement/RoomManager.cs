using DarkRift;
using DarkRift.Server;
using ServerPlugin.PlayerManagement;
using System;
using System.Collections.Generic;

namespace ServerPlugin.RoomManagement
{
	public class RoomManager : Plugin
	{
		public override bool ThreadSafe => false;
		public override Version Version => PluginVersion.Version;

		private Dictionary<string, Room> createdRooms;
		private PlayerManager playerManager;

		public RoomManager(PluginLoadData pluginLoadData) : base(pluginLoadData)
		{
			createdRooms = new Dictionary<string, Room>();
		}

		public void InjectDependecies(PlayerManager playerManager)
		{
			this.playerManager = playerManager;
		}

		public void CreateRoom(object sender, MessageReceivedEventArgs messageEvent)
		{
			var room = new Room();
			var client = messageEvent.Client;
			room.JoinRoom(client, playerManager.GetPlayer(client));
			room.SendRoomCreationNotification();
			AddRoom(room);
			Logger.Log($"Room of if {room.ID} created.".ToString(), LogType.Info);
		}

		public void JoinRoom(object sender, MessageReceivedEventArgs messageEvent)
		{
			using (DarkRiftReader reader = messageEvent.GetMessage().GetReader())
			{
				string roomID = reader.ReadString();
				var room = GetRoomOfID(roomID);
				if (room == null)
				{
					SendRoomNotExistMessage(messageEvent.Client);
				}
				else
				{
					JoinRoom(room, messageEvent.Client);
				}
			}
		}

		public void LeaveRoom(object sender, MessageReceivedEventArgs messageEvent)
		{
			using (DarkRiftReader reader = messageEvent.GetMessage().GetReader())
			{
				string roomID = reader.ReadString();
				var room = GetRoomOfID(roomID);
				var client = messageEvent.Client;
				room.LeaveRoom(client);
				if (room.IsEmpty())
				{
					RemoveRoom(room);
				}
				Logger.Log($"Player left room of id {room.ID}".ToString(), LogType.Info);
			}
		}

		private void JoinRoom(Room room, IClient client)
		{
			room.JoinRoom(client, playerManager.GetPlayer(client));
			using (DarkRiftWriter playerWriter = DarkRiftWriter.Create())
			{
				using (Message playerMessage = Message.Create(Tags.Tags.JoinRoomResponseSucess, playerWriter))
				{
					client.SendMessage(playerMessage, SendMode.Reliable);
				}
			}
			Logger.Log($"Player joined room of id {room.ID}".ToString(), LogType.Info);
		}

		private void SendRoomNotExistMessage(IClient client)
		{
			using (DarkRiftWriter playerWriter = DarkRiftWriter.Create())
			{
				using (Message playerMessage = Message.Create(Tags.Tags.JoinRoomResponseFail, playerWriter))
				{
					client.SendMessage(playerMessage, SendMode.Reliable);
				}
			}
		}

		private void AddRoom(Room room)
		{
			createdRooms.Add(room.ID, room);
		}

		public void RemoveRoom(Room room)
		{
			createdRooms.Add(room.ID, room);
		}

		public Room GetRoomOfID(string Id)
		{
			return createdRooms[Id];
		}
	}
}
