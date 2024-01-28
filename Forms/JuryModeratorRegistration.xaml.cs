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
using System.Windows.Shapes;

using Wpf.Ui.Controls;

namespace practice.Forms
{
    /// <summary>
    /// Логика взаимодействия для JuryModeratorRegistration.xaml
    /// </summary>
    public partial class JuryModeratorRegistration : UiWindow
    {
        public JuryModeratorRegistration()
        {
            InitializeComponent();

        }

        private void pass_check_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void pass_check_Click(object sender, RoutedEventArgs e)
        {
            if (pass_check.IsChecked == true)
            {
                passBoxTxt.Text = passBox.Password;
                passBoxTxt.Visibility = Visibility.Visible;
                passBox.Visibility = Visibility.Hidden;

                passBoxTxt2.Text = passBox2.Password;
                passBoxTxt2.Visibility = Visibility.Visible;
                passBox2.Visibility = Visibility.Hidden;
            }
            else
            {
                passBox.Password = passBoxTxt.Text;
                passBox.Visibility = Visibility.Visible;
                passBoxTxt.Visibility = Visibility.Hidden;

                passBox2.Password = passBoxTxt2.Text;
                passBox2.Visibility = Visibility.Visible;
                passBoxTxt2.Visibility = Visibility.Hidden;
            }
        }

       

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(Char.IsDigit(e.Text,0))
            { e.Handled = true; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
