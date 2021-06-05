using muntajatyClassicStores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace muntajatyClassicStores.Controllers
{
    public class telegramNotificationController : Controller
    {
        static public async Task<string> notificationByTelegramForBill(string storeId, string billId)
        {
            string salute = (DateTime.Now.Hour > 12) ? "مساء الخير 🌹 \n" : "صباح الخبر 🌹☀ \n";
            Store store = await StoresController.retrieveOneItem(storeId);
            Bill bill = await BillsController.retrieveOneItem(billId, storeId);
            string chatId = store.telegramInfo.chatId;
            string link = "";

            try
            {
                string apilToken = "1691033961:AAGV48qYKz6eN_Vk-U1VD2BswaB-TE-rPBs";
                string contents = salute + "\n ______________________\n";
                contents += $"رقم الفاتورة 📇: {bill.Serial_No.ToString("D3") }\n";
                contents += $"تاريخ الفاتورة 📆: {bill.Create_Date.ToString("dd/MM/yyyy - hh:mm tt") }\n\n";
                contents += $"الأغراض المطلوبة 🛒:\n";
                string del = string.Empty;
                int i = 1;
                foreach (var item in bill.Items)
                {
                    if (item.Barcode != "0")
                    {
                        contents += $"{i} - {item.Arabic_Name_With_Type} - {item.English_Name_With_Type} - (الكمية {item.Quantity})\n";
                        del = "تجهيز في مقر المتجر 📍: "+ store.location.locationLink;
                    }
                    else
                    {
                        del = $"توصيل مقابل ({store.delivery.cost} ر.س) 🚚";
                    }
                    i++;
                }
                contents += $"______________________\n\n الحساب 💳:\n";
                contents += $"المجموع 🧮: {string.Format("{0:n}", bill.Total_Summation)} ر.س\n";
                contents += $"الضريبة 💸: {string.Format("{0:n}", bill.VAT)} ر.س\n";
                contents += $"المجموع شاملاً الضريبة 💰: {string.Format("{0:n}", bill.Total_with_VATs)} ر.س\n";
                contents += $"______________________\n\n معلومات الزبون 👨👩‍🦰:\n";
                contents += $"الاسم ✍: {bill.Customer.Full_Name}\n";
                contents += $"الجوال 📱: {bill.Customer.Mobile_No}\n";
                contents += $"طريقة الاستلام 📦: {del}\n";
                contents += $"التواصل عبر واتسآب 🔗:\n {shareWithSocialMediaController.sendToCustomer(bill.Customer.Mobile_No)}\n\n\n";
                contents += $"أعزائنا منسوبي متجر {store.YourCompanyName_Ar} نود إشعاركم بأنكم تلقيتم طلباً بواسطة موقع متاجر منتجاتي 🛒 \n تطوير فريق فرسان البرمجة 🐴";

                link = $"https://api.telegram.org/bot{apilToken}/sendMessage?chat_id={chatId}&text={contents.Replace("\n", "%0A")}";
                WebClient webclient = new WebClient();
                return webclient.DownloadString(link);
            }
            catch
            {
                return link;
            }
        }
        static public string notificationByTelegramForAds(string name, string mobileNo, string email)
        {
            string chatId = "-554604399";
            string salute = (DateTime.Now.Hour > 12) ? "مساء الخير 🌹 \n" : "صباح الخبر 🌹☀ \n";
            string link = "";
            try
            {
                string apilToken = "1691033961:AAGV48qYKz6eN_Vk-U1VD2BswaB-TE-rPBs";
                string contents = salute + "\n";
                contents += $"هناك شخص طلب إعلاناً على موقع متاجر منتجاتي: 📇\n";
                contents += $"______________________\n\n معلومات مقدّم الطلب 👨👩‍🦰:\n";
                contents += $"الاسم ✍: {name}\n";
                contents += $"الجوال 📱: {mobileNo}\n";
                contents += $"البريد الإلكتروني 📧: {email}\n";
                contents += $"التواصل عبر واتسآب 🔗:\n {shareWithSocialMediaController.sendToCustomer(mobileNo)}\n\n\n";
                contents += $"نود إشعاركم بأنكم تلقيتم طلب إعلان بواسطة موقع متاجر منتجاتي 🛒 \n تطوير فريق فرسان البرمجة 🐴";

                link = $"https://api.telegram.org/bot{apilToken}/sendMessage?chat_id={chatId}&text={contents.Replace("\n", "%0A")}";
                WebClient webclient = new WebClient();
                return webclient.DownloadString(link);
            }
            catch
            {
                return link;
            }
        }

        // GET: telegramNotification
        public ActionResult Index()
        {
            return View();
        }
    }
}