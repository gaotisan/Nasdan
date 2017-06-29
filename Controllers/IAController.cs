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
            return View();
        }

      
    }
}
