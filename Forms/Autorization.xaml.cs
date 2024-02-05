using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using practice.Database;
using practice.Models;

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
using System.Windows.Shapes;

using Wpf.Ui.Controls;

namespace practice.Forms
{
    /// <summary>
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class Autorization : UiWindow
    {
        PracticeContext db;
        public Autorization()
        {
            db = new PracticeContext();
            InitializeComponent();
        }




        private void password_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            Captcha captcha = new Captcha();
            captcha.ShowDialog();
            User user = SignUp();
            if (user == null)
            {
                return;
            }

            Window windowToOpen;
            switch (user.Role.Name)
            {
                case "Организатор":
                    {
                        windowToOpen = new Organizator(user);
                        break;  
                    }
                case "Модератор":
                    {
                        windowToOpen = new Moderator();
                        break;
                    }
                case "Жюри":
                    {
                        windowToOpen = new Jury(user);
                        break;
                    }
                default:
                    {
                        windowToOpen = new MainWindow();
                        break;
                    }
            }
            windowToOpen.Show();
            
            foreach (Window w in App.Current.Windows)
            {
                if (w != windowToOpen)
                {
                    w.Close();
                }
            }

        }

        private void btnAuth_Click_1(object sender, RoutedEventArgs e)
        {
            bool mainWindowIsOpen = false;
            foreach (var w in App.Current.Windows)
            {
                if (w.GetType() == typeof(MainWindow))
                {
                    mainWindowIsOpen = true;
                    break;
                }
            }
            if (!mainWindowIsOpen) {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }            
            this.Close();
        }

        private User SignUp()
        {
            if (txtID.Text.IsNullOrEmpty() || password.Password.IsNullOrEmpty())
            {
                return null;
            }
            User user = db.Users.Include(u=>u.Role).Where(p => p.Id == Convert.ToInt32(txtID.Text) && p.Password == password.Password).FirstOrDefault();
            return user;
        }
        
    }
}
