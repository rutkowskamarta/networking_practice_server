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

		public RoomManager(PluginLoadData pluginLoadData, PlayerManager playerManager) : base(pluginLoadData)
		{
			createdRooms = new Dictionary<string, Room>();
			this.playerManager = playerManager;
		}

		public void CreateRoom(object sender, MessageReceivedEventArgs messageEvent)
		{
			var room = new Room();
			var client = messageEvent.Client;
			room.JoinRoom(client, playerManager.GetPlayer(client));
			room.SendRoomCreationNotification();
			AddRoom(room);
		}

		private void AddRoom(Room room)
		{
			createdRooms.Add(room.ID, room);
		}

		public void RemoveRoom(Room room)
		{
			createdRooms.Add(room.ID, room);
		}

		public void GetRoomOfID(string ID)
		{

		}
	}
}
