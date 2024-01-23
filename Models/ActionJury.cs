using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice.Models
{
    public class ActionJury
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int JuryID { get; set; }
        public Jury Jury { get; set; }
        
        public int? ActionId { get; set; }
        public Action Action { get; set; }
    }
}
