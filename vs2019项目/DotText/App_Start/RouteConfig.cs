using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DotText
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}"); //屏蔽一些地址
            //路由配置
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}", //正常的url路径,去匹配url路径中的值,该路径可以调整顺序,先访问动作,再访问控制器,不过一般是先访问控制器
                defaults: new { controller = "TestMVC", action = "Index", id = UrlParameter.Optional } //默认的路由路径，如果没有写路径，就会调用该路径
            );
        }
    }
}
