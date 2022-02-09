using DarkRift.Server;
using System;

namespace ServerPlugin
{
	public class ServerPluginManager : Plugin
	{
		public override bool ThreadSafe => false;

		public override Version Version => new Version(0, 0, 1);

		public ServerPluginManager(PluginLoadData pluginLoadData) : base(pluginLoadData)
		{
			ClientManager.ClientConnected += OnClientConnected;
			ClientManager.ClientDisconnected += OnClientDisonnected;
		}

		private void OnClientConnected(object sender, ClientConnectedEventArgs e)
		{
			Logger.Log("client connected " + e.Client.ID, DarkRift.LogType.Info);
			e.Client.MessageReceived += Client_MessageReceived;
		}

		private void OnClientDisonnected(object sender, ClientDisconnectedEventArgs e)
		{
			Logger.Log("client disconnected " + e.Client.ID, DarkRift.LogType.Info);
		}

		private void Client_MessageReceived(object sender, MessageReceivedEventArgs e)
		{
			Logger.Log(e.GetMessage().ToString(), DarkRift.LogType.Info);
		}

	}
}
