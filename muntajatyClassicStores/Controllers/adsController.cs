using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.Http;
using muntajatyClassicStores.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace muntajatyClassicStores.Controllers
{
    public class adsController : Controller
    {
        //------------------------- نص الاتصال ------------------------------------
        static string Table = "ads";

        static public IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = firebaseConfiguration.AuthSecret,
            BasePath = firebaseConfiguration.BasePath
        };
        static public IFirebaseClient Client = new FireSharp.FirebaseClient(config);
        //----------------------------------------------------

        public async Task<ActionResult> insert(ads new_item, IFormFile file, IFormFile transferMoney)
        {
            //try
            //{
            //string path = Path.Combine(Server.MapPath("~/Uploads"), file.FileName);
            //file.SaveAs(path);
            //new_item.img.image = file.FileName;
            //}
            //catch
            //{
            //}

            //try
            //{
            //string path_2 = Path.Combine(Server.MapPath("~/Uploads"), transferMoney.FileName);
            //fileUploadViewModel fileUpload = new fileUploadViewModel()
            //{
            //    file = file
            //};
            
            //new_item.img.image = await firebaseStorageController.abood(fileUpload);
            new_item.img.image = "https://firebasestorage.googleapis.com/v0/b/muntajatyclassic.appspot.com/o/banner%2Fbanner_7.PNG?alt=media&token=8a2cddd8-bf5f-460b-b1c5-531880da5c7c";




            new_item.person.transferMoney = "";
            //}
            //catch
            //{
            //    new_item.person.transferMoney = "https://firebasestorage.googleapis.com/v0/b/muntajatyclassic.appspot.com/o/banner%2Fbanner_7.PNG?alt=media&token=8a2cddd8-bf5f-460b-b1c5-531880da5c7c";
            //}

            new_item.img.startDate = DateTime.Now;
            new_item.img.endDate = DateTime.Now.AddDays(5);
            new_item.img.order = DateTime.Now.Second;
            new_item.img.block = true;
            PushResponse message = await Client.PushTaskAsync(Table, new_item);
            new_item.id = message.Result.Name;
            SetResponse setResponse = await Client.SetTaskAsync(Table + "/" + new_item.id, new_item);
            telegramNotificationController.notificationByTelegramForAds(new_item.person.fullName, new_item.person.mobileNumber, new_item.person.email);
            ViewBag.success = "لقد تمت إضافة الإعلان بنجاح";
            return Redirect("~/Home/Index");
        }


        static public async Task<List<ads>> retrieveAllItems()
        {
            var Data = await Client.GetTaskAsync(Table);
            List<ads> _list = new List<ads>();
            if (Data.Body != "null")
            {
                try
                {
                    Dictionary<string, ads> List = Data.ResultAs<Dictionary<string, ads>>();
                    var list = List.Values.Where(a => a.img.block == false && a.img.endDate > DateTime.Now).OrderBy(a=> a.img.order ).ToList();
                    _list = list;
                }
                catch { }
            }
            return _list;
        }


        // GET: ads
        public ActionResult Index()
        {
            ViewBag.success = "";
            return View();
        }
    }
}