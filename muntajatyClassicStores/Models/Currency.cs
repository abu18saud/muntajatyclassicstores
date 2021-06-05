using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace muntajatyClassicStores.Models
{
    public class Currency
    {
        public string ID { get; set; }
        public string Currency_ar { get; set; }
        public string Currency_en { get; set; }
        public string Code { get; set; }
        public decimal Exchange_Rate { get; set; }
    }
}