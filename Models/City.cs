using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace practice.Models
{
    public class City
    {

        public int Id { get; set; }

        public string HyperAddress { get; set; }

        public string CityName { get; set; }
    }
}
