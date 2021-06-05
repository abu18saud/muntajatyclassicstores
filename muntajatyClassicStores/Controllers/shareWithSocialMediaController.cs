using muntajatyClassicStores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Web.Mvc;

namespace muntajatyClassicStores.Controllers
{
    public class shareWithSocialMediaController : Controller
    {
        static public string sendToCustomer(string mobile)
        {
            string link = $"https://api.whatsapp.com/send?phone=966{mobile.Substring(1)}";
            return link;
        }
        public async Task<ActionResult> sendService(string id, string serviceName)
        {
            Store store = await Controllers.StoresController.retrieveOneItem(id);
            string salute = (DateTime.Now.Hour > 12) ? "مساء الخير 🌹 \n" : "صباح الخبر 🌹☀ \n";

            string contents = salute + $"السلام عليكم منسوبي متجر *{store.YourCompanyName_Ar}*  أنا فقط أود أن أتفاوض على سعر للخدمة أدناه:\n" + Environment.NewLine;
            contents += $"*الخدمة المطلوبة* 📜: {serviceName}" + Environment.NewLine;
            contents += "*تمت المراسلة عبر موقع متاجر منتجاتي* 🛒";
            string link = $"https://api.whatsapp.com/send?phone=966{store.MobileNO.Substring(1)}&text={contents.Replace("\n", "%0A")}";

            return Redirect(link);
        }

        public async Task<ActionResult> shareStoreByWhatsapp(string id, string pageLink)
        {
            Store store = await Controllers.StoresController.retrieveOneItem(id);
            string contents = "*تفضل بزيارة متجر " + store.YourCompanyName_Ar + " عبر متاجر منتجاتي 🛒* 😍" + Environment.NewLine;
            contents += "*هذا هو رابط المتجر 🔗:* " + Environment.NewLine + Request.Url.Host + pageLink + Environment.NewLine;
            contents += "*جوال المتجر 📱:* " + Environment.NewLine + store.MobileNO + Environment.NewLine;
            contents += "*بريد المتجر 📧:* " + Environment.NewLine + store.Email + Environment.NewLine + Environment.NewLine;
            contents += "تم نشر هذه الرسالة بواسطة موقع متاجر منتجاتي 🛒";
            string link = $"https://api.whatsapp.com/send?text={contents.Replace("\n", "%0A")}";

            return Redirect(link);
        }

        public async Task<ActionResult> shareStoreByTelegram(string id, string pageLink)
        {
            //string hashtag = "#متاجر_منتجاتي";
            Store store = await Controllers.StoresController.retrieveOneItem(id);
            string contents = "تفضل بزيارة متجر " + store.YourCompanyName_Ar + " عبر متاجر منتجاتي 🛒 😍" + Environment.NewLine;
            contents += "هذا هو رابط المتجر 🔗: " + Environment.NewLine + Request.Url.Host + pageLink + Environment.NewLine;
            contents += "جوال المتجر 📱: " + Environment.NewLine + store.MobileNO + Environment.NewLine;
            contents += "بريد المتجر 📧: " + Environment.NewLine + store.Email + Environment.NewLine + Environment.NewLine;
            contents += $"تم نشر هذه الرسالة بواسطة موقع متاجر منتجاتي 🛒";
            string link = $"https://telegram.me/share/url?url={Request.Url.Host + pageLink}&text={contents.Replace("\n", "%0A")}";
            return Redirect(link);
        }

        public async Task<ActionResult> shareStoreByTwitter(string id, string pageLink)
        {
            string hashtag = "#متاجر_منتجاتي";
            string title = "#فريق_فرسان_البرمجة";

            Store store = await Controllers.StoresController.retrieveOneItem(id);
            string contents = "تفضل بزيارة متجر " + store.YourCompanyName_Ar + " عبر متاجر منتجاتي 🛒 😍" + Environment.NewLine;
            contents += "هذا هو رابط المتجر 🔗: " + Environment.NewLine + Request.Url.Host + pageLink + Environment.NewLine;
            contents += "جوال المتجر 📱: " + Environment.NewLine + store.MobileNO + Environment.NewLine;
            contents += "بريد المتجر 📧: " + Environment.NewLine + store.Email + Environment.NewLine + Environment.NewLine;
            contents += $"تم نشر هذه الرسالة بواسطة موقع متاجر منتجاتي 🛒";
            string link = "https://twitter.com/intent/tweet?&text=" + contents.Replace("\n", "%0A") + "&hashtags=" + hashtag + "," + title + "&ref_src=twsrc%5Etfw";
            return Redirect(link);
        }

