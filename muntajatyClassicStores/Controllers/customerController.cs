using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using muntajatyClassicStores.Models;

namespace muntajatyClassicStores.Controllers
{
    public class customerController : Controller
    {
        static string Table = "Stores/ID/Customers";

        static public IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = firebaseConfiguration.AuthSecret,
            BasePath = firebaseConfiguration.BasePath
        };
        static public IFirebaseClient Client = new FireSharp.FirebaseClient(config);
        //----------------------------------------------------
        static public async Task<Customer> retrieveOneItemByMobileNo(string Mobile, string storeId)
        {
            Customer Customer = new Customer();

            var Data = await Client.GetTaskAsync(Table.Replace("ID", storeId));
            if (Data.Body != "null")
            {
                try
                {
                    Dictionary<string, Customer> List = Data.ResultAs<Dictionary<string, Customer>>();
                    var list = List.Values.Where(a => a.Mobile_No == Mobile).ToList();
                    Customer = list[0];
                }
                catch { }
            }
            return Customer;
        }

        static public async void Insert(Customer new_item, string storeId)
        {
            //الإستعلام عن إذا ما كان موجوداً مسبقاً
            bool exist = await Exist(new_item, storeId);

            if (exist == false)
            {
                if (new_item.First_Name.Trim() != string.Empty)
                {
                    PushResponse message = await Client.PushTaskAsync(Table.Replace("ID", storeId), new_item);
                    new_item.ID = message.Result.Name;
                    SetResponse setResponse = await Client.SetTaskAsync(Table.Replace("ID", storeId) + "/" + new_item.ID, new_item);
                    //if (Message == true)
                    //    MessageBox_.ShowSuccess("تم بنجاح", "لقد تمت إضافة العنصر بنجاح");
                }
            }
            else
            {
                //if (Message == true)
                //    MessageBox_.ShowError("هناك خطأ", "هذا السجل مضاف مسبقاً");
            }

        }
        static public async Task<bool> Exist(Customer new_item, string storeId)
        {
            bool result = false;
            var Data = await Client.GetTaskAsync(Table.Replace("ID", storeId));
            //الإستعلام عن إذا ما كان موجوداً مسبقاً
            try
            {
                if (Data.Body != "null")
                {
                    Dictionary<string, Customer> items = Data.ResultAs<Dictionary<string, Customer>>();
                    var list = items.Where(a => a.Value.Mobile_No == new_item.Mobile_No).ToList();
                    if (list.Count == 0)
                    {
                        result = false;
                    }
                    else
                    {
                        result = true;
                    }
                }
            }
            catch
            {

            }
            return result;
        }






        // GET: customer
        public ActionResult Index()
        {
            return View();
        }
    }
}