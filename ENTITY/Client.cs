using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Client : Persona
    {
        public Client()
        {
        }
        public Client(
                     string cedula, string nombre,
                    string apellido, string correo
                    ) : base(
                               cedula, nombre,
                              apellido, correo
                              )
        {
        }
    }
}
