using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Product
    {
        public string Reference { get; set; }
        public string HealthReg { get; set; }
        public string BarCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }//Descripciones y contraindicaciones
        public string Category { get; set; }
        public string PresentationType { get; set; }
        public string Dose { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Lote { get; set; }
        public Boolean Prescription { get; set; }
        public string SoldType { get; set; }
        public string Manufacturer { get; set; }
        public string StorageConditions { get; set; }
        public string Ubication { get; set; }
        public string Supplier { get; set; }
        public DateTime LastPurchase { get; set; }
        public DateTime LastSale { get; set; }
        public int PurchasePrice { get; set; }
        public int SalePrice { get; set; }
        //Tener en cuenta IVA para el futuro
        public int Stock { get; set; }
        public byte[] Foto { get; set; }

        public Product() { }
        public Product(
            string reference,
            string healthReg,
            string barCode,
            string name,
            string description,
            string category,
            string presentationType,
            string dose,
            DateTime expirationDate,
            string lote,
            bool prescription,
            string soldType,
            string manufacturer,
            string storageConditions,
            string ubication,
            string supplier,
            DateTime lastPurchase,
            DateTime lastSale,
            int purchasePrice,
            int salePrice,
            int stock,
            byte[] foto
        )
        {
            Reference = reference;
            HealthReg = healthReg;
            BarCode = barCode;
            Name = name;
            Description = description;
            Category = category;
            PresentationType = presentationType;
            Dose = dose;
            ExpirationDate = expirationDate;
            Lote = lote;
            Prescription = prescription;
            SoldType = soldType;
            Manufacturer = manufacturer;
            StorageConditions = storageConditions;
            Ubication = ubication;
            Supplier = supplier;
            LastPurchase = lastPurchase;
            LastSale = lastSale;
            PurchasePrice = purchasePrice;
            SalePrice = salePrice;
            Stock = stock;
            Foto = foto;
        }
    }
}
