using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace muntajatyClassicStores.Models
{
    public class Customer
    {
        public string ID { get; set; }
        [Required]
        public string First_Name { get; set; }
        public string Middle_Name { get; set; }
        [Required]
        public string Last_Name { get; set; }
        public string Full_Name { get { var Full = (First_Name + " " + Middle_Name + " " + Last_Name).Trim(); return Full.Trim(); } }
        public Gender Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Zip_Code { get; set; }
        public Country Country { get; set; }
        public City City { get; set; }
        public string Address { get; set; }
        [Required]
        public string Mobile_No { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Picture { get; set; }
        public string Picture_File_Name { get; set; }
        public String Description { get; set; }
        public string Create_By { get; set; }
        public DateTime Create_Date { get; set; }
        public string Modify_By { get; set; }
        public DateTime Modify_Date { get; set; }
    }
}