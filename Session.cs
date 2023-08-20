using System;
using System.Collections.Generic;
using System.Windows.Threading;

namespace GameSessionTimer
{
    public class Session
    {
        public DispatcherTimer _dispatcherTimer { get; private set; }
        public TimeSpan _sessionTime { get; private set; }
        public List<Game> CurrentSessionGames { get; private set; } = new List<Game>();

        public Session()
        {
            _dispatcherTimer = new DispatcherTimer();
            _sessionTime = TimeSpan.Zero;
            StartSessionTimer();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (CurrentSessionGames.Count > 0) _sessionTime = _sessionTime.Add(_dispatcherTimer.Interval);
            foreach (var game in CurrentSessionGames) game.UpdateTime(_dispatcherTimer.Interval);

            ProcessWatcher.Update(CurrentSessionGames);
            if (CurrentSessionGames.Count == 0) _sessionTime = TimeSpan.Zero;
        }

        private void StartSessionTimer()
        {
            _dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            _dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            _dispatcherTimer.Start();
        }
    }
}
