using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace practice.Models
{
    public class Country
    {
        public int Id { get; set; }

        public string CountryName { get; set; }

        public string CountryEngName { get; set; }

        public string Code { get; set; }

    }
}
