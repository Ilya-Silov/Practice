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

            Event = PracticeContext.Instance.Ivents.Include(i=> i.Activities).Include(i => i.City).Where(i=>i.Id==3).FirstOrDefault();

            

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

            StartDatePickerStandard.SelectedDate = DateTime.Now + TimeSpan.FromDays(1);

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
    }
}
