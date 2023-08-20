using Newtonsoft.Json;
using System;

namespace GameSessionTimer
{
    public class Game
    {
        [JsonProperty] public string Name { get; private set; }
        [JsonProperty] public string ProcessName { get; private set; }
        [JsonProperty] public TimeSpan TotalTime { get; private set; }
        public Game()
        {
            Name = "untitled";
            TotalTime = TimeSpan.Zero;
        }
        public Game(string name, string processName)
        {
            Name = name;
            TotalTime = TimeSpan.Zero;
            ProcessName = processName;
        }
        public void UpdateTime(TimeSpan time)
        {
            TotalTime = TotalTime.Add(time);
        }
    }
}
