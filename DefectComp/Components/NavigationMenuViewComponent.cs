using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefectComp.Models;
using Microsoft.AspNetCore.Mvc;


namespace DefectComp.Components 
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedItem = RouteData?.Values["controller"];
            return View();
        }

    }
}
