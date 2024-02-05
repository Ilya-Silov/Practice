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
using Microsoft.VisualBasic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace practice
{

    public partial class MainWindow : UiWindow
    {
        PracticeContext db;
        List<string> strings = new List<string>() { "A-Я", "Я-А", "По возрастанию", "По убыванию" };
        List<Ivent> ivents = new List<Ivent>();

        public MainWindow()
        {
            db = new PracticeContext();

            InitializeComponent();

            ivents = db.Ivents.ToList();
            Ivents.ItemsSource = ivents;


            SortNameIvents.ItemsSource = strings;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Autorization autorization = new Autorization();
            autorization.ShowDialog();

        }

        private void SortNameIvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(SortNameIvents.SelectedItem)
            {
                case "A-Я":
                    {
                        ivents = db.Ivents.OrderBy(p => p.Name).ToList();
                        Ivents.ItemsSource = ivents;
                        break;
                    }
                case "Я-А":
                    {
                        ivents = db.Ivents.OrderByDescending(p => p.Name).ToList();
                        Ivents.ItemsSource = ivents;
                        break;
                    }
                case "По возрастанию":
                    {
                        ivents = db.Ivents.OrderBy(p => p.DateBegin).ToList();
                        Ivents.ItemsSource = ivents;
                        break;
                    }
                case "По убыванию":
                    {
                        ivents = db.Ivents.OrderByDescending(p => p.DateBegin).ToList();
                        Ivents.ItemsSource = ivents;
                        break;
                    }

            }
            
        }
    }
}
