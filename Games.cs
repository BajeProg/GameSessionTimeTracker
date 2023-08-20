using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace GameSessionTimer
{
    public class Games
    {
        public delegate void AddGameHandler(Game game);
        public static event AddGameHandler onGameAdded;
        public static List<Game> GameList { get; private set; }

        public Games()
        {
            GameList = new List<Game>();
        }

        public static void Load()
        {
            if (!File.Exists("GameList.json")) return;

            using (var reader = new StreamReader("GameList.json"))
                GameList.AddRange(JsonConvert.DeserializeObject<List<Game>>(reader.ReadToEnd()));
        }

        public static void Save()
        {
            using (var writer = new StreamWriter("GameList.json"))
                writer.Write(JsonConvert.SerializeObject(GameList));
        }

        public static void AddGame(Game game)
        {
            GameList.Add(game);
            onGameAdded?.Invoke(game);
        }
    }
}
