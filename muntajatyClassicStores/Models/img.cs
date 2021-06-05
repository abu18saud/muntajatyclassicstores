using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace muntajatyClassicStores.Models
{
    public class img
    {
        [Required]
        [Display(Name = "صورة البنر:")]
        public string image { get; set; }
        [Display(Name = "الترتيب:")]
        public int order { get; set; }
        [Display(Name = "وصف رابط الإعلان:")]
        public string description { get; set; }
        [Display(Name = "رابط مستهدف:")]
        public string link { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public bool block { get; set; }
    }
}