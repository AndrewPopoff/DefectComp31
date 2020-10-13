// меню проекта
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DefectComp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ViewResult Index() => View();
    }
}

