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
    /// Логика взаимодействия для Organizator.xaml
    /// </summary>
    public partial class Organizator : UiWindow
    {
        public Organizator()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            JuryModeratorRegistration registration = new JuryModeratorRegistration();
            registration.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile();
            profile.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Participant participant = new Participant();
            participant.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            EventWindow eventWindow = new EventWindow();
            eventWindow.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Autorization autorization = new Autorization(); 
            autorization.Show();
            this.Close();
        }
    }
}
