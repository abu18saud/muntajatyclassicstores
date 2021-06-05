using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace muntajatyClassicStores.Models
{
    public class PaymentMode
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Eng_Name { get; set; }
        public string Notes { get; set; }
        public bool Block { get; set; }
    }
}