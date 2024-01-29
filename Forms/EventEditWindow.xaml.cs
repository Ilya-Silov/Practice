using Microsoft.EntityFrameworkCore;

using practice.Database;
using practice.Models;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для EventEditWindow.xaml
    /// </summary>
    public partial class EventEditWindow : UiWindow
    {
        public Ivent Event { get; set; }

        public ObservableCollection<City> Cities { get; set; }

        public ObservableCollection<User> Users { get; set; }

        public EventEditWindow()
        {
            this.DataContext = this;

            Event = PracticeContext.Instance.Ivents.Include(i => i.Activities).ThenInclude(a => a.Moderator).Include(i => i.City).Where(i => i.Id == 3).FirstOrDefault();

            //TODO: При изменении не подтягивается порядок
            Event.Activities = new ObservableCollection<Activity>(Event.Activities.OrderBy(a => a.DayNumber).ThenBy(a => a.TimeBegin));



            new Task(() =>
            {
                try
                {
                    Cities = new ObservableCollection<City>(PracticeContext.Instance.Cites);
                    Users = new ObservableCollection<User>(PracticeContext.Instance.Users.Where(u=>u.Role.Name.Equals("Модератор")));
                }
                catch(Exception ex){
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    throw new SystemException();
                }
                Dispatcher.Invoke(() => CityPicker.ItemsSource =Cities);

                Dispatcher.Invoke(() => CityPicker.SelectedItem = Cities.Where(c=>c.Name.Equals(Event.City.Name)).First());
            }).Start();

            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Event.Activities.Add(new Activity() { IventId = Event.Id });
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Wpf.Ui.Controls.Button)sender).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ed4337"));
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Wpf.Ui.Controls.Button)sender).Foreground = Brushes.White;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var activity = (Activity)((Wpf.Ui.Controls.Button)sender).DataContext;
            Event.Activities.Remove(activity);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PracticeContext.Instance.SaveChanges(); 
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(((Wpf.Ui.Controls.TextBox)sender).Text, out int inputValue))
            {
                if (inputValue < 0)
                {
                    ((Wpf.Ui.Controls.TextBox)sender).Text = "0";
                }
                if (inputValue > 23) {
                    ((Wpf.Ui.Controls.TextBox)sender).Text = "23";
                }
            }
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(((Wpf.Ui.Controls.TextBox)sender).Text, out int inputValue))
            {
                if (inputValue < 0)
                {
                    ((Wpf.Ui.Controls.TextBox)sender).Text = "0";
                }
                if (inputValue > 59)
                {
                    ((Wpf.Ui.Controls.TextBox)sender).Text = "59";
                }
            }
        }
    }
}
