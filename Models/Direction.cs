using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace practice.Models
{
    public class Direction
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string DirectionName { get; set; }
    }
}
