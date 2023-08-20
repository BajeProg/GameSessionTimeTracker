using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Forms = System.Windows.Forms;

namespace GameSessionTimer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Session _session;
        private Dictionary<Game, Label> _labelTimes = new Dictionary<Game, Label>();
        private Forms.NotifyIcon _notifyIcon;
        public MainWindow()
        {
            InitializeComponent();
            Games.Load();
            _session = new Session();
            _session._dispatcherTimer.Tick += dispatcherTimer_Tick;
            SetIconToMainApplication();

            foreach (var game in Games.GameList) DisplayGame(game);
        }
        private void SetIconToMainApplication()
        {
            _notifyIcon = new Forms.NotifyIcon()
            {
                Icon = Properties.Resources.ResourceManager.GetObject("icon") as Icon,
                ContextMenuStrip = new Forms.ContextMenuStrip(),
                Visible = false
            };
            _notifyIcon.ContextMenuStrip.Items.AddRange(new[]
            {
                new Forms.ToolStripButton("Open", null, (a,e) => 
                {
                    Show();
                    _notifyIcon.Visible = false;
                }),
                new Forms.ToolStripButton("Close", null, (a,e) => Close())
            } );
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            currentTime.Text = _notifyIcon.Text = _session._sessionTime.ToString();
            currentGamesText.Text = string.Empty;
            foreach (var game in _session.CurrentSessionGames)
            {
                _labelTimes[game].Content = game.TotalTime.ToString();
                currentGamesText.Text += $"{game.Name}   ";
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Games.Save();
        }

        private void DisplayGame(Game game)
        {
            Border border = new Border();
            StackPanel stackPanel = new StackPanel();
            stackPanel.Children.Add(new Label()
            {
                Content = game.Name,
                FontWeight = FontWeights.Bold
            });
            Label time = new Label()
            {
                Content = game.TotalTime.ToString()
            };
            _labelTimes[game] = time;
            stackPanel.Children.Add(time);
            border.Child = stackPanel;
            GamesGrid.Children.Add(border);
        }

        private void addGameBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            addGameBtn.TextDecorations.Add(new TextDecoration() { Location = TextDecorationLocation.Underline });
        }

        private void addGameBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            addGameBtn.TextDecorations.Clear();
        }

        private void addGameBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var addGameWindow = new AddGame();
            addGameWindow.Show();
            Games.onGameAdded += (g) => DisplayGame(g);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                e.Cancel = true;
                Hide();
                _notifyIcon.Visible = true;
            }
            else _notifyIcon.Visible = false;
        }
    }
}
