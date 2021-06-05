using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace muntajatyClassicStores.Models
{
    public class Prog_Info
    {
        //المعلومات الأساسية
        public string ID { get; set; }        
        public string Program_Name { get; set; }
        public string Program_slogan { get; set; }
        public string Program_Eng_Name { get; set; }
        public string Program_Eng_slogan { get; set; }
        public string Version { get; set; }
        public string Program_About { get; set; }
        public string Program_Eng_About { get; set; }
        public string Name_and_slogn { get { string name_andslogn = Program_Name + " - " + Program_slogan; return name_andslogn; } }

        //ملعومات الاتصال
        public string Technical_Name { get; set; }
        public string Technical_Email { get; set; }
        public string Technical_Mobile_No { get; set; }
        public string Telephone_No { get; set; }
        public string Technical_Period { get; set; }
        public string Technical_Presence { get; set; }
        public string website { get; set; }
        public string Facebook_Link { get; set; }
        public string Twitter_Link { get; set; }
        public string Instagram_Link { get; set; }
        public string Whatsapp_Link { get; set; }
        public string Telegram_Link { get; set; }
        //روابط التطبيقات
        public string Web_Url { get; set; }
        public string AppStore_Url { get; set; }
        public string PlayStore_Url { get; set; }
        public string WindowsAppliction_url { get; set; }
        public string MacAppliction_url { get; set; }
        public string Upgrade_url { get; set; }
        public string Request_Data_url { get; set; }

        //السياسات
        public string Privacy_Policy { get; set; }
        public string Usage_Policy { get; set; }//سياسة الاستخدام
        //التحديثات
        public string Update_Name { get; set; }
        public string Update_Url { get; set; }
        public string Update_Features { get; set; }
        public string Update_Extension { get; set; }

        //الشروط والأحكام
        public string Terms_and_Conditions { get; set; }//الشروط والأحكام
        public string Terms_and_Conditions_eng { get; set; }//الشروط والأحكام

        //المساعدة
        public string FQA_Link { get; set; }//رابط المساعدة
        public string Program_LearnVideo_Link { get; set; }
        public string Program_LearnBook_Link { get; set; }
        public DateTime UpdateDate { get; set; }
        public int Trial_Days { get; set; }
        public string Trial_MSG { get; set; }
        public string Trial_URL { get; set; }
    }
}