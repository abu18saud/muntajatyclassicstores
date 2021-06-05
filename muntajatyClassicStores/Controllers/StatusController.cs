using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using muntajatyClassicStores.Models;
using FireSharp.Config;
using FireSharp.Interfaces;

namespace muntajatyClassicStores.Controllers
{
    public class StatusController : Controller
    {
        static string Table = "Statuses";

        static public IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = firebaseConfiguration.AuthSecret,
            BasePath = firebaseConfiguration.BasePath
        };
        static public IFirebaseClient Client = new FireSharp.FirebaseClient(config);
        //----------------------------------------------------
        static public async Task<Status> retrieveOneItemAr(string Ar)
        {
            Status Status = new Status();

            var Data = await Client.GetTaskAsync(Table);
            if (Data.Body != "null")
            {
                try
                {
                    Dictionary<string, Status> List = Data.ResultAs<Dictionary<string, Status>>();
                    var list = List.Values.Where(a => a.status_ar == Ar).ToList();
                    Status = list[0];
                }
                catch { }
            }
            return Status;
        }
        // GET: Status
        public ActionResult Index()
        {
            return View();
        }
    }
}