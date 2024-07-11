using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tamagotchi.Areas.Admin.Controllers
{
    [Authorize]
    public class AuthorizeBaseController : Controller
    {
    }
}
