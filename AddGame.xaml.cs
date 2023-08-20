using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace GameSessionTimer
{
    /// <summary>
    /// Логика взаимодействия для AddGame.xaml
    /// </summary>
    public partial class AddGame : Window
    {
        public AddGame()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var filePeaker = new OpenFileDialog();
            if (filePeaker.ShowDialog().Value)
            {
                chooseFileNmaeButton.Content = ValidateFileName(filePeaker.SafeFileName);
                if (nameBox.Text != string.Empty) addBtn.IsEnabled = true;
            }
        }

        private void nameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((string)chooseFileNmaeButton.Content != "Указать путь")
                addBtn.IsEnabled = true;

            if (nameBox.Text == string.Empty)
                addBtn.IsEnabled = false;
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            Games.AddGame(new Game(nameBox.Text, (string)chooseFileNmaeButton.Content));
            Close();
        }

        private string ValidateFileName(string fileName)
        {
            int index = fileName.IndexOf(".exe");
            if (index == -1) return fileName;

            return fileName.Remove(index);
        }
    }
}
