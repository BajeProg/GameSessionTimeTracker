using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GameSessionTimer
{
    public static class ProcessWatcher
    {
        public static void Update(List<Game> currentSessionGames)
        {
            CheckRunGames(currentSessionGames);
            CheckProcesses(currentSessionGames);
        }
        private static void CheckRunGames(List<Game> currentSessionGames)
        {
            foreach (var game in Games.GameList)
            {
                var process = Process.GetProcessesByName(game.ProcessName).FirstOrDefault();

                if (process is null || currentSessionGames.Contains(game)) continue;

                currentSessionGames.Add(game);
            }
        }
        private static void CheckProcesses(List<Game> currentSessionGames)
        {
            List<Game> gamesToRemove = new List<Game>();
            foreach (var game in currentSessionGames)
            {
                var process = Process.GetProcessesByName(game.ProcessName).FirstOrDefault();
                if (process is null) gamesToRemove.Add(game);
            }

            foreach (var game in gamesToRemove)
                currentSessionGames.Remove(game);
        }
    }
}
