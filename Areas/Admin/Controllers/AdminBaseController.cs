using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tamagotchi.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminBaseController : Controller
    {
    }
}
