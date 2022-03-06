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
            SendCategoryUpdateStateNotification(Tags.Tags.GameCategoryAddedNotification, category);
        }

        public void RemoveCetegory(string category)
        {
            Categories.Remove(category);
            SendCategoryUpdateStateNotification(Tags.Tags.GameCategoryRemovedNotification, category);
        }

        public void SendCategoryUpdateStateNotification(ushort tag, string category)
        {
            using (DarkRiftWriter writer = DarkRiftWriter.Create())
            {
                writer.Write(category);
                SendMessageToAllPlayers(tag, writer);
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
