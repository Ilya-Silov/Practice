using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

using Wpf.Ui;
using Wpf.Ui.Controls;

namespace practice.Forms
{
    /// <summary>
    /// Логика взаимодействия для Event.xaml
    /// </summary>
    public partial class EventRegistrationWindow : FluentWindow
    {

        List<string> strings = new List<string>() { "A-Я", "Я-А", "По возрастанию", "По убыванию" };
        ObservableCollection<Ivent> ivents;

        private User _user;

        public EventRegistrationWindow(User user)
        {

            this.DataContext = this;

            InitializeComponent();

            ivents = new ObservableCollection<Ivent>(PracticeContext.Instance.Ivents.Include(i => i.Activities).ThenInclude(a => a.Moderator).Include(i => i.City));
            Ivents.ItemsSource = ivents;

            _user = user;

            SortNameIvents.ItemsSource = strings;
        }

        private void SortNameIvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (SortNameIvents.SelectedItem)
            {
                case "A-Я":
                    {
                        ivents = new ObservableCollection<Ivent>(ivents.OrderBy(p => p.Name));
                        Ivents.ItemsSource = ivents;
                        break;
                    }
                case "Я-А":
                    {
                        ivents = new ObservableCollection<Ivent>(ivents.OrderByDescending(p => p.Name));
                        Ivents.ItemsSource = ivents;
                        break;
                    }
                case "По возрастанию":
                    {
                        ivents = new ObservableCollection<Ivent>(ivents.OrderBy(p => p.DateBegin));
                        Ivents.ItemsSource = ivents;
                        break;
                    }
                case "По убыванию":
                    {
                        ivents = new ObservableCollection<Ivent>(ivents.OrderByDescending(p => p.DateBegin));
                        Ivents.ItemsSource = ivents;
                        break;
                    }

            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Activity activity = (Activity)((FrameworkElement)sender).DataContext;
            activity.ModeratorId = _user.Id;
            PracticeContext.Instance.SaveChanges();

            SnackbarService snackbarService = new SnackbarService();
            snackbarService.SetSnackbarPresenter(snack);
            snackbarService.Show(
                "Данные обновлены",
                "Вы стали модератором активности",
                ControlAppearance.Success,
                new SymbolIcon(SymbolRegular.Checkmark12),
                TimeSpan.FromSeconds(4)
                );
            
        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            if (((Ivent)((FrameworkElement)sender).DataContext).Activities.IsNullOrEmpty())
            {
                ((FrameworkElement)sender).Visibility = Visibility.Visible;
            }
        }
    }
}
