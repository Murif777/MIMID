using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class User : Persona
    {
        public byte[] Photo { get; set; } = null;
        public string Password { get; set; }

        public User()
        {
        }

        public User(
                     string id, string firstName,
                    string lastName, string phone, string gender,
                    string email, DateTime birthDate, byte[] photo, string password
                    ) : base(
                               id, firstName,
                              lastName, phone, gender,
                              email, birthDate
                              )
        {
            Photo = photo;
            Password = password;
        }
    }
}
