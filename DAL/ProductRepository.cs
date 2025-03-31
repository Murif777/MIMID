using ENTITY;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ProductRepository : ConexionBD
    {
        public string SaveProduct(Product product)
        {
            string sql = "INSERT INTO productos(Reference, HealthReg, BarCode, Name, Description, Category, PresentationType, Dose, ExpirationDate, Lote, Prescription, SoldType, Manufacturer, StorageConditions, Ubication, Supplier, LastPurchase, LastSale, PurchasePrice, SalePrice, Stock, Foto) " +
                  "VALUES (@Reference, @HealthReg, @BarCode, @Name, @Description, @Category, @PresentationType, @Dose, @ExpirationDate, @Lote, @Prescription, @SoldType, @Manufacturer, @StorageConditions, @Ubication, @Supplier, @LastPurchase, @LastSale, @PurchasePrice, @SalePrice, @Stock, @Foto)";
            var conexionBd = conexionBD();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBd);
                comando.Parameters.AddWithValue("@Reference", product.Reference);
                comando.Parameters.AddWithValue("@HealthReg", product.HealthReg);
                comando.Parameters.AddWithValue("@BarCode", product.BarCode);
                comando.Parameters.AddWithValue("@Name", product.Name);
                comando.Parameters.AddWithValue("@Description", product.Description);
                comando.Parameters.AddWithValue("@Category", product.Category);
                comando.Parameters.AddWithValue("@PresentationType", product.PresentationType);
                comando.Parameters.AddWithValue("@Dose", product.Dose);
                comando.Parameters.AddWithValue("@ExpirationDate", product.ExpirationDate);
                comando.Parameters.AddWithValue("@Lote", product.Lote);
                comando.Parameters.AddWithValue("@Prescription", product.Prescription);
                comando.Parameters.AddWithValue("@SoldType", product.SoldType);
                comando.Parameters.AddWithValue("@Manufacturer", product.Manufacturer);
                comando.Parameters.AddWithValue("@StorageConditions", product.StorageConditions);
                comando.Parameters.AddWithValue("@Ubication", product.Ubication);
                comando.Parameters.AddWithValue("@Supplier", product.Supplier);
                comando.Parameters.AddWithValue("@LastPurchase", product.LastPurchase);
                comando.Parameters.AddWithValue("@LastSale", product.LastSale);
                comando.Parameters.AddWithValue("@PurchasePrice", product.PurchasePrice);
                comando.Parameters.AddWithValue("@SalePrice", product.SalePrice);
                comando.Parameters.AddWithValue("@Stock", product.Stock);
                comando.Parameters.AddWithValue("@Foto", product.Foto);
                var res = comando.ExecuteNonQuery();
                if (res == 0)
                {
                    return "Product not saved";
                }
                if (res != 0)
                {
                    return "Product saved";
                }
            }
            catch (MySqlException ex)
            {
                return "Error saving product: " + ex.Message;
            }
            finally
            {
                conexionBd.Close();
            }
            return null;
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            string ssql = "SELECT * FROM productos";

            var conexionBd = conexionBD();

            try
            {
                MySqlCommand comando = new MySqlCommand(ssql, conexionBd);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(Map(reader));
                }
                if (products == null)
                {
                    Console.WriteLine("Empty list");
                    return null;
                }
                return products;
            }
            catch (MySqlException)
            {
                return null;
            }
            finally
            {
                conexionBd.Close();
            }
        }

        public string UpdateStock(Product product)
        {
            var conexionBd = conexionBD();

            string sql = "UPDATE Productos SET Stock = @Stock WHERE Reference = @Reference";

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBd);
                comando.Parameters.AddWithValue("@Stock", product.Stock);
                comando.Parameters.AddWithValue("@Reference", product.Reference);
                comando.ExecuteNonQuery();
                return "Stock updated";
            }
            catch (MySqlException ex)
            {
                return "Error updating stock: " + ex.Message;
            }
            finally
            {
                conexionBd.Close();
            }
        }

        public List<Product> GetProductsByReference(string reference)
        {
            List<Product> products = new List<Product>();

            string ssql = $"SELECT * FROM productos WHERE Reference = @Reference";

            var conexionBd = conexionBD();

            try
            {
                MySqlCommand comando = new MySqlCommand(ssql, conexionBd);
                comando.Parameters.AddWithValue("@Reference", reference);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(Map(reader));
                }
                if (products == null)
                {
                    Console.WriteLine("Empty list");
                    return null;
                }
                return products;
            }
            catch (MySqlException)
            {
                return null;
            }
            finally
            {
                conexionBd.Close();
            }
        }

        private Product Map(MySqlDataReader reader)
        {
            Product product = new Product
            {
                Reference = reader.GetString("Reference"),
                HealthReg = reader.GetString("HealthReg"),
                BarCode = reader.GetString("BarCode"),
                Name = reader.GetString("Name"),
                Description = reader.GetString("Description"),
                Category = reader.GetString("Category"),
                PresentationType = reader.GetString("PresentationType"),
                Dose = reader.GetString("Dose"),
                ExpirationDate = reader.GetDateTime("ExpirationDate"),
                Lote = reader.GetString("Lote"),
                Prescription = reader.GetBoolean("Prescription"),
                SoldType = reader.GetString("SoldType"),
                Manufacturer = reader.GetString("Manufacturer"),
                StorageConditions = reader.GetString("StorageConditions"),
                Ubication = reader.GetString("Ubication"),
                Supplier = reader.GetString("Supplier"),
                LastPurchase = reader.GetDateTime("LastPurchase"),
                LastSale = reader.GetDateTime("LastSale"),
                PurchasePrice = reader.GetInt32("PurchasePrice"),
                SalePrice = reader.GetInt32("SalePrice"),
                Stock = reader.GetInt32("Stock")
            };

            if (!reader.IsDBNull(reader.GetOrdinal("Foto")))
            {
                product.Foto = (byte[])reader["Foto"];
            }

            return product;
        }

        public string DeleteProduct(string productReference)
        {
            string sql = "DELETE FROM Productos WHERE Reference = @Reference";

            using (var conexionBd = conexionBD())
            {
                using (MySqlTransaction transaction = conexionBd.BeginTransaction())
                {
                    try
                    {
                        using (MySqlCommand comando = new MySqlCommand(sql, conexionBd, transaction))
                        {
                            comando.Parameters.AddWithValue("@Reference", productReference);
                            comando.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return "Product deleted successfully";
                    }
                    catch (MySqlException ex)
                    {
                        transaction.Rollback();
                        return "Error deleting product: " + ex.Message;
                    }
                    finally
                    {
                        conexionBd.Close();
                    }
                }
            }
        }

        public string UpdateProduct(Product product)
        {
            string query = "UPDATE Productos SET HealthReg=@HealthReg, BarCode=@BarCode, Name=@Name, Description=@Description, Category=@Category, PresentationType=@PresentationType, Dose=@Dose, ExpirationDate=@ExpirationDate, Lote=@Lote, Prescription=@Prescription, SoldType=@SoldType, Manufacturer=@Manufacturer, StorageConditions=@StorageConditions, Ubication=@Ubication, Supplier=@Supplier, LastPurchase=@LastPurchase, LastSale=@LastSale, PurchasePrice=@PurchasePrice, SalePrice=@SalePrice, Stock=@Stock, Foto=@Foto WHERE Reference=@Reference";
            var conexionBd = conexionBD();
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conexionBd);
                cmd.Parameters.AddWithValue("@Reference", product.Reference);
                cmd.Parameters.AddWithValue("@HealthReg", product.HealthReg);
                cmd.Parameters.AddWithValue("@BarCode", product.BarCode);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@Category", product.Category);
                cmd.Parameters.AddWithValue("@PresentationType", product.PresentationType);
                cmd.Parameters.AddWithValue("@Dose", product.Dose);
                cmd.Parameters.AddWithValue("@ExpirationDate", product.ExpirationDate);
                cmd.Parameters.AddWithValue("@Lote", product.Lote);
                cmd.Parameters.AddWithValue("@Prescription", product.Prescription);
                cmd.Parameters.AddWithValue("@SoldType", product.SoldType);
                cmd.Parameters.AddWithValue("@Manufacturer", product.Manufacturer);
                cmd.Parameters.AddWithValue("@StorageConditions", product.StorageConditions);
                cmd.Parameters.AddWithValue("@Ubication", product.Ubication);
                cmd.Parameters.AddWithValue("@Supplier", product.Supplier);
                cmd.Parameters.AddWithValue("@LastPurchase", product.LastPurchase);
                cmd.Parameters.AddWithValue("@LastSale", product.LastSale);
                cmd.Parameters.AddWithValue("@PurchasePrice", product.PurchasePrice);
                cmd.Parameters.AddWithValue("@SalePrice", product.SalePrice);
                cmd.Parameters.AddWithValue("@Stock", product.Stock);
                cmd.Parameters.AddWithValue("@Foto", product.Foto);
                int res = cmd.ExecuteNonQuery();
                if (res == 0)
                {
                    return "Product not updated";
                }
                return "Product updated successfully";
            }
            catch (MySqlException ex)
            {
                return "Error updating product: " + ex.Message;
            }
            finally
            {
                conexionBd.Close();
            }
        }

        public int GetStockByReference(string productReference)
        {
            int stock = 0;

            string query = "SELECT Stock FROM Productos WHERE Reference = @Reference";

            using (var conexionBd = conexionBD())
            {
                MySqlCommand command = new MySqlCommand(query, conexionBd);
                command.Parameters.AddWithValue("@Reference", productReference);

                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    stock = Convert.ToInt32(result);
                }
            }

            return stock;
        }

        public decimal GetUnitPriceByReference(string productReference)
        {
            decimal unitPrice = 0m;

            string query = "SELECT SalePrice FROM Productos WHERE Reference = @Reference";

            using (var conexionBd = conexionBD())
            {
                MySqlCommand command = new MySqlCommand(query, conexionBd);
                command.Parameters.AddWithValue("@Reference", productReference);

                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    unitPrice = Convert.ToDecimal(result);
                }
            }

            return unitPrice;
        }
    }
}
