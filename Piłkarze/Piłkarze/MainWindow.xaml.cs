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


namespace Piłkarze
{
    public partial class MainWindow : Window
    {
        //private List<Player> playerList;
        public MainWindow()
        {
            InitializeComponent();
            AddValuesToAge();

        }

        private Boolean Check()
        {
            bool firstNameCorrect = firstName_tbx.Text.Length != 0 & firstName_tbx.Text.All(char.IsLetter) ? true : false;
            bool lastNameCorrect = lastName_tbx.Text.Length != 0 & lastName_tbx.Text.All(char.IsLetter) ? true : false;
            if(firstNameCorrect && lastNameCorrect)
            {
                return true;
            } else
            {
                return false;
            }
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

        private void Clear()
        {
            firstName_tbx.Text = "";
            lastName_tbx.Text = "";
            weight_slider.Value = 50.0;
            age_cb.SelectedIndex = 0;
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            
            if(Check())
            {
                Player player = GetPlayerFromForms();
                playersList_lb.Items.Add(player);
                Clear();
            }
            else
            {
                MessageBox.Show("Niepoprawne dane wejsciowe");
            }
        }

        private void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            playersList_lb.Items.Remove(playersList_lb.SelectedItem);
            Clear();
        }

        private void Modify_btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno zmodyfikować tego piłkarza?", "Modyfikacja Piłkarza", MessageBoxButton.YesNo);
            switch (result) 
            {
                case MessageBoxResult.Yes:
                    if(Check())
                    {
                        playersList_lb.Items.Remove(playersList_lb.SelectedItem);
                        Player player = GetPlayerFromForms();
                        playersList_lb.Items.Add(player);
                        Clear();
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
                firstName_tbx.Text = player.firstName;
                lastName_tbx.Text = player.lastName;
                age_cb.SelectedItem = player.age;
                weight_slider.Value = player.weight;
            } catch
            {

            }
        }


    }
}
