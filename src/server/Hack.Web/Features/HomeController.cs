using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Hack.Web.Features
{
    public class HomeController:Controller
    {
        public ActionResult Index()
        {
            return File("~/index.html", "text/html");
        }
    }
}
