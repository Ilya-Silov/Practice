using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;

using practice.Database;
using practice.Models;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

using MessageBox = System.Windows.MessageBox;

namespace practice.Forms
{
    /// <summary>
    /// Логика взаимодействия для JuryModeratorRegistration.xaml
    /// </summary>
    public partial class JuryModeratorRegistration : FluentWindow
    {
        public string FilePath { get; set; }

        PracticeContext db;
        List<Role> roles = new();
        List<string> strRoles;
        User user = new User();
        public JuryModeratorRegistration()
        {
            this.DataContext = this;
            db = new PracticeContext();
            roles = db.Roles.ToList();
            strRoles = new List<string>();
            foreach (Role role in roles)
            {
                strRoles.Add(role.Name);
            }
            InitializeComponent();
            VisibilityCB(Visibility.Collapsed);
            RoleCB.ItemsSource = strRoles;
            IventCB.ItemsSource = GetIvents();



        }
        public ObservableCollection<Ivent> GetIvents()
        {
            return new ObservableCollection<Ivent>(
                    db.Ivents.ToList<Ivent>());
        }
        public ObservableCollection<Activity> GetActivites()
        {
            return new ObservableCollection<Activity>(
                    db.Activites.Where(p=> p.IventId == IventCB.SelectedIndex+1).ToList<Activity>());
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
            if (Char.IsDigit(e.Text, 0))
            { e.Handled = true; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            user.Surname = SurnameTxt.Text;
            user.Name = NameTxt.Text;
            user.Patronomic = PatronomicTxt.Text;            
            user.Phone = new string(PhoneTxt.Text.Where(t => Char.IsDigit(t)).ToArray());
            user.Email = EmailTxt.Text;
            user.Password = passBox.Password;

            if (Check() && CheckEmail() && CheckPhone())
            {
                Task<string> task = Profile.UploadImage(FilePath);
                task.ContinueWith(t =>
                {
                    if (!task.IsFaulted)
                    {
                        user.Photo = task.Result;
                    }
                    PracticeContext.Instance.Users.Update(user);

                    PracticeContext.Instance.SaveChanges();

                });
                db.Users.Add(user);
                db.SaveChanges();
                this.Close();
            }
            else 
            {  
                SnackbarService snackbarService = new SnackbarService();
                snackbarService.SetSnackbarPresenter(snack);
                snackbarService.Show(
                    "Ошибка",
                    "Правильно заполните все поля",
                    ControlAppearance.Danger,
                    new SymbolIcon(SymbolRegular.SlideText16),
                    TimeSpan.FromSeconds(3)
                    );
            }
   

            //switch (user.RoleId)
            //{
            //    case 3:
            //        {
            //            ActivityJury activityJury = new ActivityJury();
            //            activityJury.Id = db.ActivityJures.Max(x => x.Id)+1;
            //            activityJury.JuryID = user.Id;
            //            activityJury.ActivityId = db.Activites.Where(x => x.Name == ActivityCB.SelectedItem.ToString()).FirstOrDefault().ID;
            //            db.ActivityJures.Add(activityJury);
            //            break;
            //        }
            //}
            //db.SaveChanges();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RoleCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (RoleCB.SelectedIndex) 
            {
                case 0: 
                    {
                        user.RoleId = RoleCB.SelectedIndex + 1;
                        VisibilityCB(Visibility.Visible);
                        break;
                    }
                case 1:
                    {
                        user.RoleId = RoleCB.SelectedIndex + 1;
                        VisibilityCB(Visibility.Visible);
                        break;

                    }
                case 2:
                    {
                        user.RoleId = RoleCB.SelectedIndex + 1;
                        VisibilityCB(Visibility.Visible);
                        break;
                    }
                case 3:
                    {
                        user.RoleId = RoleCB.SelectedIndex + 1;
                        VisibilityCB(Visibility.Collapsed);

                        break;
                    }
            }

        }
        private bool Check() 
        {
            if (SurnameTxt.Text.IsNullOrEmpty() || 
                NameTxt.Text.IsNullOrEmpty() || 
                PatronomicTxt.Text.IsNullOrEmpty() || 
                PhoneTxt.Text.IsNullOrEmpty() || 
                EmailTxt.Text.IsNullOrEmpty() || 
                passBox.Password.IsNullOrEmpty() ||
                string.IsNullOrWhiteSpace(SurnameTxt.Text) ||
                string.IsNullOrWhiteSpace(NameTxt.Text) ||
                string.IsNullOrWhiteSpace(PatronomicTxt.Text) ||
                string.IsNullOrWhiteSpace(PhoneTxt.Text) ||
                string.IsNullOrWhiteSpace(passBox.Password))
            {
                return false;
            }
            return true;
        }
        private bool CheckEmail()
        {
            Regex regex = new Regex(@"([А-Яа-яa-zA-Z0-9.-]+@[А-Яа-яa-zA-Z0-9.-]+.[А-Яа-яa-zA-Z0-9_-]+)");
            MatchCollection matchCollection = regex.Matches(EmailTxt.Text);
            if (matchCollection.Count < 1)
            {
                return false;
            }
            return true;
        }
        private bool CheckPhone()
        {
            if (user.Phone.Length < 11)
            {
                return false;
            }
            return true;
        }
        private void VisibilityCB (Visibility visibility) 
        {
            IventCB.Visibility = visibility;
            ActivityCB.Visibility = visibility;
            IventTxt.Visibility = visibility;
            ActivityTxt.Visibility = visibility;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (GenderCB.SelectedIndex)
            {
                case 0:
                    {
                        user.Gender = "мужской";
                        break;
                    }
                case 1: 
                    {
                        user.Gender = "женский";
                        break;
                    }
            }
        }

        private void IventCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var activites = GetActivites();
            ActivityCB.ItemsSource = activites;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == true)
            {
                FilePath = openFile.FileName;
                ImagePhoto.Source = new BitmapImage(new Uri(FilePath, UriKind.Absolute));
            }
        }
    }
}
