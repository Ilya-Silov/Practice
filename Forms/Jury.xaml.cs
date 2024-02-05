using Microsoft.EntityFrameworkCore;


using practice.Database;
using practice.Models;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Wpf.Ui.Controls;



namespace practice.Forms
{
    /// <summary>
    /// Логика взаимодействия для Jury.xaml
    /// </summary>
    public partial class Jury : UiWindow
    {
        public User User { get; set; }

        public ObservableCollection<Activity> Activities { get; set; }

        public Jury(User user)
        {
            this.DataContext = this;

            User = user;
            Activities = new ObservableCollection<Activity>(PracticeContext.Instance.ActivityJures.Where(x => x.JuryID == user.Id)
                .Include(aj=>aj.Activity)
                .ThenInclude(a => a.Moderator)
                .Include(a => a.Activity.Ivent)
                .Select(x => x.Activity));

            InitializeComponent();
        }

    }
}
