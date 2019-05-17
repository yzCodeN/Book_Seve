using MvcMovie.Filters;
using System.Web;
using System.Web.Mvc;

namespace MvcMovie
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //添加一个自己的过滤给所有人
            filters.Add(new LoginFilter() { isCheck = true });   //isCheck默认为true，表示默认调用该筛选器
            filters.Add(new MyExHandlettribute());               //全局的异常筛选器
        }
    }
}
