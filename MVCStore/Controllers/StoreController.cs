using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCStore.Controllers
{
    public class StoreController : Controller
    {
        //
        // GET: /Store/

        public string Index()
        {
            return Server.HtmlEncode("Hello from store");
        }

        public string Browse(string type) {
            return Server.HtmlEncode("Hello from browse" + type);
        }

        public string Details()
        {
            return Server.HtmlEncode("Hello from details");
        }
    }
}
