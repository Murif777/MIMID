using DAL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BILL
{
    public class UserService : ICrud<User>
    {
        private UserRepository userRepository = new UserRepository(); // Instancia de UserRepository

        public string Registrar(User user)
        {
            try
            {
                return userRepository.GuardarUsuarioBD(user);
            }
            catch (Exception ex)
            {
                return "Error al registrar: " + ex.Message;
            }
        }

        public User BuscarPorId(string id)
        {
            // Llamar al método BuscarPorId definido en UserRepository
            return userRepository.BuscarPorId(id);
        }

        public string EliminarUsuario(string id)
        {
            return userRepository.EliminarUsuarioBD(id);
        }

        public string ActualizarUsuario(User user)
        {
            try
            {
                return userRepository.ActualizarUsuarioBD(user);
            }
            catch (Exception ex)
            {
                return "Error al actualizar: " + ex.Message;
            }
        }
    }

}
