using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace muntajatyClassicStores.Models
{
    public class Bill_Item
    {
        public string ID { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public string Arabic_Name_With_Type { get { var total = $"{Name} ({type.Name})"; return total; } }
        public string English_Name_With_Type { get { var total = $"{Eng_Name} ({type.Eng_Name})"; return total; } }
        public string Eng_Name { get; set; }
        public decimal WholesalePrice { get; set; }
        public decimal Selling_Price { get; set; }
        public decimal VAT_Percent { get; set; }
        public decimal Profit { get { var profit = (Selling_Price - WholesalePrice) * Quantity; return profit; } }//الربح
        public int Quantity { get; set; }
        public int Stock { get; set; }
        public decimal Total { get { var total = Convert.ToDecimal(Selling_Price * Quantity); return total; } }
        public decimal Total_with_VATs { get { var _total = Total + (Total * VAT_Percent / 100); return Convert.ToDecimal(_total); } }
        public decimal VAT { get { var vat = Convert.ToDecimal(Total_with_VATs) - Total; return vat; } }

        public decimal Total_Import { get { var total = Convert.ToDecimal(WholesalePrice * Quantity); return total; } }
        public decimal Total_Import_with_VAT { get { var total = (Convert.ToDecimal(WholesalePrice * Quantity) * VAT_Percent) / 100; return total; } }
        public int due_Qty { get { var total = Convert.ToInt32(Stock - Quantity); return total; } }
        public Items_Type type { get; set; }
        public string Notes { get; set; }
    }
}