using Microsoft.IdentityModel.Tokens;

using practice.Database;
using practice.Models;

using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : FluentWindow
    {
        public Profile(User user)
        {
            User = user;
            this.DataContext = this;
            InitializeComponent();
            PhoneTxt.Text = "";
            PhoneTxt.Text = User.Phone;
            if (user.Gender.Equals("женский"))
            {
                GenderCB.SelectedIndex = 1;
            }
            else
            {
                GenderCB.SelectedIndex = 0;
            }
            
        }
        public User User { get; set; }


        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Char.IsDigit(e.Text, 0))
            { e.Handled = true; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User.Surname = SurnameTxt.Text;
            User.Name = NameTxt.Text;
            User.Patronomic = PatronomicTxt.Text;
            User.Phone = PhoneTxt.Text;
            User.Email = EmailTxt.Text;

            if (Check())
            {

                PracticeContext.Instance.Users.Update(User);
                PracticeContext.Instance.SaveChanges();
                return;
            }
            else
            {
                SnackbarService snackbarService = new SnackbarService();
                snackbarService.SetSnackbarPresenter(snack);
                snackbarService.Show(
                    "Ошибка",
                    "Заполните все поля",
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

        
        private bool Check()
        {
            if (SurnameTxt.Text.IsNullOrEmpty() ||
                NameTxt.Text.IsNullOrEmpty() ||
                PatronomicTxt.Text.IsNullOrEmpty() ||
                PhoneTxt.Text.IsNullOrEmpty() ||
                EmailTxt.Text.IsNullOrEmpty())
            {
                return false;
            }
            return true;
        }
        

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (GenderCB.SelectedIndex)
            {
                case 0:
                    {
                        User.Gender = "мужской";
                        break;
                    }
                case 1:
                    {
                        User.Gender = "женский";
                        break;
                    }
            }
        }

    }
}
