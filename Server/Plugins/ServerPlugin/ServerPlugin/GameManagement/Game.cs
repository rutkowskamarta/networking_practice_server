using DarkRift;
using ServerPlugin.RoomManagement;
using System.Collections.Generic;

namespace ServerPlugin.GameManagement
{
    public class Game
    {
        public GameState GameState { get; private set; }
        public List<string> Categories { get; private set; } = new List<string>();

        private Room room;

        public Game(Room room)
        {
            this.room = room;
        }

        public void SendGameStartedNotification()
        {
            using (DarkRiftWriter writer = DarkRiftWriter.Create())
            {
                SendMessageToAllPlayers(Tags.Tags.GameStartedResponseSucess, writer);
            }
        }

        public void AddCategory(string category)
        {
            Categories.Add(category);
            SendCategoryUpdateStateNotification();
        }

        public void RemoveCetegory(string category)
        {
            Categories.Remove(category);
            SendCategoryUpdateStateNotification();
        }

        public void SendCategoryUpdateStateNotification()
        {
            using (DarkRiftWriter writer = DarkRiftWriter.Create())
            {
                writer.Write(Categories.ToArray());
                SendMessageToAllPlayers(Tags.Tags.GameCategoryUpdateNotification, writer);
            }
        }

        private void SendMessageToAllPlayers(ushort tag, DarkRiftWriter writer)
        {
            foreach (var kvp in room.Players)
            {
                using (Message message = Message.Create(tag, writer))
                {
                    kvp.Key.SendMessage(message, SendMode.Reliable);
                }
            }
        }
    }
}
