using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace muntajatyClassicStores.Models
{
    public class Category
    {
        public string ID { get; set; }
        public string Category_Name { get; set; }
        public string Category_Eng_Name { get; set; }
        public String Description { get; set; }
        public string Picture { get; set; }
        public string Create_By { get; set; }
        public DateTime Create_Date { get; set; }
        public string Modify_By { get; set; }
        public DateTime Modify_Date { get; set; }
    }
}