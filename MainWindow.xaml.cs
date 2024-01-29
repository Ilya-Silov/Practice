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
using System.Security.Cryptography.X509Certificates;

namespace practice
{

    public partial class MainWindow : UiWindow
    {
        PracticeContext db;
        List<string> strings = new List<string>() { "A-Я", "Я-А" };
        List<string> strings2 = new List<string>() { "По возрастанию", "По убыванию" };
        List<Ivent> ivents = new List<Ivent>();

        public MainWindow()
        {
            db = new PracticeContext();

            InitializeComponent();

            ivents = db.Ivents.ToList();
            Ivents.ItemsSource = ivents;


            SortNameIvents.ItemsSource = strings;
            SortDateIvents.ItemsSource = strings2;



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Autorization autorization = new Autorization();
            autorization.Show();

        }

        private void SortNameIvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SortNameIvents.SelectedItem == strings[0] )
            {
                ivents = db.Ivents.OrderBy(p=>p.Name).ToList();
                Ivents.ItemsSource = ivents;
            }
            if (SortNameIvents.SelectedItem == strings[1])
            {
                ivents = db.Ivents.OrderByDescending(p => p.Name).ToList();
                Ivents.ItemsSource = ivents;
            }
        }

        private void SortDateIvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SortDateIvents.SelectedItem == strings2[0])
            {
                ivents = db.Ivents.OrderBy(p => p.DateBegin).ToList();
                Ivents.ItemsSource = ivents;
            }
            if (SortDateIvents.SelectedItem == strings2[1])
            {
                ivents = db.Ivents.OrderByDescending(p => p.DateBegin).ToList();
                Ivents.ItemsSource = ivents;
            }
        }
    }
}
