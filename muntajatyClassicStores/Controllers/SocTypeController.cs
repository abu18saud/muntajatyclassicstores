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
    public class SocTypeController : Controller
    {
        static string Table = "SocTypes";

        static public IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = firebaseConfiguration.AuthSecret,
            BasePath = firebaseConfiguration.BasePath
        };
        static public IFirebaseClient Client = new FireSharp.FirebaseClient(config);
        //------------------------------------------
        static public async Task<SocType> retrieveOneItem(string ID)
        {
            SocType SocType = new SocType();

            var Data = await Client.GetTaskAsync(Table);
            if (Data.Body != "null")
            {
                try
                {
                    Dictionary<string, SocType> List = Data.ResultAs<Dictionary<string, SocType>>();
                    var list = List.Values.Where(a => a.ID == ID).ToList();
                    SocType = list[0];
                }
                catch { }
            }
            return SocType;
        }
        static public async Task<List<SocType>> retrieveAllItemsForMuliItem(List<Social_Media> list)
        {
            List<SocType> socTypes = new List<SocType>();

            foreach(Social_Media socialMedia in list)
            {
                socTypes.Add(await retrieveOneItem(socialMedia.Type.ID));
            }
            return socTypes;
        }
        //------------------------------------------
        // GET: SocType
        public ActionResult Index()
        {
            return View();
        }
    }
}