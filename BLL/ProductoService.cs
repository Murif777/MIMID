using DAL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BILL
{
    public class ProductService : ICrud<Product>
    {
        ProductRepository productRepository = new ProductRepository();

        public string Register(Product product)
        {
            try
            {
                return productRepository.SaveProduct(product);
            }
            catch (Exception ex)
            {
                return "Error registering product: " + ex.Message;
            }
        }

        public List<Product> GetAll()
        {
            try
            {
                return productRepository.GetAllProducts();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting products: " + ex.Message);
                return null;
            }
        }

        public List<Product> GetByReference(string reference)
        {
            try
            {
                return productRepository.GetProductsByReference(reference);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting product by reference: " + ex.Message);
                return null;
            }
        }

        public string UpdateStock(Product product)
        {
            try
            {
                return productRepository.UpdateStock(product);
            }
            catch (Exception ex)
            {
                return "Error updating stock: " + ex.Message;
            }
        }

        public string DeleteProduct(string productReference)
        {
            return productRepository.DeleteProduct(productReference);
        }

        public string UpdateProduct(Product product)
        {
            try
            {
                return productRepository.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                return "Error updating product: " + ex.Message;
            }
        }

        public string GetStockByReference(string reference)
        {
            int stock = productRepository.GetStockByReference(reference);
            return stock.ToString();
        }

        public string GetUnitPriceByReference(string productReference)
        {
            return productRepository.GetUnitPriceByReference(productReference).ToString("F2");
        }
    }
}
