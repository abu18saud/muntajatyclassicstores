using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace muntajatyClassicStores.Models
{
    public class location
    {
        public string locationX { get; set; }//Latitude
        public string locationY { get; set; }//Longitude
        public string googleMapUrl { get; set; }
        public string locationLink { get { string link = $"http://maps.google.com/maps?q={locationX.Trim()},{locationY.Trim()}"; return link; } }
    }
}