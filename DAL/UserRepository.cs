using ENTITY;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserRepository : ConexionBD
    {
        public string GuardarUsuarioBD(User user)
        {
            string sql = "INSERT INTO Usuarios(Id, FirstName, LastName, Phone, Gender, BirthDate, Email, Photo, Password) " +
                  "VALUES (@Id, @FirstName, @LastName, @Phone, @Gender, @BirthDate, @Email, @Photo, @Password)";
            var conexionBd = conexionBD();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBd);
                comando.Parameters.AddWithValue("@Id", user.Id);
                comando.Parameters.AddWithValue("@FirstName", user.FirstName);
                comando.Parameters.AddWithValue("@LastName", user.LastName);
                comando.Parameters.AddWithValue("@Phone", user.Phone);
                comando.Parameters.AddWithValue("@Gender", user.Gender);
                comando.Parameters.AddWithValue("@BirthDate", user.BirthDate.ToString("yyyy-MM-dd"));
                comando.Parameters.AddWithValue("@Email", user.Email);
                comando.Parameters.AddWithValue("@Photo", user.Photo);
                comando.Parameters.AddWithValue("@Password", user.Password);
                var res = comando.ExecuteNonQuery();
                if (res == 0)
                {
                    return "Usuario no guardado";
                }
                if (res != 0)
                {
                    return $"Usuario registrado";
                }
            }
            catch (MySqlException ex)
            {
                return "Error al guardar " + ex.Message;
            }
            finally
            {
                conexionBd.Close();
            }
            return null;
        }

        public User BuscarPorId(string id)
        {
            string sql = "SELECT * FROM Usuarios WHERE Id = @Id";

            using (var conexionBd = conexionBD())
            {
                try
                {
                    MySqlCommand comando = new MySqlCommand(sql, conexionBd);
                    comando.Parameters.AddWithValue("@Id", id);

                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapUser(reader);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error al buscar el usuario por Id: " + ex.Message);
                    return null;
                }
            }
        }

        private User MapUser(MySqlDataReader reader)
        {
            User user = new User
            {
                Id = reader.GetString("Id"),
                FirstName = reader.GetString("FirstName"),
                LastName = reader.GetString("LastName"),
                Phone = reader.GetString("Phone"),
                Gender = reader.GetString("Gender"),
                Email = reader.GetString("Email"),
                BirthDate = reader.GetDateTime("BirthDate")
            };
            if (!reader.IsDBNull(reader.GetOrdinal("Photo")))
            {
                user.Photo = (byte[])reader["Photo"];
            }
            user.Password = reader.GetString("Password");
            return user;
        }

        public string EliminarUsuarioBD(string id)
        {
            using (var conexionBd = conexionBD())
            {
                using (MySqlTransaction transaction = conexionBd.BeginTransaction())
                {
                    try
                    {
                        string sqlEliminarUsuario = "DELETE FROM Usuarios WHERE Id = @Id";
                        using (MySqlCommand comandoUsuario = new MySqlCommand(sqlEliminarUsuario, conexionBd, transaction))
                        {
                            comandoUsuario.Parameters.AddWithValue("@Id", id);
                            comandoUsuario.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return "Usuario eliminado exitosamente";
                    }
                    catch (MySqlException ex)
                    {
                        transaction.Rollback();
                        return "Error al intentar eliminar el usuario: " + ex.Message;
                    }
                }
            }
        }

        public string ActualizarUsuarioBD(User user)
        {
            string sql = "UPDATE Usuarios SET FirstName=@FirstName, LastName=@LastName, " +
                         "Phone=@Phone, Gender=@Gender, BirthDate=@BirthDate, " +
                         "Email=@Email, Photo=@Photo, Password=@Password " +
                         "WHERE Id=@Id";
            var conexionBd = conexionBD();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBd);
                comando.Parameters.AddWithValue("@Id", user.Id);
                comando.Parameters.AddWithValue("@FirstName", user.FirstName);
                comando.Parameters.AddWithValue("@LastName", user.LastName);
                comando.Parameters.AddWithValue("@Phone", user.Phone);
                comando.Parameters.AddWithValue("@Gender", user.Gender);
                comando.Parameters.AddWithValue("@BirthDate", user.BirthDate.ToString("yyyy-MM-dd"));
                comando.Parameters.AddWithValue("@Email", user.Email);
                comando.Parameters.AddWithValue("@Photo", user.Photo);
                comando.Parameters.AddWithValue("@Password", user.Password);

                int res = comando.ExecuteNonQuery();
                if (res == 0)
                {
                    return "Usuario no actualizado";
                }
                return "Usuario actualizado exitosamente";
            }
            catch (MySqlException ex)
            {
                return "Error al actualizar: " + ex.Message;
            }
            finally
            {
                conexionBd.Close();
            }
        }
    }


}



