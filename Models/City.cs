using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace practice.Models
{
    public class City
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
