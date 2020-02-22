namespace GeletoCarDealer.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class VehiclesController : Controller
    {
        public IActionResult GetAll()
        {
            return this.View("Vehicles");
        }

        public IActionResult Info()
        {
            return this.View();
        }
    }
}
