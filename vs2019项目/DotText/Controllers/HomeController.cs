using DotText.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotText.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        public ViewResult About()
        {
            ViewBag.Greeting = DateTime.Now.Hour < 12 ? "早上好" : "碗上号";
            return View();  //返回一个视图,可以在其中指定返回的视图
        }

        //相同的action,使用特性来区分
        [HttpGet]
        public ViewResult RsvpForm()
        {
            return View();
        }
        [HttpPost]
        public ViewResult RsvpForm(GuestResponse model)
        {
            if(ModelState.IsValid)  //用来判断Model中的模型是否符合规定,符合返回true
            {
                return View("Thanks", model);
            }
            else
            {
                return View();
            }
           
        }

    }
}