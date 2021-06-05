using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace muntajatyClassicStores.Models
{
    public class Store
    {
        //البيانات الأساسية
        public string ID { get; set; }
        //معلومات السجل التجاري
        public string OwnerName { get; set; }
        public string ID_ExpiryDate { get; set; }
        public string Owner_Offical_ID { get; set; }
        public string Activity { get; set; }
        public string CR_Number { get; set; }
        public string Issue_CR_Hijri_Date { get; set; }
        public City Issue_CR_City { get; set; }
        //----------------------------------------
        public DateTime Start_Subscription { get; set; }
        public DateTime EXP_Subscription { get; set; }
        public string YourCompanyName_Ar { get; set; }
        public string Slogan { get; set; }
        public BusinessType businessType { get; set; }
        public string YourCompanyName_En { get; set; }
        public string Slogan_En { get; set; }
        //المعلومات الضريبية
        public string VAT_Number { get; set; }
        public decimal VAT_Rate { get; set; }
        public decimal VAT_Rate_Import { get; set; }
        public DateTime Issue_VAT_Certificate { get; set; }
        //العنوان الوطني
        public Country country { get; set; }
        public Region region { get; set; }
        public City city { get; set; }
        public string Area { get; set; }
        public string Area_En { get; set; }
        public string StreetName_Ar { get; set; }
        public string StreetName_En { get; set; }
        public string PostalCode { get; set; }
        public string PO_Box { get; set; }
        public string AdditionalCode { get; set; }
        public string Full_Address
        {
            get
            {
                string area = (Area != null) ? Area : "";
                var full_address = country.CountryName_ar.Replace("السعودية", "المملكة العربية السعودية").Trim() + Environment.NewLine + region.Name_Ar + " - " + city.Name_Ar + " - " + area + " - " + StreetName_Ar;
                return full_address;
            }
        }
        public string Full_Address_En
        {
            get
            {
                string area_en = (Area_En != null) ? Area_En : "";
                var full_address = country.CountryName_en.Replace("Saudi Arabia", "Kingdom of Saudi Arabia").Trim() + Environment.NewLine + region.Name_En + " - " + city.Name_En + " - " + area_en + " - " + StreetName_En;
                return full_address;
            }
        }
        public location location { get; set; }
        //معلومات الاتصال
        public string MobileNO { get; set; }
        public string PhoneNO { get; set; }
        public string FaxNO { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        //صور هامة للشركة
        public string Stamp { get; set; }
        public string Logo { get; set; }
        public string Signture { get; set; }
        public string _Unique_Stamp { get; set; }
        public string _Unique_Logo { get; set; }
        public string _Unique_Signture { get; set; }
        //أطوال الصور
        public decimal width_stamp { get; set; }
        public decimal height_stamp { get; set; }
        public decimal width_logo { get; set; }
        public decimal height_logo { get; set; }
        public decimal width_signature { get; set; }
        public decimal height_signature { get; set; }
        //------------------------عدادات-----------------
        public int Expense_Serial_No { get; set; }
        public int Bill_Serial_No { get; set; }
        public int ImportGood_Serial_No { get; set; }
        public int Product_Service_Serial_No { get; set; }
        //------------------------الملاحظات---------------
        public string Payback_Note { get; set; }
        public string Footer_Note { get; set; }
        //-----------------------------------------------
        public bool Online_Store { get; set; }
        public string OnlineURL { get; set; }
        //-----------------------------------------------
        public delivery delivery { get; set; }
        //-----------------------------------------
        public bool Block { get; set; }
        //-----------------------------------------------
        public telegramInfo telegramInfo { get; set; }
        public string Create_By { get; set; }
        public DateTime Create_Date { get; set; }
        public string Modify_By { get; set; }
        public DateTime Modify_Date { get; set; }

    }
}