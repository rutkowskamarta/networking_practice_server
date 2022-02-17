using ServerPlugin.PlayerManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerPlugin.RoomManagement
{
	class Room
	{
		private const int RoomIdLenght = 6;
		private Dictionary<string, Player> players;

		public string ID { get; private set; }

		public Room()
		{
			ID = GenerateRandomId();
			players = new Dictionary<string, Player>();
		}

		public void JoinRoom(Player player)
		{

		}

		public void LeaveRoom(Player player)
		{

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
