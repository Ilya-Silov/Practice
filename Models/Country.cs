using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace practice.Models
{
    public class Country
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string EngName { get; set; }

        public string Acronym { get; set; }
    }
}
