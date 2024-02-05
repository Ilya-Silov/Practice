using Microsoft.EntityFrameworkCore;

using Npgsql.Internal.TypeMapping;

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
    /// Логика взаимодействия для Jury.xaml
    /// </summary>
    public partial class Jury : UiWindow
    {
        public User user { get; set; }

       public ObservableCollection<Activity> Activities { get; set; }

        public Jury(User user)
        {

            Activities = new ObservableCollection<Activity>(PracticeContext.Instance.ActivityJures.Where(x => x.JuryID == user.Id)
                .Include(aj=>aj.Activity)
                .ThenInclude(a => a.Moderator)
                .Include(a => a.Activity.Ivent)
                .Select(x => x.Activity));
                this.DataContext = this;
            InitializeComponent();
        }


    }
}
