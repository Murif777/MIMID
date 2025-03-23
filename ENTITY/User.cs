using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class User : Persona
    {
        public byte[] Foto { get; set; } = null;
        public User()
        {
        }
        public User(
                     string cedula, string nombre,
                    string apellido, string telefono, string sexo,
                    string correo, DateTime fechaNacimiento, byte[] foto
                    ) : base(
                               cedula, nombre,
                              apellido, telefono, sexo,
                              correo, fechaNacimiento
                              )
        {
            Foto = foto;
        }
    }
}
