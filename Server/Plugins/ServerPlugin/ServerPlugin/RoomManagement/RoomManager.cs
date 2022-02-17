using System.Collections.Generic;

namespace ServerPlugin.RoomManagement
{
	static class RoomManager
	{
		private static Dictionary<string, Room> createdRooms = new Dictionary<string, Room>();

		public static void AddRoom(Room room)
		{
			createdRooms.Add(room.ID, room);
		}

		public static void RemoveRoom(Room room)
		{
			createdRooms.Add(room.ID, room);
		}

		public static void GetRoomOfID(string ID)
		{

		}
	}
}
