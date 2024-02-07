using Microsoft.EntityFrameworkCore;


using practice.Database;
using practice.Models;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

using Wpf.Ui.Controls;



namespace practice.Forms
{
    /// <summary>
    /// Логика взаимодействия для Jury.xaml
    /// </summary>
    public partial class Jury : FluentWindow
    {
        public User User { get; set; }

        public ObservableCollection<Activity> Activities { get; set; }

        public Jury(User user)
        {
            this.DataContext = this;

            User = user;
            Activities = new ObservableCollection<Activity>(PracticeContext.Instance.Activites
                .Include(a => a.ActivityJurys)
                .Include(a=> a.Moderator)
                .Include(a=> a.Ivent)
                .Where(a=> a.ActivityJurys.Any(aj=>aj.JuryID==User.Id))
                );
            //Activities = new ObservableCollection<Activity>(PracticeContext.Instance.ActivityJures.Where(x => x.JuryID == user.Id)
            //    .Include(aj=>aj.Activity)
            //    .ThenInclude(a => a.Moderator)
            //    .Include(a => a.Activity.Ivent)
            //    .Include(a=>a.Activity.ActivityJurys)
            //    .Select(x => x.Activity));

            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            JuryModeratorRegistration registration = new JuryModeratorRegistration();
            registration.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile(User);
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

        private void RatingControl_ValueChanged(object sender, RoutedEventArgs e)
        {
            RatingControl ratingControl = (RatingControl)sender;
            Activity activity = (Activity)ratingControl.DataContext;
            ActivityJury? aj = activity.ActivityJurys.Where(aj => aj.JuryID == User.Id).FirstOrDefault();
            if (aj != null && ratingControl.Value != null)
            {
                aj.Rate = ratingControl.Value;
                PracticeContext.Instance.Update(aj);
            }
        }

        private void RatingControl_Loaded(object sender, RoutedEventArgs e)
        {
            RatingControl ratingControl = (RatingControl)sender;
            Activity activity = (Activity)ratingControl.DataContext;
            ActivityJury? aj = activity.ActivityJurys.Where(aj => aj.JuryID == User.Id).FirstOrDefault();
            if (aj != null && ratingControl.Value != null)
            {
                if (aj.Rate.HasValue)
                {
                    ratingControl.Value = aj.Rate.Value;

                }
            }
        }
    }
}
