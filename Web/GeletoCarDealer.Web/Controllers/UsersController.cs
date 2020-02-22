namespace GeletoCarDealer.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : Controller
    {
        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(string name)
        {
            return this.View();
        }
    }
}
