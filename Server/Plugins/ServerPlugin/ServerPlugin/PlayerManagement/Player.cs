namespace ServerPlugin.PlayerManagement
{
	public class Player
	{
		public int PlayerId { get; private set; }
		public string PlayerName { get; private set; }
		public int PlayerPicture { get; private set; }

		public Player()
		{

		}

		public Player(int playerId, string playerName, int playerPicture)
		{
			this.PlayerId = playerId;
			this.PlayerName = playerName;
			this.PlayerPicture = playerPicture;
		}

		public void SetData(int playerId, string playerName, int playerPicture)
		{
			this.PlayerId = playerId;
			this.PlayerName = playerName;
			this.PlayerPicture = playerPicture;
		}
	}
}
