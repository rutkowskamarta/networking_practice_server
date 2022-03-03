using DarkRift.Server;
using System;

namespace ServerPlugin.GameManagement
{
	public class GameManager : Plugin
	{
		public override bool ThreadSafe => false;

		public override Version Version => PluginVersion.Version;


		public GameManager(PluginLoadData pluginLoadData) : base(pluginLoadData)
		{

		}

		public void StartGame()
		{

		}
	}
}
