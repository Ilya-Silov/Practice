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
using practice.Parser;
using practice.Forms;
namespace practice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Parser.Parser.ParseCity();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Autorization autorization = new Autorization();
            autorization.ShowDialog();
            this.Close();
        }

        private void mainBtnAuth_MouseEnter(object sender, MouseEventArgs e)
        {
            mainBtnAuth.Foreground = new SolidColorBrush(Color.FromRgb(126, 180, 234));

        }

        private void mainBtnAuth_MouseLeave(object sender, MouseEventArgs e)
        {
            mainBtnAuth.Foreground = new SolidColorBrush(Colors.Black);

        }

        private void mainBrdAuth_MouseEnter(object sender, MouseEventArgs e)
        {
            mainBrdAuth.BorderBrush = new SolidColorBrush(Color.FromRgb(126, 180, 234));

        }

        private void mainBrdAuth_MouseLeave(object sender, MouseEventArgs e)
        {
            mainBrdAuth.BorderBrush = new SolidColorBrush(Colors.Black);

        }
    }
}
