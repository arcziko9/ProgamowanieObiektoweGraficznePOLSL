using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonDigit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if(Component.Text != "0" && Component.Text != "out of range" && Component.Text != "Cannot divide by zero")
            {
                Component.Text += button.Content.ToString();
            }
            else
            {
                Component.Text = button.Content.ToString();
            }
        }

        private void SignButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (Component.Text != "0" && Sentence.Text == "0" && Component.Text != "out of range" && Component.Text != "Cannot divide by zero")
            {
                Sentence.Text = Component.Text + " " + button.Content.ToString();
                Component.Text = "0";
            }
            else if (Component.Text != "0" && Sentence.Text != "0" && Component.Text != "out of range" && Component.Text != "Cannot divide by zero")
            {
                Sentence.Text += " " + Component.Text + " " + button.Content.ToString();
                Component.Text = 0.ToString();
            }

        }

        private void DotButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (!Component.Text.Contains("."))
            {
                Component.Text += ".";
            }
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (Component.Text != "0" && Component.Text.Length > 1)
            {
                Component.Text = Component.Text.Remove(Component.Text.Length - 1, 1);
            }
            else if (Component.Text.Length == 1)
            {
                Component.Text = "0";
            }
        }

        private void ACButton_Click(object sender, RoutedEventArgs e)
        {
            Component.Text = "0";
            Sentence.Text = "0";
        }

        private void CButton_Click(object sender, RoutedEventArgs e)
        {
            Component.Text = "0";
        }

        private void ChangeSignButton_Click(object sender, RoutedEventArgs e)
        {
            string text = Component.Text;
            if (Component.Text.IndexOf("-") < 0)
            {
                Component.Text = Component.Text.Insert(0, "-");
            }
            else
            {
                Component.Text = Component.Text.Remove(0, 1);
            }



        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            if (isNumber(Component.Text) && !Component.Text.EndsWith("."))
            {
                string math = (Sentence.Text += Component.Text);
                Sentence.Text = "0";
                Component.Text = calculate(math).ToString();
            }
        }

        private bool isNumber(string text)
        {
            double temp;
            if (double.TryParse(text, out temp))
            {
                return true;
            }
            return false;
        }

        private string calculate(string math)
        {
            math = Regex.Replace
            (
                math, @"\d+(\.\d+)?", m =>
                {
                    var x = m.ToString();
                    return x.Contains(".") ? x : string.Format("{0}.0", x);
                }
            );
            try
            {
                double value = Math.Round(Convert.ToDouble(new DataTable().Compute(math, string.Empty)), 8);
                return (value < -9999999999 || value > 9999999999) ? "out of range" : value.ToString();
            }
            catch (DivideByZeroException)
            {
                return "Cannot divide by zero";
            }

        }
    }
}
