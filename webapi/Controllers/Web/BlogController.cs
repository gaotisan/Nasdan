using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Nasdan.Controllers.Web
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            ViewData["IAActive"] = "";
            ViewData["BlogActive"] = "active";
            return View();
        }

    }
}
