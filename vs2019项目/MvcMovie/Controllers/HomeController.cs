using MvcMovie.Filters;
using MvcMovie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMovie.Controllers
{
    [MyExHandlettribute]
    public class HomeController : Controller
    {
        //用来测试下拉框的枚举
        public enum ListEnum{
            age,
            id,
            name,
            num,
            RMB
        }
        //实例化一个student类的集合，并给其赋值
        List<StudentModel> students = new List<StudentModel>() {
                //实例化一个Student类对其进行初始化
                //集合的内部是student类，所以其中的值也只能放Student类
                new StudentModel(){ Name="天天",Sex="man",Age=20 },
                new StudentModel(){ Name="南天",Sex="man",Age=20 },
                new StudentModel(){ Name="楠楠",Sex="girl",Age=20 }
        };
        //使用从数据库获取的实体框架
        //EF查询操作
        //1.创建上下文对象
        testEntities Tef = new testEntities();   //创建的实体类
        [LoginFilter(isCheck = false)]  //表示该Action无需使用筛选器
        public ActionResult Login()
        {
            ViewBag.Info = students.Select(t => t.Name).ToList();  //自定义的数据类
            ViewBag.SqlInfo = Tef.Customer.Where(t => !String.IsNullOrEmpty(t.Name)).Select(t => t.Name).ToList(); //通过EF从数据库带出的数据           
            return View();
        }
        //创建一个Action来处理用户登录信息
        //从前台获取用户输入的信息
        //两种接收方式：
        //1.通过表单集合接收    使用FormCollection类实例来进行调用，该类可以获取form表单中的全部元素，通过Name属性来区别每一个元素
        //2.单个信息接收        直接在形参中定义来使用，名称要和前台标签的Name属性名称一致
        [LoginFilter(isCheck = false)]
        public ActionResult CommitData(/*FormCollection fc*/string Name, string ID)
        {
            //string Name = fc["Name"];
            //string ID = fc["ID"];
            //处理用户登录的请求
            //if(Name=="天天" && ID=="123")
            //{
            //    return Content("<script>alert('登录成功！')</script>");  //Content表示输出一句话,也可以是JS
            //}
            //else
            //{
            //    return Content("<script>alert('账号不正确！');window.location.href='/Home/Index'</script> ");
            //};

            //获取到数据库中的值来进行比较，如果有返回值，总数就大于0
            int Index = Tef.Customer.Where(t => t.Name == Name && t.ID == ID).Count();
            if (Index > 0)
            {
                Session["Name"] = Name;
                return View("Index"); //同一个文件夹下的视图可以使用return View找到该视图
                //不同文件夹下的视图如何跳转？使用 /文件夹/视图
                //return Redirect("/Movies/Index"); 
                //return Content("<script>alert('登录成功！');window.location.href='/Home/Index'</script>");
            }
            else
            {
                return View("Login");
                //return Content("<script>alert('账号不正确！');window.location.href='/Home/Login'</script> ");
            }
        }
        public ActionResult Index()
        {
            return View();
        }     
        public string testLinq()
        {
            //Linq查询
            //查询表达式写法
            //取出其中每一个类
            var stuUser = from user in students
                          select user;
            //查询方法写法
            var stuUserA = students.Select(t => t);

            //取出类中需要的值
            var stuUserVal = from user in students
                             select user.Name;
            var stuUserValA = students.Select(t => t.Name).ToArray();
            //按条件取出不同的值
            var stuUserWhere = from user in students
                               where user.Name.Contains("天")
                               select user;
            var stuUserWhereA = students.Where(t => t.Name.Contains("天")).Select(t => t.Name).ToArray();
            //创建别名,匿名类型，使用new关键字加上大括号
            var nickName = students.Select(t => new { 姓名 = t.Name }).ToList();
            //按性别进行分组
            //var sexGroup = (from stu in students
            //               group stu by stu.Sex into stuT     //将查询排序后的数据放入另一个表中存储
            //               select new { key = stuT.Key, count = stuT.Count() }).ToList();     //创建一个匿名类型，用来保存表中属性，Key值表示用来排序的字段
            var sexGroup = (from stu in students
                            group stu by new { stu.Sex, stu.Age } into stuT   //如果同时使用多个字段来进行排序
                            select new { Agekey = stuT.Key.Age, Sexkey = stuT.Key.Sex }).ToList(); //那么可以直接使用Key值去点出各个字段来进行赋值
            string Data = "";  //定义一个用来保存返回数据的字符串
            for (int i = 0; i < sexGroup.Count; i++)  //将key值循环添加进字符串
            {
                Data += sexGroup[i].Agekey + "\r\n";
            }
            //分组后，取出其中的值
            var GroupVal = (from stu in students
                            group stu by stu.Sex into stuT  //排序后，为多维数组，将其再次使用from拆分成一维数组
                            from item in stuT
                            select new { item.Name, item.Sex, Age = item.Age }).ToList();
            //上面的最后一句查询，原本的写法为Age = item.Age这种写法，但是编译器可以自行推断出属性的名字，一般推断的名字和本来的字段名相同，如果有特殊情况也可以自定义名称
            string tabData = "";
            for (int i = 0; i < GroupVal.Count; i++) //每输出一行数据，使用 || 分隔，最后一行不添加
            {
                tabData += GroupVal[i].Name + "\r\n";
                tabData += GroupVal[i].Sex + "\r\n";
                tabData += GroupVal[i].Age + "\r\n";
                if (i < GroupVal.Count - 1)
                {
                    tabData += "|| \r\n";
                }
            }

            return tabData;
        }
        public ActionResult CustInfo()
        {
            return View();
        }
        //EasyUi读取该方法
        public ActionResult GetInfo()
        {
            var list = Tef.Customer.ToList(); //将整张表转换为List
            return Json(list);  //转换为JSON传给前台
        }
        public ActionResult EDeleteByType(string id)
        {
            #region EF删除
            //找到在实体表中的该字段
            //也可以理解为找到表中相等的字段，返回一个对象
            var ReVal = Tef.Customer.FirstOrDefault(t => t.Type == id);
            if (ReVal != null)
            {
                //删除该对象
                Tef.Customer.Remove(ReVal);
            }

            //EF需要再保存一下该操作,保存操作并返回修改的条数
            var i = Tef.SaveChanges();
            //Content表示返回语句，可以写成JS
            return Content("<script>alert('删除了" + i + "条数据');window.location.href='/Home/Index'</script> ");
            #endregion

        }
        [HttpGet]
        public ActionResult EAddByType()
        {
            //自定义DropDownList的值
            //List<SelectListItem> listItem = new List<SelectListItem>();
            //listItem.Add(new SelectListItem { Text = "是", Value = "1" });
            //listItem.Add(new SelectListItem { Text = "否", Value = "0" });
            //ViewData["List"] = new SelectList(listItem, "Value", "Text", "");
            //从EF实体中取，也就是数据库中取
            var list = new SelectList(Tef.Customer, "Code", "Name");
            ViewBag.List = list;
            //从枚举类型中取
            ViewBag.Role = new SelectList(Enum.GetValues(typeof(ListEnum)));
            return View();
        }
        [HttpPost]
        //通过添加按钮进入该方法进行保存数据到数据库
        public ActionResult EAddByType([Bind(Include = "ID,Code,Name,Type,Contact,Phone,Region,Address,Business,Industry")] Customer cust)
        {
            //实例化数据库实体
            Tef.Customer.Add(cust);
            Tef.SaveChanges();           
            //从EF实体中取，也就是数据库中取
            var list = new SelectList(Tef.Customer, "Code", "Name");
            ViewBag.List = list;
            //从枚举类型中取
            ViewBag.Role = new SelectList(Enum.GetValues(typeof(ListEnum)));
            return RedirectToAction("Index");
        }
        public ActionResult EUpdateByType()
        {
            return Content("测试中~~~");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}