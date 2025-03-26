using DAL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BILL
{
    public class ConexionService
    {
		private ConexionBD ConexionBD = new ConexionBD();
        public bool Login(User usuario)
        {
			try
			{
				return ConexionBD.Login(usuario);
			}
			catch (Exception ex )
			{
                Console.WriteLine("error :"+ ex.Message);
                return false;
			}
        }
		public User UsuarioRecuperacion()
		{
			try
			{
				return ConexionBD.UsuariodeRecuperacion();
			}
			catch (Exception ex)
			{
				Console.WriteLine("error :" + ex.Message);

				throw;
			}
		}
        public void cerrarConexion()
        {
            try
            {
                ConexionBD.Cerrarconexion();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error :" + ex.Message);

                throw;
            }
        }
        
        public bool prueba()
        {
            User usuario = new User();
            usuario.Email = "root";
            usuario.Password = "root";
            try
            {
                return ConexionBD.Login(usuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine("error :" + ex.Message);
                return false;
            }
        }
    }
}
