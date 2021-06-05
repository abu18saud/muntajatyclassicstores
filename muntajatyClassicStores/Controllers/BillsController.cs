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
    public class BillsController : Controller
    {
        static string Table = "Stores/ID/Bills";

        static public IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = firebaseConfiguration.AuthSecret,
            BasePath = firebaseConfiguration.BasePath
        };
        static public IFirebaseClient Client = new FireSharp.FirebaseClient(config);
        //----------------------------------------------------
        [HttpPost]
        public async Task<ActionResult> Insert(
            string storeId,
            int totalQuantity,
            decimal totalSummation,
            decimal vatPercent,
            string firstName,
            string lastName,
            string _mobileNo,
            string delivery
            )
        {            
            Store store = await StoresController.retrieveOneItem(storeId);
            List<Bill_Item> cart = (List<Bill_Item>)Session["cart_" + storeId];

            Bill bill = new Bill();
            bill.Total_Discount = 0;
            bill.Total_Quantity = totalQuantity;
            bill.Total_Summation = totalSummation;
            bill.VAT_Percent = vatPercent;
            if (cart != null)
            {
                bill.Items = cart;
                if (delivery != "on")
                {
                    bill.Items.Add(new Bill_Item()
                    {
                        Barcode = "0",
                        Name = $"التوصيل ({store.delivery.companyOrperson})",
                        Eng_Name = "Delivery",
                        ID = "",
                        Notes = $"التوصيل بواسطة ({store.delivery.companyOrperson})",
                        Quantity = 1,
                        Selling_Price = store.delivery.cost,
                        WholesalePrice = 0,
                        Stock = 40,
                        type = new Items_Type() { ID = "", Name = "توصيل", Eng_Name = "Delivery", Incream = true, Order = 1 },
                        VAT_Percent = 0
                    });
                }

            }

            bill.status = new Status() { ID = "-MH9_2aA3U96PeT4hOvt", status_ar = "عبر الإنترنت", status_en = "Online" };
            //bill.ID = storeId;
            //-----------------------------
            //Customer customer = await customerController.retrieveOneItemByMobileNo(mobileNo, storeId);

            Customer customer = new Customer()
            {
                First_Name = firstName,
                Middle_Name = "",
                Last_Name = lastName,
                Mobile_No = _mobileNo,
                Email = "",
                Gender = new Gender() { gen_Ar = "ذكر", gen_En = "Male", ID = "0" },
                Address = "",
                City = new City() { ID = "0", Name_Ar = "الأحساء", Name_En = "" },
                Country = new Country() { CountryName_ar = "المملكة العربية السعودية", CountryName_en = "" },
                DOB = DateTime.Now.AddYears(-30),
                ID = "0",
                Description = "",
                Create_By = "Online",
                Create_Date = DateTime.Now,
                Modify_By = "Online",
                Modify_Date = DateTime.Now,
                Zip_Code = "",
                Picture = "",
                Picture_File_Name = "",
            };
            customerController.Insert(customer, storeId);



            bill.Customer = customer;

            //----------------------------
            bill.Create_By = "Online";
            bill.Create_Date = DateTime.Now;
            //----------------------------
            bill.Modify_By = "Online";
            bill.Modify_Date = DateTime.Now;
            bill.Date = DateTime.Now;
            bill.currency = new Currency() { Code = "SR", Currency_ar = "الريال السعودي", Currency_en = "Saudi Riyal", Exchange_Rate = 3, ID = "0" };
            bill.Payment_Type = new PaymentMode() { ID = "-MFttNSVPJQUl32dfWMA", Name = "نقداً", Eng_Name = "Cash", Notes = "عند الاستلام" };


            //الإستعلام عن إذا ما كان موجوداً مسبقاً
            //if (bill.Total_Quantity != 0)
            //{
            //Serial Number
            store.Bill_Serial_No = store.Bill_Serial_No + 1;
            StoresController.Update(store);
            // ------------End----------------------
            PushResponse message = await Client.PushTaskAsync(Table.Replace("ID", storeId), bill);//
            bill.ID = message.Result.Name;
            bill.Serial_No = store.Bill_Serial_No;
            SetResponse setResponse = await Client.SetTaskAsync(Table.Replace("ID", storeId) + "/" + bill.ID, bill);
            await telegramNotificationController.notificationByTelegramForBill(storeId, bill.ID);
            ViewBag.success = "لقد تمت الإضافة بنجاح";
            //}
            return Redirect("~/Stores/Details/" + storeId);
        }

        static public async Task<Bill> retrieveOneItem(string billId, string storeId)
        {
            Bill Bill = new Bill();

            var Data = await Client.GetTaskAsync(Table.Replace("ID", storeId));
            if (Data.Body != "null")
            {
                try
                {
                    Dictionary<string, Bill> List = Data.ResultAs<Dictionary<string, Bill>>();
                    var list = List.Values.Where(a => a.ID == billId).ToList();
                    Bill = list[0];
                }
                catch { }
            }
            return Bill;
        }
        // GET: Bills
        public ActionResult Index()
        {
            return View();
        }
    }
}
