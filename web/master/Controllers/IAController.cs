using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Nasdan.Controllers
{
    public class IAController : Controller
    {
        public IActionResult Index()
        {            
            ViewData["IAActive"] = "active";
            ViewData["BlogActive"] = "";
            //var h = new library.Hello();
            //var t = h.GetTomHanks();
            var r = new library.Test();
            var tom = r.GetTomHanks();
            return View();
        }

    }
}
