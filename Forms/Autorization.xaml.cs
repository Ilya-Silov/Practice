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
            if (SignUp())
            {
                Organizator organizator = new Organizator();
                organizator.Show();
                this.Close();
            }
            else 
            {
                
            }
        }

        private void btnAuth_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool SignUp()
        {
            if (txtID.Text.IsNullOrEmpty() || password.Text.IsNullOrEmpty())
            {
                return false;
            }
            User user = db.Users.Where(p => p.Id == Convert.ToInt32(txtID.Text)).FirstOrDefault();
          
            if (user!=null && password.Text == user.Password )
            {
                return true;
            }
            return false;
        }
        
    }
}
