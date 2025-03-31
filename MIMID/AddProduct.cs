using BILL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MIMID
{
    public partial class AddProduct : Form
    {
        private ProductService productoService = new ProductService();
        public event EventHandler OnRegresar;
        private byte[] imageBytes;
        private Product productoRecibido;
        public AddProduct(Product newProduct)
        {
            InitializeComponent();
            this.productoRecibido = newProduct;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                Limpiar_Campos();
            }
        }

        private bool ValidarCampos()
        {
            // Lista de errores
            List<string> errores = new List<string>();

            // Validar referencia
            string referencia = txtReferencia.Text.Trim();
            if (string.IsNullOrWhiteSpace(referencia) || !referencia.All(char.IsDigit))
            {
                errores.Add("La referencia debe contener solo números y no puede estar vacía.");
                MessageBox.Show("La referencia debe contener solo números y no puede estar vacía.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Detener la ejecución del método
            }

            // Validar nombre
            string nombre = txtNombre.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombre) || !nombre.All(char.IsLetter))
            {
                errores.Add("El nombre debe contener solo letras y no puede estar vacío.");
                MessageBox.Show("El nombre debe contener solo letras y no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Detener la ejecución del método
            }

            // Validar descripción
            string descripcion = txtDescription.Text.Trim();
            if (string.IsNullOrWhiteSpace(descripcion) || !descripcion.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
            {
                errores.Add("La descripción debe contener solo letras y no puede estar vacía.");
                MessageBox.Show("La descripción debe contener solo letras y no puede estar vacía.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Detener la ejecución del método
            }

            // Validar precio de compra
            string valorCompraTexto = txtPurchase.Text.Trim();
            if (!int.TryParse(valorCompraTexto, out int valorCompra))
            {
                errores.Add("El precio de compra debe ser un número válido y no puede estar vacío.");
                MessageBox.Show("El precio de compra debe ser un número válido y no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Detener la ejecución del método
            }

            // Validar precio de venta
            string valorVentaTexto = txtSale.Text.Trim();
            if (!int.TryParse(valorVentaTexto, out int valorVenta))
            {
                errores.Add("El precio de venta debe ser un número válido y no puede estar vacío.");
                MessageBox.Show("El precio de venta debe ser un número válido y no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Detener la ejecución del método
            }

            // Validar cantidad
            string cantidadTexto = txtStock.Text.Trim();
            if (!int.TryParse(cantidadTexto, out int cantidad))
            {
                errores.Add("La cantidad debe ser un número válido y no puede estar vacía.");
                MessageBox.Show("La cantidad debe ser un número válido y no puede estar vacía.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Detener la ejecución del método
            }

            // Validar otros campos
            string healthReg = txtReg.Text.Trim();
            string barCode = lblBarCode.Text.Trim();
            string category = cbCategory.Text.Trim();
            string presentationType = cbPresentation.Text.Trim();
            string dose = txtDose.Text.Trim();
            DateTime expirationDate = dpExpiration.Value;
            string lote = txtLote.Text.Trim();
            bool prescription = txtPrescription.Text.Trim().ToLower() == "true";
            string soldType = cbSoldType.Text.Trim();
            string manufacturer = txtManufacturer.Text.Trim();
            string storageConditions = txtStorage.Text.Trim();
            string ubication = txtUbication.Text.Trim();
            string supplier = txtSupplier.Text.Trim();

            // Si no hay errores, crear y registrar el producto
            Product producto = new Product
            {
                Reference = referencia,
                HealthReg = healthReg,
                BarCode = barCode,
                Name = nombre,
                Description = descripcion,
                Category = category,
                PresentationType = presentationType,
                Dose = dose,
                ExpirationDate = expirationDate,
                Lote = lote,
                Prescription = prescription,
                SoldType = soldType,
                Manufacturer = manufacturer,
                StorageConditions = storageConditions,
                Ubication = ubication,
                Supplier = supplier,
                LastPurchase = DateTime.Now,
                LastSale = DateTime.Now,
                PurchasePrice = valorCompra,
                SalePrice = valorVenta,
                Stock = cantidad,
                Foto = imageBytes
            };

            MessageBox.Show(productoService.Register(producto));
            return true;
        }

        private void CargarDatosProducto()
        {
            if (productoRecibido != null)
            {
                txtReferencia.Text = productoRecibido.Reference;
                txtReg.Text = productoRecibido.HealthReg;
                lblBarCode.Text = productoRecibido.BarCode;
                txtNombre.Text = productoRecibido.Name;
                txtDescription.Text = productoRecibido.Description;
                cbCategory.Text = productoRecibido.Category;
                cbPresentation.Text = productoRecibido.PresentationType;
                txtDose.Text = productoRecibido.Dose;
                dpExpiration.Value = productoRecibido.ExpirationDate;
                txtLote.Text = productoRecibido.Lote;
                txtPrescription.Text = productoRecibido.Prescription.ToString();
                cbSoldType.Text = productoRecibido.SoldType;
                txtManufacturer.Text = productoRecibido.Manufacturer;
                txtStorage.Text = productoRecibido.StorageConditions;
                txtUbication.Text = productoRecibido.Ubication;
                txtSupplier.Text = productoRecibido.Supplier;
                txtPurchase.Text = productoRecibido.PurchasePrice.ToString();
                txtSale.Text = productoRecibido.SalePrice.ToString();
                txtStock.Text = productoRecibido.Stock.ToString();
                if (productoRecibido.Foto != null)
                {
                    Image image = Image.FromStream(new MemoryStream(productoRecibido.Foto));
                    int nuevoAncho = 175;
                    int nuevoAlto = 175;
                    Image imagenRedimensionada = new Bitmap(image, nuevoAncho, nuevoAlto);
                    pbFoto.Image = imagenRedimensionada;
                }
                imageBytes = productoRecibido.Foto;
            }
        }

        private void Limpiar_Campos()
        {
            txtReferencia.Clear();
            txtReg.Clear();
            lblBarCode.Text = string.Empty;
            txtNombre.Clear();
            txtDescription.Clear();
            cbCategory.SelectedIndex = -1;
            cbPresentation.SelectedIndex = -1;
            txtDose.Clear();
            dpExpiration.Value = DateTime.Now;
            txtLote.Clear();
            txtPrescription.Clear();
            cbSoldType.SelectedIndex = -1;
            txtManufacturer.Clear();
            txtStorage.Clear();
            txtUbication.Clear();
            txtSupplier.Clear();
            txtPurchase.Clear();
            txtSale.Clear();
            txtStock.Clear();
            imageBytes = null;
            pbFoto.Image = null;
        }

        /*
        private void actualizarProductoBD()
        {
            string referencia = txtReferencia.Text;
            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;
            int valor = int.Parse(txtPrecio.Text);
            int cantidad = int.Parse(txtCantidad.Text);
            Producto producto = new Producto(
                referencia, nombre, descripcion, valor, cantidad, imageBytes
            );

            MessageBox.Show(productoService.ActualizarProducto(producto));
        }


        private void Btnregresar_Click(object sender, EventArgs e)
        {
            OnRegresar?.Invoke(this, EventArgs.Empty);
        }


        public void AsignarCam7os()
        {
            if (productoRecibido != null)
            {
                txtReferencia.Text = productoRecibido.Reference;
                txtReferencia.Enabled = false;
                txtNombre.Text = productoRecibido.Name;
                txtDescripcion.Text = productoRecibido.Description;
                txtPrecio.Text = productoRecibido.PurchasePrice.ToString();
                txtCantidad.Text = productoRecibido.Stock.ToString();
                imageBytes = productoRecibido.Foto;
                // Asignar la foto si existe
                byte[] foto = productoRecibido.Foto;
                if (foto != null)
                {
                    Image image = Image.FromStream(new MemoryStream(foto));
                    int nuevoAncho = 175;
                    int nuevoAlto = 175;
                    Image imagenRedimensionada = new Bitmap(image, nuevoAncho, nuevoAlto);
                    pbFoto.Image = imagenRedimensionada;
                }
                else
                {
                    pbFoto.Image = null;
                }
            }
            else
            {
                pbFoto.Image = null;
            }
        }


        private void btnSubirfoto_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                //Asegúrate de que este diálogo solo se muestre una vez
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Obtener la ruta del archivo seleccionado
                    string filePath = openFileDialog.FileName;
                    imageBytes = File.ReadAllBytes(filePath);
                    MessageBox.Show("Imagen seleccionada: " + filePath);
                }
            }
            byte[] foto = imageBytes;
            if (foto != null)
            {
                Image image = Image.FromStream(new MemoryStream(foto));

                int nuevoAncho = 175;
                int nuevoAlto = 175;
                Image imagenRedimensionada = new Bitmap(image, nuevoAncho, nuevoAlto);

                pbFoto.Image = imagenRedimensionada;
            }
            else
            {
                pbFoto.Image = null;
            }
        }
        */

    }
}
