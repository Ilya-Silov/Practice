﻿using practice.Models;

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
    /// Логика взаимодействия для Participant.xaml
    /// </summary>
    public partial class Participant : UiWindow
    {
        public User User { get; set; }

        public Participant(User user)
        {
            this.DataContext = this;
            User = user;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            JuryModeratorRegistration registration = new JuryModeratorRegistration();
            registration.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile();
            profile.ShowDialog();
        }


        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            EventWindow eventWindow = new EventWindow();
            eventWindow.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Autorization autorization = new Autorization();
            autorization.Show();
            this.Close();
        }

        public string MrMrs
        {
            get
            {
                if (User.Gender.Equals("женский"))
                {
                    return "Mrs.";
                }
                return "Mr.";
            }
        }

        public string WelcomDatePart
        {
            get
            {
                if (DateTime.Now.TimeOfDay > new TimeSpan(18, 0, 0))
                {
                    return "Добрый вечер";
                }
                if (DateTime.Now.TimeOfDay > new TimeSpan(11, 0, 0))
                {
                    return "Добрый день";
                }
                if (DateTime.Now.TimeOfDay > new TimeSpan(5, 0, 0))
                {
                    return "Доброе утро";
                }
                else
                {
                    return "Доброй ночи";
                }
            }
        }
    }
}
