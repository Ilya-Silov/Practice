using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice.Models
{
    public class Activity
    {

        public int ID { get; set; }

        public string Name { get; set; }

        public int DayNumber { get; set; }

        public DateTime TimeBegin { get; set; }

        public int ModeratorId { get; set; }
        public User Moderator { get; set; }

        public int IventId { get; set; }
        public Ivent Ivent { get; set; }

    }
}
