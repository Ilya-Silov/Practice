using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice.Models
{
    public class Ivent
    {
        public int Id { get; set; }

        public int Name { get; set; }

        public DateTime DateBegin { get; set; }

        public int AmountDays { get; set; }

        public string Photo { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public int? WinnerId{ get; set; }
        public User? Winner { get; set; }
    }
}