        public ActionResult shareStoreByFacebook(string pageLink)
        {
            //https://www.facebook.com/sharer.php?u=https%3A%2F%2Fwww.sharethis.com%2F

            string link = $"https://www.facebook.com/sharer.php?u={pageLink}";
            return Redirect(link);
        }

        public async Task<ActionResult> shareStoreBySMS(string id, string pageLink)
        {
            //https://sharethis.com/onboarding/
            Store store = await Controllers.StoresController.retrieveOneItem(id);
            string contents = "تفضل بزيارة متجر " + store.YourCompanyName_Ar + " عبر متاجر منتجاتي 🛒 😍" + Environment.NewLine;
            contents += "هذا هو رابط المتجر 🔗: " + Environment.NewLine + Request.Url.Host + pageLink + Environment.NewLine;
            contents += "جوال المتجر 📱: " + Environment.NewLine + store.MobileNO + Environment.NewLine;
            contents += "بريد المتجر 📧: " + Environment.NewLine + store.Email + Environment.NewLine + Environment.NewLine;
            contents += $"تم نشر هذه الرسالة بواسطة موقع متاجر منتجاتي 🛒";
            string link = $"sms:?body={contents.Replace("\n", "%0A")}";
            return Redirect(link);
        }

        public async Task<ActionResult> shareStoreByGmail(string id, string pageLink)
        {
            //https://sharethis.com/onboarding/
            Store store = await Controllers.StoresController.retrieveOneItem(id);
            string title = "تفضل بزيارة متجر " + store.YourCompanyName_Ar + " عبر متاجر منتجاتي 🛒 😍";
            string contents = "تفضل بزيارة متجر " + store.YourCompanyName_Ar + " عبر متاجر منتجاتي 🛒 😍" + Environment.NewLine;
            contents += "هذا هو رابط المتجر 🔗: " + Environment.NewLine + Request.Url.Host + pageLink + Environment.NewLine;
            contents += "جوال المتجر 📱: " + Environment.NewLine + store.MobileNO + Environment.NewLine;
            contents += "بريد المتجر 📧: " + Environment.NewLine + store.Email + Environment.NewLine + Environment.NewLine;
            contents += $"تم نشر هذه الرسالة بواسطة موقع متاجر منتجاتي 🛒";
            string link = $"https://mail.google.com/mail/u/0/?view=cm&to&su={title}&body={contents.Replace("\n", "%0A")}&bcc&cc&fs=1&tf=1";
            return Redirect(link);
        }
        //------------------------------------------------
        public async Task<string> notificationByTelegram(string id, string pageLink)
        {
            //string apilToken = "1341560558:AAFPHJladiixugKRYd7SN1vBmsCGcE_fPXc";
            string apilToken = "1691033961:AAGV48qYKz6eN_Vk-U1VD2BswaB-TE-rPBs";
            string destID = "7956856";
            //t.me/usinfobot
            //t.me/muntajatyClassicBot

            //https://api.telegram.org/bot1691033961:AAGV48qYKz6eN_Vk-U1VD2BswaB-TE-rPBs/sendMessage?chat_id=7956856&text=contents
            //string hashtag = "#متاجر_منتجاتي";
            Store store = await Controllers.StoresController.retrieveOneItem(id);
            string contents = "تفضل بزيارة متجر " + store.YourCompanyName_Ar + " عبر متاجر منتجاتي 🛒 😍" + Environment.NewLine;
            contents += "هذا هو رابط المتجر 🔗: " + Environment.NewLine + Request.Url.Host + pageLink + Environment.NewLine;
            contents += "جوال المتجر 📱: " + Environment.NewLine + store.MobileNO + Environment.NewLine;
            contents += "بريد المتجر 📧: " + Environment.NewLine + store.Email + Environment.NewLine + Environment.NewLine;
            contents += $"تم نشر هذه الرسالة بواسطة موقع متاجر منتجاتي 🛒";
            string link = $"https://api.telegram.org/bot{apilToken}/sendMessage?chat_id={destID}&text={contents.Replace("\n", "%0A")}";
            WebClient webclient = new WebClient();
            return webclient.DownloadString(link);
        }


    }
}