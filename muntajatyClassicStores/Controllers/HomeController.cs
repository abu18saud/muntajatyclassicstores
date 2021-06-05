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
    public class HomeController : Controller
    {
        static string Table = "ProgInfo";

        static public IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = firebaseConfiguration.AuthSecret,
            BasePath = firebaseConfiguration.BasePath
        };
        static public IFirebaseClient Client = new FireSharp.FirebaseClient(config);
        //----------------------------------------------------
        static public async Task<Prog_Info> retrieveLastOne()
        {
            Prog_Info Prog_Info = new Prog_Info();

            var Data = await Client.GetTaskAsync(Table);
            if (Data.Body != "null")
            {
                try
                {
                    Dictionary<string, Prog_Info> List = Data.ResultAs<Dictionary<string, Prog_Info>>();
                    var list = List.Values.ToList();
                    Prog_Info = list.Last();
                }
                catch { }
            }
            return Prog_Info;
        }
        public async Task<ActionResult> Index()
        {
            ViewBag.progInfo = await retrieveLastOne();
            List<Store> _stores = await StoresController.retrieveAllItems();
            ViewBag.stores = _stores;
            ViewBag.ads = await adsController.retrieveAllItems();


            List<int> productsCount = new List<int>();

            foreach (Store store in _stores)
            {
                productsCount.Add(await Products_ServicesController.Count(store.ID));
            }

            ViewBag.productsCounts = productsCount;
            ViewBag._id = "-MEVYAVZNgbKgAgMvGOO";



            List<Social_Media> _list = await Social_MediaController.retrieveAllItems(ViewBag._id);
            ViewBag.socMediaList = _list;

            return View();
        }

        public ActionResult index_2()
        {

            return View();
        }
        public async Task<ActionResult> About()
        {
            Prog_Info Prog_Info = new Prog_Info();

            var Data = await Client.GetTaskAsync(Table);
            if (Data.Body != "null")
            {
                try
                {
                    Dictionary<string, Prog_Info> List = Data.ResultAs<Dictionary<string, Prog_Info>>();
                    var list = List.Values.ToList();
                    Prog_Info = list.Last();
                }
                catch { }
            }
            ViewBag.progInfo = await retrieveLastOne();

            //ViewBag.projectName = Prog_Info.Program_Name;
            //ViewBag.projectSlogan = Prog_Info.Program_slogan;
            //ViewBag.projectAbout = Prog_Info.Program_About;


            return View();
        }
        public async Task<ActionResult> Contact()
        {
            ViewBag.progInfo = await retrieveLastOne();

            return View();
        }
        public async Task<ActionResult> internalPolicy()
        {
            ViewBag.progInfo = await retrieveLastOne();

            return View();
        }
        public async Task<ActionResult> privatePolicy()
        {
            ViewBag.progInfo = await retrieveLastOne();

            return View();
        }
    }
}