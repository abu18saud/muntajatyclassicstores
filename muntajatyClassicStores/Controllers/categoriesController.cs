using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FireSharp.Config;
using FireSharp.Interfaces;
using muntajatyClassicStores.Models;
using System.Windows;
using System.Text;
using FireSharp.Response;
using System.Windows.Forms;


namespace muntajatyClassicStores.Controllers
{
    public class categoriesController : Controller
    {
        //------------------------- نص الاتصال ------------------------------------
        static string Table = "Stores/ID/Categories";

        static ToolTip toolTip = new ToolTip();
        static public IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = firebaseConfiguration.AuthSecret,
            BasePath = firebaseConfiguration.BasePath
        };
        static public IFirebaseClient Client = new FireSharp.FirebaseClient(config);
        //----------------------------------------------------
        static public async Task<int> Count(string storeId)
        {
            int i = 0;
            var Data = await Client.GetTaskAsync(Table.Replace("ID",storeId));
            if (Data.Body != "null")
            {
                Dictionary<string, Category> items = Data.ResultAs<Dictionary<string, Category>>();
                var list = items.ToList();
                i = list.Count;
            }
            return i;
        }

        static public async Task<List<Category>> retrieveAllItems(string storeId)
        {
            var Data = await Client.GetTaskAsync(Table.Replace("ID",storeId));
            List<Category> list = new List<Category>();
            if (Data.Body != "null")
            {
                try
                {
                    Dictionary<string, Category> List = Data.ResultAs<Dictionary<string, Category>>();
                    list = List.Values.ToList();
                }
                catch { }
            }
            return list;
        }

        // GET: categories
        public ActionResult Index()
        {
            return View();
        }
    }
}