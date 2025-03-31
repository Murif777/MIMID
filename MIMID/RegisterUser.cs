using BILL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace MIMID
{
    public partial class RegisterUser : Form
    {
        private ConexionService conexionService = new ConexionService();
        private UserService userService = new UserService();
        public RegisterUser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //conexionService.prueba();
            UserValidator();
        }

        private User UserValidator()
        {
            // Lista de errores
            List<string> errores = new List<string>();

            // Validar cédula
            string cedula = txtCedula.Text;
            if (string.IsNullOrWhiteSpace(cedula) || !cedula.All(char.IsDigit))
            {
                errores.Add("La cédula solo puede contener números y no puede estar vacía.");
                MessageBox.Show("La cédula solo puede contener números y no puede estar vacía.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; // Detener la ejecución del método
            }

            // Validar nombre
            string nombre = txtNombre.Text;
            if (string.IsNullOrWhiteSpace(nombre) || !nombre.All(char.IsLetter))
            {
                errores.Add("El nombre solo puede contener letras y no puede estar vacío.");
                MessageBox.Show("El nombre solo puede contener letras y no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; // Detener la ejecución del método
            }
            else
            {
                nombre = char.ToUpper(nombre[0]) + nombre.Substring(1).ToLower();
            }

            // Validar apellido
            string apellido = txtApellido.Text;
            if (string.IsNullOrWhiteSpace(apellido) || !apellido.All(char.IsLetter))
            {
                errores.Add("El apellido solo puede contener letras y no puede estar vacío.");
                MessageBox.Show("El apellido solo puede contener letras y no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; // Detener la ejecución del método
            }
            else
            {
                apellido = char.ToUpper(apellido[0]) + apellido.Substring(1).ToLower();
            }

            // Validar teléfono
            string telefono = txtTelefono.Text;
            if (string.IsNullOrWhiteSpace(telefono) || !telefono.All(char.IsDigit))
            {
                errores.Add("El teléfono solo puede contener números y no puede estar vacío.");
                MessageBox.Show("El teléfono solo puede contener números y no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; // Detener la ejecución del método
            }

            // Validar correo electrónico
            string correo = txtCorreo.Text;
            if (string.IsNullOrWhiteSpace(correo) || !IsValidEmail(correo))
            {
                errores.Add("El correo electrónico no tiene un formato válido o está vacío.");
                MessageBox.Show("El correo electrónico no tiene un formato válido o está vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; // Detener la ejecución del método
            }

            // Validar contraseña
            if (txtClave.Text != txtCheckPassword.Text)
            {
                errores.Add("Las contraseñas no coinciden.");
                MessageBox.Show("Las contraseñas no coinciden.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; // Detener la ejecución del método
            }
            string password = txtClave.Text;

            // Si no hay errores, crear y retornar el objeto User
            DateTime FechaNacimiento = fechaNacimiento.Value;
            User user = new User
            {
                Id = cedula,
                FirstName = nombre,
                LastName = apellido,
                Phone = telefono,
                Gender = Sexo(),
                Email = correo,
                BirthDate = FechaNacimiento,
                Password = password
            };

            RegisterCheckedUser(user);
            return user;
        }

        private void RegisterCheckedUser(User user)
        {
            try
            {
                conexionService.Login(user);
                string resultadoUsuario = userService.Register(user);
                MessageBox.Show(resultadoUsuario);
                if (resultadoUsuario.IndexOf("error", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    MessageBox.Show("Miembro no guardado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Miembro guardado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar_Campos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Limpiar_Campos()
        {
            txtCedula.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            rdbtnHombre.Checked = false;
            rdbtnMujer.Checked = false;
            txtClave.Clear();
            txtCheckPassword.Clear();
        }

        private string Sexo()
        {
            if (rdbtnHombre.Checked)
            {
                return "Hombre";
            }
            if (rdbtnMujer.Checked)
            {
                return "Mujer";
            }
            return null;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
