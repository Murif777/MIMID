using ENTITY;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.IO;
using Microsoft.Extensions.Configuration;
namespace DAL
{
    public class ConexionBD
    {
        private string servidor;
        private string bd;
        private static MySqlConnection conexionBd;
        private string user;
        private string password;

        public ConexionBD()
        {
            // Usar ConfigurationBuilder de Microsoft.Extensions.Configuration explícitamente
            var configuration = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Leer configuración
            var dbSettings = configuration.GetSection("DatabaseSettings");
            servidor = dbSettings["Server"];
            bd = dbSettings["Database"];
            user = dbSettings["User"];
            password = dbSettings["Password"];
        }

        // Método para crear la cadena de conexión y la conexión
        private MySqlConnection CrearConexion()
        {
            string cadenaConexion = $"Server={servidor};Database={bd};Uid={user};Pwd={password};";
            Console.WriteLine("Cadena de conexión: " + cadenaConexion);  // Para depuración

            try
            {
                MySqlConnection conexion = new MySqlConnection(cadenaConexion);
                Console.WriteLine("Conexión creada exitosamente.");  // Depuración
                return conexion;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al crear la conexión: " + ex.Message);
                return null;
            }
        }

        // Método para iniciar sesión y abrir la conexión
        public bool Login(User usuario)
        {
            user = usuario.Email;
            password = usuario.Password;

            miembro();
            conexionBd = CrearConexion();

            if (conexionBd == null)
            {
                Console.WriteLine("La conexión no pudo ser creada." + user);
                return false;
            }

            try
            {
                //// // conexionBd.Open();
                Console.WriteLine("Conexión abierta exitosamente.");  // Depuración
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al intentar conectar con la base de datos: " + ex.Message);
                conexionBd.Dispose();
                conexionBd = null;  // Asegurarse de que la conexión no válida no se retiene
                return false;
            }
        }

        public MySqlConnection conexionBD()
        {
            if (conexionBd == null)
            {
                Console.WriteLine("La conexión no está inicializada.");
                return null;
            }

            if (conexionBd.State == System.Data.ConnectionState.Closed)
            {
                conexionBd.Open();
            }
            if (conexionBd.State == System.Data.ConnectionState.Open)
            {
                return conexionBd;
            }
            else
            {
                Console.WriteLine("No hay conexión abierta.");
                return null;
            }
        }

        public void Cerrarconexion()
        {
            if (conexionBd.State == System.Data.ConnectionState.Open)
            {
                conexionBd.Close();
            }
        }

        public User UsuariodeRecuperacion()
        {
            User usuario = new User
            {
                Email = "usuarioRecuperacion",
                Password = "123"
            };
            return usuario;
        }

        private void miembro()
        {
            user = "root";
            password = "root";
        }
    }
}
