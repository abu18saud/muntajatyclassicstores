using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace muntajatyClassicStores.Models
{
    public class Bill
    {
        public string ID { get; set; }
        public int Serial_No { get; set; }
        public DateTime Date { get; set; }
        public Status status { get; set; }
        public Customer Customer { get; set; }
        public PaymentMode Payment_Type { get; set; }
        public decimal Total_Discount { get; set; }
        public decimal Amount_Paid { get; set; }
        public decimal Change_Due { get { var Total = (Total_with_VATs - Amount_Paid) * -1; return Total; } }
        public decimal Total_Summation { get; set; }
        public decimal VAT_Percent { get; set; }
        public decimal Total_with_VATs { get { var _total = Total_Summation + (Total_Summation * VAT_Percent / 100); return Convert.ToDecimal(_total); } }
        public decimal VAT { get { var vat = Convert.ToDecimal(Total_with_VATs) - Total_Summation; return vat; } }
        //public decimal Profit
        //{
        //    get
        //    {
        //        decimal profit = 0;
        //        if (Items.Count > 1)
        //        {
        //            foreach (Bill_Item item in Items)
        //            {
        //                profit += item.Profit;
        //            }
        //        }
        //        else
        //        {
        //            profit = Items[0].Profit;
        //        }

        //        return profit;
        //    }
        //}//الربح
        public int Total_Quantity { get; set; }
        //public IEnumerable<Products_Services> _Items { get; set; }
        public List<Bill_Item> Items { get; set; }
        public Currency currency { get; set; }
        public string Link_CopyOfBill { get; set; }
        public string Payment_Receipt { get; set; }
        public string Create_By { get; set; }
        public DateTime Create_Date { get; set; }
        public string Modify_By { get; set; }
        public DateTime Modify_Date { get; set; }
    }
}