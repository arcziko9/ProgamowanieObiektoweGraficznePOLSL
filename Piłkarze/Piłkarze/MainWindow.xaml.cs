using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;


namespace Piłkarze
{
    public partial class MainWindow : Window
    {
        private const string FILE_PATH = @"D:\Politechnika\Semestr4\ProgamowanieObiektoweGraficznePOLSL\Piłkarze\bazaPilkarzy.txt";
        private List<Player> playerList = new List<Player>();

        public MainWindow()
        {
            InitializeComponent();
            FirstLoad();
            AddValuesToAge();

        }

        private bool PlayerExist(Player p)
        {
            if (playerList.Contains(p))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void PrintPlayersToListBox()
        {
            playersList_lb.Items.Clear();
            foreach (var item in playerList)
            {
                playersList_lb.Items.Add(item);
            }
        }

        private void LoadPlayersFromFile()
        {
            string[] lines = File.ReadAllLines(FILE_PATH);
            string[][] data = new string[lines.Length][];
            string firstName = "";
            string lastName = "";
            double weight = 0.0;
            int age = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                string[] tmp = lines[i].Split(',');
                data[i] = new string[tmp.Length + 1];
                for (int j = 0; j < tmp.Length; j++)
                {
                    data[i][j] = Convert.ToString(tmp[j]);
                }
            }
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    firstName = data[i][0];
                    lastName = data[i][1];
                    age = Int32.Parse(data[i][2]);
                    weight = Double.Parse(data[i][3]);
                }
                Player player = new Player(firstName, lastName, age, weight);
                playerList.Add(player);
                PrintPlayersToListBox();
            }
        }

        private void UpdateFile()
        {
            File.WriteAllText(FILE_PATH, String.Empty);
            foreach (var item in playerList)
            {
                File.AppendAllText(FILE_PATH, item.ToStringToFile() + "\n");
            }
        }

        private void FirstLoad()
        {
            firstName_tbx.Foreground = Brushes.Gray;
            firstName_tbx.Text = "Podaj imie";
            lastName_tbx.Foreground = Brushes.Gray;
            lastName_tbx.Text = "Podaj nazwisko";
            LoadPlayersFromFile();
        }

        private Boolean Check()
        {
            bool firstNameCorrect = firstName_tbx.Text.Length != 0 & firstName_tbx.Text.All(char.IsLetter) ? true : false;
            bool lastNameCorrect = lastName_tbx.Text.Length != 0 & lastName_tbx.Text.All(char.IsLetter) ? true : false;
            if(!firstNameCorrect)
            {
                firstName_tbx.BorderBrush = Brushes.Red;
                firstName_tbx.BorderThickness = new Thickness(5, 5, 5, 5);
            } 
            if(!lastNameCorrect)
            {
                lastName_tbx.BorderBrush = Brushes.Red;
                lastName_tbx.BorderThickness = new Thickness(5, 5, 5, 5);

            }
            return firstNameCorrect && lastNameCorrect;
        }

        private void AddValuesToAge()
        {
            for (int i = 15; i < 45; i++)
            {
                age_cb.Items.Add(i);
            }
        }
        
        private Player GetPlayerFromForms()
        {
            string firstName = firstName_tbx.Text;
            string lastName = lastName_tbx.Text;
            string tmp = age_cb.SelectedItem.ToString();
            int age = Int32.Parse(tmp);
            tmp = weight_slider.Value.ToString();
            double weight = Double.Parse(tmp);
            Player player = new Player(firstName, lastName, age, weight);
            return player;
        }

        private void ClearFirstName()
        {
            firstName_tbx.Text = "";
            firstName_tbx.BorderThickness = default;
            firstName_tbx.BorderThickness = default;
            firstName_tbx.Foreground = Brushes.Black;
        }

        private void ClearLastName()
        {
            lastName_tbx.Text = "";
            lastName_tbx.BorderThickness = default;
            lastName_tbx.BorderThickness = default;
            lastName_tbx.Foreground = Brushes.Black;
        }

        private void ClearAllFields()
        {
            ClearFirstName();
            ClearLastName();
            weight_slider.Value = 50.0;
            age_cb.SelectedIndex = 0;
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            
            if(Check())
            {
                Player player = GetPlayerFromForms();
                if(!PlayerExist(player))
                {
                playerList.Add(player);
                PrintPlayersToListBox();
                UpdateFile();
                ClearAllFields();
                }
                else
                {
                    MessageBox.Show("Piłkarz już istnieje");
                }
                    
            }
            else
            {
                MessageBox.Show("Niepoprawne dane wejsciowe");
            }
        }

        private void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            if(playersList_lb.SelectedItem != null)
            {
                Player player = (Player)playersList_lb.SelectedItem;
                playerList.Remove(player);
                PrintPlayersToListBox();
                UpdateFile();
                ClearAllFields();
            }
            
        }

        private void Modify_btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno zmodyfikować tego piłkarza?", "Modyfikacja Piłkarza", MessageBoxButton.YesNo);
            switch (result) 
            {
                case MessageBoxResult.Yes:
                    if(Check() && playersList_lb.SelectedItem != null)
                    {                  
                        Player player = GetPlayerFromForms();
                        if (!PlayerExist(player))
                        {
                            playerList.Remove((Player)playersList_lb.SelectedItem);
                            playerList.Add(player);
                            PrintPlayersToListBox();
                            UpdateFile();
                            ClearAllFields();
                        }
                        else
                        {
                            MessageBox.Show("Piłkarz już istnieje");
                        }
                    }
                    else if(playersList_lb.SelectedItem == null)
                    {
                        MessageBox.Show("Zaznacz piłkarza, którego chcesz edytować");
                    }

                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void SelectionChanged_lb(object sender, RoutedEventArgs e)
        {
            Player player = (Player)playersList_lb.SelectedItem;
            try
            {
                lastName_tbx.Foreground = Brushes.Black;
                firstName_tbx.Foreground = Brushes.Black;
                firstName_tbx.Text = player.firstName;
                lastName_tbx.Text = player.lastName;
                age_cb.SelectedItem = player.age;
                weight_slider.Value = player.weight;
            } catch
            {

            }
        }

        private void MouseDown_firstName_tbx(object sender, MouseButtonEventArgs e)
        {
            if(firstName_tbx.Text == "Podaj imie")
            {
                ClearFirstName();
            }
        }

        private void MouseDown_lastName_tbx(object sender, MouseButtonEventArgs e)
        {
            if (lastName_tbx.Text == "Podaj nazwisko")
            {
                ClearLastName();
            }
        }
    }
}
