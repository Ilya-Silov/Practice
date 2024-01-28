using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice.Models
{
    public class ActivityJury
    {

        public int Id { get; set; }

        public int JuryID { get; set; }
        public User Jury { get; set; }
        
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}
