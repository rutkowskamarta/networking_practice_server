using System.Collections.Generic;

namespace ServerPlugin.RoomManagement
{
	public class RoomManager
	{
		private Dictionary<string, Room> createdRooms = new Dictionary<string, Room>();

		public void AddRoom(Room room)
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
