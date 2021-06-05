using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FireSharp.Config;
using FireSharp.Interfaces;
using muntajatyClassicStores.Models;

namespace muntajatyClassicStores.Controllers
{
    public class Social_MediaController : Controller
    {
        static string Table = "Stores/ID/Social_Medias";

        static public IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = firebaseConfiguration.AuthSecret,
            BasePath = firebaseConfiguration.BasePath
        };
        static public IFirebaseClient Client = new FireSharp.FirebaseClient(config);
        //------------------------------------------
        static public async Task<List<Social_Media>> retrieveAllItems(string id)
        {
            var Data = await Client.GetTaskAsync(Table.Replace("ID", id));
            List<Social_Media> list = new List<Social_Media>();
            if (Data.Body != "null")
            {
                try
                {
                    Dictionary<string, Social_Media> List = Data.ResultAs<Dictionary<string, Social_Media>>();
                    list = List.Values.ToList();
                }
                catch { }
            }
            return list;
        }


        public async Task<List<Social_Media>> retrieveAllItems_(string id)
        {
            var Data = await Client.GetTaskAsync(Table.Replace("ID", id));
            List<Social_Media> list = new List<Social_Media>();
            if (Data.Body != "null")
            {
                try
                {
                    Dictionary<string, Social_Media> List = Data.ResultAs<Dictionary<string, Social_Media>>();
                    list = List.Values.ToList();
                }
                catch { }
            }
            ViewBag.socMediaList____ = list;

            return list;
        }
        //------------------------------------------

        // GET: Social_Media
        public ActionResult Index()
        {
            return View();
        }
    }
}