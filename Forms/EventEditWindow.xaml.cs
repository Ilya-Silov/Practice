using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using practice.Database;
using practice.Models;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class EventEditWindow : FluentWindow, INotifyPropertyChanged
    {
        public Ivent Event { get; set; }

        public ObservableCollection<City> Cities { get; set; }

        public ObservableCollection<User> Moderators { get; set; }
        public ObservableCollection<User> Juries { get; set; }

        public EventEditWindow(Ivent ivent)
        {
            this.DataContext = this;

            Event = ivent;
            Event.PropertyChanged += this.PropertyChanged;
            //Event = PracticeContext.Instance.Ivents.Include(i => i.Activities).ThenInclude(a => a.Moderator).Include(i => i.City).Where(i => i.Id == 3).FirstOrDefault();

            //TODO: При изменении не подтягивается порядок

            if (!Event.Activities.IsNullOrEmpty())
            {
                Event.Activities = new ObservableCollection<Activity>(Event.Activities.OrderBy(a => a.DayNumber).ThenBy(a => a.TimeBegin));
            }


            
                try
                {
                    Cities = new ObservableCollection<City>(PracticeContext.Instance.Cites);
                    OnPropertyChanged(nameof(Cities));
                    Moderators = new ObservableCollection<User>(PracticeContext.Instance.Users.Where(u=>u.Role.Name.Equals("Модератор")));
                    OnPropertyChanged(nameof(Moderators));
                    Juries = new ObservableCollection<User>(PracticeContext.Instance.Users.Where(u => u.RoleId == 1));
                    OnPropertyChanged(nameof(Juries));
                }
                catch(Exception ex){
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    throw new SystemException();
                }
                
            
            OnPropertyChanged();

            InitializeComponent();

            Dispatcher.Invoke(() => CityPicker.ItemsSource = Cities);

            Dispatcher.Invoke(() => CityPicker.SelectedItem = Cities.Where(c => c.Name.Equals(Event.City.Name)).First());
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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


        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(((Wpf.Ui.Controls.TextBox)sender).Text, out int inputValue))
            {
                if (inputValue > Event.AmountDays)
                {
                    ((Wpf.Ui.Controls.TextBox)sender).Text = Event.AmountDays.ToString();
                }
            }            
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Type asd = ((ListView)sender).SelectedItems.GetType();
            List<User> userstoadd = new List<User>();
            foreach (var item in ((ListView)sender).SelectedItems)
            {
                userstoadd.Add((User)item);
            };
            Activity activity = ((ListView)sender).DataContext as Activity;
            List<ActivityJury> currentjur = PracticeContext.Instance.ActivityJures.Include(aj=> aj.Jury).Where(aj => aj.ActivityId == activity.ID).ToList();
            foreach (var item in currentjur)
            {
                if (!userstoadd.Contains(item.Jury))
                {
                    PracticeContext.Instance.Remove(item);
                }
                else
                {
                    userstoadd.Remove(userstoadd.FirstOrDefault(uta => uta.Id == item.JuryID));

                }
            }
            
            foreach (var item in userstoadd)
            {
                PracticeContext.Instance.Add(new ActivityJury()
                {
                    ActivityId = activity.ID,
                    JuryID = item.Id
                });
            }

            PracticeContext.Instance.SaveChanges();
        
        }

        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            List<User> userstoadd = new List<User>();
            Activity activity = ((ListView)sender).DataContext as Activity;
            List<User> currentjur = PracticeContext.Instance.ActivityJures.Include(aj => aj.Jury).Where(aj => aj.ActivityId == activity.ID).Select(aj=>aj.Jury).ToList();
            foreach (var item in ((ListView)sender).Items)
            {
                if (currentjur.Contains(item))
                {
                    ((ListView)sender).SelectedItems.Add(item);
                }
            }
        }
    }
}
