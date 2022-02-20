namespace ServerPlugin.PlayerManagement
{
	public class Player
	{
		public int PlayerId { get; private set; }
		public string PlayerName { get; private set; }
		public PlayerVisualisation PlayerVisualisation { get; private set; }

		public Player()
		{

		}

		public Player(int playerId, string playerName, PlayerVisualisation playerVisualisation)
		{
			this.PlayerId = playerId;
			this.PlayerName = playerName;
			this.PlayerVisualisation = playerVisualisation;
		}

		public void SetData(int playerId, string playerName, PlayerVisualisation playerVisualisation)
		{
			this.PlayerId = playerId;
			this.PlayerName = playerName;
			this.PlayerVisualisation = playerVisualisation;
		}
	}
}
