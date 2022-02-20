using DarkRift;

namespace ServerPlugin.PlayerManagement
{
	public class PlayerVisualisation : IDarkRiftSerializable
	{
		public int PlayerVisualisationAccessories { get; private set; }

		public int PlayerVisualisationBackground { get; private set; }

		public int PlayerVisualisationBody { get; private set; }

		public int PlayerVisualisationFace { get; private set; }

		public PlayerVisualisation()
		{

		}

		public PlayerVisualisation(int playerVisualisationAccessories, int playerVisualisationBackground, int playerVisualisationBody, int playerVisualisationFace)
		{
			PlayerVisualisationAccessories = playerVisualisationAccessories;
			PlayerVisualisationBackground = playerVisualisationBackground;
			PlayerVisualisationBody = playerVisualisationBody;
			PlayerVisualisationFace = playerVisualisationFace;
		}

		public void Deserialize(DeserializeEvent deserializeEvent)
		{
			PlayerVisualisationAccessories = deserializeEvent.Reader.ReadInt32();
			PlayerVisualisationBackground = deserializeEvent.Reader.ReadInt32();
			PlayerVisualisationBody = deserializeEvent.Reader.ReadInt32();
			PlayerVisualisationFace = deserializeEvent.Reader.ReadInt32();
		}

		public void Serialize(SerializeEvent serializeEvent)
		{
			serializeEvent.Writer.Write(PlayerVisualisationAccessories);
			serializeEvent.Writer.Write(PlayerVisualisationBackground);
			serializeEvent.Writer.Write(PlayerVisualisationBody);
			serializeEvent.Writer.Write(PlayerVisualisationFace);
		}
	}
}
