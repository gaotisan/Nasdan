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
            //Cogeremos el último estimulo recibido y abrieremos esa vista
            ViewData["IAActive"] = "active";
            ViewData["BlogActive"] = "";
            return View("Seeing");
        }
        public IActionResult Seeing()
        {
            ViewData["IAActive"] = "active";
            ViewData["BlogActive"] = "";
            return View();
        }

        public IActionResult Terminal()
        {
            ViewData["IAActive"] = "active";
            ViewData["BlogActive"] = "";
            return View();
        }


        public IActionResult Learning()
        {
            ViewData["IAActive"] = "active";
            ViewData["BlogActive"] = "";
            return View();
        }

        public IActionResult Monitoring()
        {
            ViewData["IAActive"] = "active";
            ViewData["BlogActive"] = "";
            return View();
        }


      
    }
}
