using GalaSoft.MvvmLight;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace practice.Models
{
    public class User: INotifyPropertyChanged
    {
        private string phone;

        public int Id { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Patronomic { get; set; }

        public string Email { get; set; }


        public string Gender { get; set; }

        public DateTime? Birthday { get; set; }

        public int? CountryID { get; set; }
        public Country? Country { get; set; }

        public string Phone { get => phone; set
            {
                phone = value;
                OnPropertyChanged();
            }
        }

        public int? DirectionId { get; set; }
        public Direction? Direction { get; set; }

        public string Password { get; set; }

        public string? Photo { get; set; }

        public override string ToString()
        {
            return Surname + " " + Name + " " + Patronomic;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
