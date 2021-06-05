using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace muntajatyClassicStores.Models
{
    public class person
    {
        [Required]
        [Display(Name = "اسم مقدّم الطلب:")]
        public string fullName { get; set; }
        [Required]
        [Display(Name = "رقم الجوال:")]
        public string mobileNumber { get; set; }
        [Required]
        [Display(Name = "البريد الإلكتروني:")]
        public string email { get; set; }
        [Required]
        [Display(Name = "صورة من الحوالة:")]
        public string transferMoney { get; set; }
    }
}