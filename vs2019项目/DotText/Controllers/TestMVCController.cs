using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotText.Controllers
{
    public class TestMVCController : Controller
    {
        // GET: TestMVC
        public ActionResult Index()
        {
            return View();
        }
    }
}