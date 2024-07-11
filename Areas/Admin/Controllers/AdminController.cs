using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tamagotchi.Areas.Admin.Controllers
{
    [Area("admin")]
    public class AdminController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Pets()
        {
            return View();
        }
    }
}
