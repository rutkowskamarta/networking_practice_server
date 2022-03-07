using DarkRift;

namespace ServerPlugin.PlayerManagement
{
	public class Player : IDarkRiftSerializable
	{
		public int PlayerId { get; private set; }
		public string PlayerName { get; private set; }
		public PlayerVisualisation PlayerVisualisation { get; private set; }

		public bool IsPlayerReady { get; private set; }

		public Player()
		{

		}

		public Player(int playerId, string playerName, PlayerVisualisation playerVisualisation)
		{
			PlayerId = playerId;
			PlayerName = playerName;
			PlayerVisualisation = playerVisualisation;
		}

		public void SetData(int playerId, string playerName, PlayerVisualisation playerVisualisation)
		{
			PlayerId = playerId;
			PlayerName = playerName;
			PlayerVisualisation = playerVisualisation;
		}

		public void Deserialize(DeserializeEvent deserializeEvent)
		{
			PlayerId = deserializeEvent.Reader.ReadInt32();
			PlayerName = deserializeEvent.Reader.ReadString();
			PlayerVisualisation = deserializeEvent.Reader.ReadSerializable<PlayerVisualisation>();
		}

		public void Serialize(SerializeEvent serializeEvent)
		{
			serializeEvent.Writer.Write(PlayerId);
			serializeEvent.Writer.Write(PlayerName);
			serializeEvent.Writer.Write(PlayerVisualisation);
		}

		public void SetPlayerReadyState(bool isReady)
		{
			IsPlayerReady = isReady;
		}
	}
}
