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
using Wpf.Ui.Controls;
using practice.Database;
using practice.Models;

namespace practice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : UiWindow
    {
        PracticeContext db;
        public MainWindow()
        {
            db = new PracticeContext();
            InitializeComponent();
            //List <Activity> actions = new List <Activity> ();
            //actions = db.Activity.ToList();
            //Actions.ItemsSource = actions;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Autorization autorization = new Autorization();
            autorization.ShowDialog();
            this.Close();
        }
    
    }
}
