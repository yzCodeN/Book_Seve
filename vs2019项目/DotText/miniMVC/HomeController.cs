using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotText.miniMVC
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index(SimpleModel model)
        {
            Action<TextWriter> callback = writer =>
            {
                writer.Write(String.Format(
                    "Controller:{0}<br/> Action:{1}<br/><br/>",
                    model.Controller, model.Action));
                writer.Write(String.Format(
                    "Foo:{0}<br/> Bar:{1} <br/> Baz:{2}",
                    model.Foo, model.Bar, model.Baz));
            };
            return new RawContentResult(callback);
        }
    }
}