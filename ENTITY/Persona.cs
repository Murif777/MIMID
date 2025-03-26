using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Persona
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public Persona() { }
        public Persona(
            string id,
            string firstName,
            string lastName,
            string phone,
            string gender,
            string email,
            DateTime birthDate)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Gender = gender;
            Email = email;
            BirthDate = birthDate;
        }
        public Persona(
            string id,
            string firstName,
            string lastName,
            string email)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}
