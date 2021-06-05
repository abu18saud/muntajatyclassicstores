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
    public class Products_ServicesController : Controller
    {
        //------------------------- نص الاتصال ------------------------------------
        static string Table = "Stores/ID/Products_Services";

        static public IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = firebaseConfiguration.AuthSecret,
            BasePath = firebaseConfiguration.BasePath
        };
        static public IFirebaseClient Client = new FireSharp.FirebaseClient(config);
        //----------------------------------------------------
        static public async Task<int> Count(string id)
        {
            int i = 0;
            var Data = await Client.GetTaskAsync(Table.Replace("ID", id));
            if (Data.Body != "null")
            {
                Dictionary<string, Products_Services> items = Data.ResultAs<Dictionary<string, Products_Services>>();
                var list = items.ToList();
                i = list.Count;
            }
            return i;
        }
        static public async Task<Products_Services> retrieveOneItem(string id, string storeId)
        {
            Products_Services Products_Services = new Products_Services();

            var Data = await Client.GetTaskAsync(Table.Replace("ID", storeId));
            if (Data.Body != "null")
            {
                try
                {
                    Dictionary<string, Products_Services> List = Data.ResultAs<Dictionary<string, Products_Services>>();
                    var list = List.Values.Where(a => a.ID == id).ToList();
                    Products_Services = list[0];
                }
                catch { }
            }
            return Products_Services;
        }
        static public async Task<List<Products_Services>> retrieveAllItems(string id)
        {
            var Data = await Client.GetTaskAsync(Table.Replace("ID", id));
            List<Products_Services> _list = new List<Products_Services>();
            if (Data.Body != "null")
            {
                try
                {
                    Dictionary<string, Products_Services> List = Data.ResultAs<Dictionary<string, Products_Services>>();
                    var list = List.Values.Where(a => a.Stock != 0).OrderBy(a => a.Category.Category_Name).ToList();
                    _list = list;
                }
                catch { }
            }
            return _list;
        }


        static public async Task<List<Products_Services>> categoryFilter(string storeId, string searchWord)
        {
            List<Products_Services> list = new List<Products_Services>();
            if (string.IsNullOrEmpty(searchWord) == false)
            {
                var Data = await Client.GetTaskAsync(Table.Replace("ID", storeId));
                if (Data.Body != "null")
                {
                    try
                    {
                        Dictionary<string, Products_Services> List = Data.ResultAs<Dictionary<string, Products_Services>>();
                        list = List.Values.Where(
                            a => a.Category.Category_Name.Contains(searchWord) //a.Barcode.Contains(searchWord)
                            && a.Stock != 0
                            //|| a.Name.Contains(searchWord)
                            //|| a.Eng_Name.Contains(searchWord)
                            //|| a.Description.Contains(searchWord)
                            //|| a.Category.Category_Name.Contains(searchWord)
                            //|| a.Industry.CountryName_ar.Contains(searchWord)
                            ).OrderByDescending(a=> a.Modify_Date).ToList();
                    }
                    catch
                    {
                    }
                }
            }
            else
            {
                list = await retrieveAllItems(storeId);
            }
            return list;
        }

        static public async Task<List<Products_Services>> search(string storeId, string searchWord)
        {
            List<Products_Services> list = new List<Products_Services>();
            if (string.IsNullOrEmpty(searchWord) == false)
            {
                var Data = await Client.GetTaskAsync(Table.Replace("ID", storeId));
                if (Data.Body != "null")
                {
                    try
                    {
                        Dictionary<string, Products_Services> List = Data.ResultAs<Dictionary<string, Products_Services>>();
                        list = List.Values.Where(
                            a => a.Barcode.Contains(searchWord)
                            || a.Name.Contains(searchWord)
                            || a.Eng_Name.Contains(searchWord)
                            || a.Description.Contains(searchWord)
                            || a.Category.Category_Name.Contains(searchWord)
                            || a.Industry.CountryName_ar.Contains(searchWord)
                            && a.Stock != 0
                            ).ToList();
                    }
                    catch
                    {
                    }
                }
            }
            else
            {
                list = await retrieveAllItems(storeId);
            }
            return list;
        }



        // GET: Products_Services
        public ActionResult Index()
        {
            return View();
        }
    }
}