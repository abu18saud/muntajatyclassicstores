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
    public class StoresController : Controller
    {
        static string Table = "Stores";

        static public IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = firebaseConfiguration.AuthSecret,
            BasePath = firebaseConfiguration.BasePath
        };
        static public IFirebaseClient Client = new FireSharp.FirebaseClient(config);
        //----------------------------------------------------
        static public async Task<Store> retrieveOneItem(string ID)
        {
            Store Store = new Store();
            var Data = await Client.GetTaskAsync(Table);
            if (Data.Body != "null")
            {
                try
                {
                    Dictionary<string, Store> List = Data.ResultAs<Dictionary<string, Store>>();
                    var list = List.Values.Where(a => a.ID == ID).ToList();
                    Store = list[0];
                }
                catch { }
            }
            return Store;
        }
        static public async Task<List<Store>> retrieveAllItems()
        {
            var Data = await Client.GetTaskAsync(Table);
            List<Store> _list = new List<Store>();
            if (Data.Body != "null")
            {
                try
                {
                    Dictionary<string, Store> List = Data.ResultAs<Dictionary<string, Store>>();
                    var list = List.Values.Where(a => a.Online_Store == true && a.Block == false).ToList();
                    _list = list;
                }
                catch { }
            }
            return _list;
        }
        static public async void Update(Store item)
        {
            try
            {
                FirebaseResponse response = await Client.UpdateTaskAsync(Table + "/" + item.ID, item);
                //if (message == true)
                //MessageBox_.ShowSuccess("تم بنجاح", "لقد تم التعديل بنجاح");
            }
            catch
            {
                //if (message == true)
                //MessageBox_.ShowError("هناك خطأ", "هناك خطأ غريب!");
            }
        }
        static public async Task<List<Store>> _search(string searchWord)
        {
            List<Store> _list = new List<Store>();

            if (searchWord.Trim() != string.Empty)
            {
                var Data = await Client.GetTaskAsync(Table);
                if (Data.Body != "null")
                {
                    try
                    {
                        Dictionary<string, Store> List = Data.ResultAs<Dictionary<string, Store>>();
                        _list = List.Values.Where(a => a.YourCompanyName_Ar.Contains(searchWord)
                            || a.YourCompanyName_En.Contains(searchWord)
                            //|| a.OwnerName.Contains(searchWord)
                            //|| a.Owner_Offical_ID.Contains(searchWord)
                            || a.Email.Contains(searchWord)
                            || a.MobileNO.Contains(searchWord)
                            || a.CR_Number.Contains(searchWord)
                            //|| a.ID.Contains(searchWord)
                            || a.Create_By.Contains(searchWord)).ToList();
                    }
                    catch { }
                    finally { }
                }
            }
            else
            {
            }

            return _list;
        }

        //----------------------------------------------------

        // GET: Stores
        public async Task<ActionResult> Index()
        {
            ViewBag.progInfo = await HomeController.retrieveLastOne();
            return View();
        }

        // GET: Stores/Details/5
        public async Task<ActionResult> Details(string id, string category, string searchWord)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = await retrieveOneItem(id);
            ViewBag.progInfo = await HomeController.retrieveLastOne();
            ViewBag.socialMedias = await Social_MediaController.retrieveAllItems(id);
            if (string.IsNullOrEmpty(searchWord) == false)
            {
                ViewBag.productsServices = await Products_ServicesController.search(id, searchWord);
                ViewBag.searchWord = searchWord;
            }
            else
            {
                ViewBag.productsServices = await Products_ServicesController.categoryFilter(id, category);
            }
            ViewBag.categories = await categoriesController.retrieveAllItems(id);
            ViewBag.category = category;

            ViewBag.stores = await StoresController.retrieveAllItems();


            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        public async Task<ActionResult> search(string searchWord)
        {
            List<Store> list = await _search(searchWord);

            ViewBag.progInfo = await HomeController.retrieveLastOne();
            ViewBag.storesList = list;
            ViewBag.searchWord = searchWord;
            ViewBag.storesCount = list.Count;

            return View(list);
        }

        //---------------------------------------------------- #Cart
        [HttpPost]
        public async Task<ActionResult> addToCart(string productId, string storeId, int count, decimal vatPercentage)
        {
            Products_Services product = await Products_ServicesController.retrieveOneItem(productId.Trim(), storeId.Trim());
            if (Session["cart_" + storeId] == null)
            {
                List<Bill_Item> cart = new List<Bill_Item>();

                Bill_Item bill_Item = new Bill_Item()
                {
                    ID = product.ID,
                    Name = product.Name,
                    Eng_Name = product.Eng_Name,
                    Quantity = count,
                    type = product.Type,
                    Barcode = product.Barcode,
                    WholesalePrice = product.WholesalePrice,
                    Selling_Price = product.SellingPrice,
                    VAT_Percent = vatPercentage,
                    Stock = product.Stock,
                    Notes = "بواسطة متاجر منتجاتي"
                };
                cart.Add(bill_Item);
                Session["cart_" + storeId] = cart;
            }
            else
            {
                List<Bill_Item> cart = (List<Bill_Item>)Session["cart_" + storeId];

                foreach (var item in cart)
                {
                    if (item.ID == productId)
                    {
                        cart.Remove(item);
                        int prevQty = item.Quantity;
                        cart.Add(new Bill_Item()
                        {
                            ID = product.ID,
                            Name = product.Name,
                            Eng_Name = product.Eng_Name,
                            type = product.Type,
                            Barcode = product.Barcode,
                            WholesalePrice = product.WholesalePrice,
                            Selling_Price = product.SellingPrice,
                            VAT_Percent = vatPercentage,
                            Stock = product.Stock,
                            Notes = "بواسطة متاجر منتجاتي",
                            Quantity = prevQty + count
                        });
                        break;
                    }
                    else
                    {
                        cart.Add(new Bill_Item()
                        {
                            ID = product.ID,
                            Name = product.Name,
                            Eng_Name = product.Eng_Name,
                            Quantity = count,
                            type = product.Type,
                            Barcode = product.Barcode,
                            WholesalePrice = product.WholesalePrice,
                            Selling_Price = product.SellingPrice,
                            VAT_Percent = vatPercentage,
                            Stock = product.Stock,
                            Notes = "بواسطة متاجر منتجاتي"
                        });
                        break;
                    }
                }
                Session["cart_" + storeId] = cart;
            }


            ViewBag.progInfo = await HomeController.retrieveLastOne();
            return Redirect("~/Stores/Details/" + storeId);
        }

        //[HttpPost]
        public async Task<ActionResult> removeFromCart(string productId, string storeId)
        {
            List<Bill_Item> cart = (List<Bill_Item>)Session["cart_" + storeId];

            foreach (var item in cart)
            {
                if (item.ID == productId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["cart_" + storeId] = cart;
            //Products_Services product = await Products_ServicesController.retrieveOneItem(productId.Trim(), storeId.Trim());

            ViewBag.progInfo = await HomeController.retrieveLastOne();
            return Redirect("~/Stores/Details/" + storeId);
        }


        public async Task<ActionResult> clearCart(string storeId)
        {
            List<Bill_Item> cart = (List<Bill_Item>)Session["cart_" + storeId];

            cart = null;

            //foreach (var item in cart)
            //{
            //    if (item.ID == productId)
            //    {
            //        cart.Remove(item);
            //        break;
            //    }
            //}
            Session["cart_" + storeId] = cart;
            //Products_Services product = await Products_ServicesController.retrieveOneItem(productId.Trim(), storeId.Trim());

            ViewBag.progInfo = await HomeController.retrieveLastOne();
            return Redirect("~/Stores/Details/" + storeId);
        }
    }
}
