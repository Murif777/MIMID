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
        public string SaveUser(User user)
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
                    return "User not saved";
                }
                if (res != 0)
                {
                    return "User registered";
                }
            }
            catch (MySqlException ex)
            {
                return "Error saving user: " + ex.Message;
            }
            finally
            {
                conexionBd.Close();
            }
            return null;
        }

        public User GetUserById(string id)
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
                    Console.WriteLine("Error getting user by Id: " + ex.Message);
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

        public string DeleteUser(string id)
        {
            using (var conexionBd = conexionBD())
            {
                using (MySqlTransaction transaction = conexionBd.BeginTransaction())
                {
                    try
                    {
                        string sqlDeleteUser = "DELETE FROM Usuarios WHERE Id = @Id";
                        using (MySqlCommand comandoUsuario = new MySqlCommand(sqlDeleteUser, conexionBd, transaction))
                        {
                            comandoUsuario.Parameters.AddWithValue("@Id", id);
                            comandoUsuario.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return "User deleted successfully";
                    }
                    catch (MySqlException ex)
                    {
                        transaction.Rollback();
                        return "Error deleting user: " + ex.Message;
                    }
                }
            }
        }

        public string UpdateUser(User user)
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
                    return "User not updated";
                }
                return "User updated successfully";
            }
            catch (MySqlException ex)
            {
                return "Error updating user: " + ex.Message;
            }
            finally
            {
                conexionBd.Close();
            }
        }
    }


}



