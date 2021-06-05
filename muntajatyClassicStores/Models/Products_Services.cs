using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace muntajatyClassicStores.Models
{
    public class Products_Services
    {
        public string ID { get; set; }
        public int Serial_No { get; set; }
        public string Barcode { get; set; }
        public Items_Type Type { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
        public string Eng_Name { get; set; }
        public Country Industry { get; set; }
        public decimal WholesalePrice { get; set; }//سعر الجملة
        public decimal SellingPrice { get; set; }//سعر البيع
        public decimal Profit { get { var profit = SellingPrice - WholesalePrice; return profit; } }//الربح
        public decimal Discount { get; set; }
        public decimal VAT { get; set; }
        public int Stock { get; set; }
        public Brand Brand { get; set; }
        public string Picture_0 { get; set; }
        public string Picture_File_Name_0 { get; set; }
        public string Picture_1 { get; set; }
        public string Picture_File_Name_1 { get; set; }
        public string Picture_2 { get; set; }
        public string Picture_File_Name_2 { get; set; }
        public string Picture_3 { get; set; }
        public string Picture_File_Name_3 { get; set; }
        public String Description { get; set; }
        public int sale_count { get; set; }
        public bool Favorite { get; set; }
        public string Create_By { get; set; }
        public DateTime Create_Date { get; set; }
        public string Modify_By { get; set; }
        public DateTime Modify_Date { get; set; }
    }
}