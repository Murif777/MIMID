using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Factura
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Client Client { get; set; }
        public DateTime FechaFactura { get; set; }
        public List<Product> Products { get; set; }

        public Factura() { }
        public Factura(int id, User user, DateTime fechafactura, List<Product> products)
        {
            Id = id;
            User = user;
            FechaFactura = fechafactura;
            Products = products;
        }
    }
}
