using DarkRift.Server;
using System;

namespace ServerPlugin
{
	public class ServerPlugin : Plugin
	{
		public override bool ThreadSafe => false;

		public override Version Version => new Version(0, 0, 1);

		public ServerPlugin(PluginLoadData pluginLoadData) : base(pluginLoadData)
		{
			ClientManager.ClientConnected += OnClientConnected;
			ClientManager.ClientDisconnected += OnClientDisonnected;
		}

		private void OnClientConnected(object sender, ClientConnectedEventArgs e)
		{
			Logger.Log("client connected " + e.Client.ID, DarkRift.LogType.Info);
		}

		private void OnClientDisonnected(object sender, ClientDisconnectedEventArgs e)
		{
			Logger.Log("client disconnected " + e.Client.ID, DarkRift.LogType.Info);
		}
	}
}
