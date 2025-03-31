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

        public string Register(User user)
        {
            try
            {
                return userRepository.SaveUser(user);
            }
            catch (Exception ex)
            {
                return "Error registering user: " + ex.Message;
            }
        }

        public User GetUserById(string id)
        {
            // Llamar al método GetUserById definido en UserRepository
            return userRepository.GetUserById(id);
        }

        public string DeleteUser(string id)
        {
            return userRepository.DeleteUser(id);
        }

        public string UpdateUser(User user)
        {
            try
            {
                return userRepository.UpdateUser(user);
            }
            catch (Exception ex)
            {
                return "Error updating user: " + ex.Message;
            }
        }
    }

}
