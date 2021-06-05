using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace muntajatyClassicStores.Models
{
    public class Social_Media
    {
        public string ID { get; set; }
        public SocType Type { get; set; }
        public string Link { get; set; }
        public String Description { get; set; }
        public string Create_By { get; set; }
        public DateTime Create_Date { get; set; }
        public string Modify_By { get; set; }
        public DateTime Modify_Date { get; set; }
        public string prefixWithUsername { get { string link = Type.Prefix + Link; return link; } }
    }
}