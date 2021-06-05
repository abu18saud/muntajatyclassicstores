using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace muntajatyClassicStores.Models
{
    public class Brand
    {
        public string ID { get; set; }
        public string Brand_Name { get; set; }
        public string Brand_Name_Eng { get; set; }
        public String Description { get; set; }
        public string Picture { get; set; }
        public string Create_By { get; set; }
        public DateTime Create_Date { get; set; }
        public string Modify_By { get; set; }
        public DateTime Modify_Date { get; set; }
    }
}