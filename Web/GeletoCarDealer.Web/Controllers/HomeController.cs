namespace GeletoCarDealer.Web.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using GeletoCarDealer.Data;
    using GeletoCarDealer.Data.Models.Models;
    using GeletoCarDealer.Services.Data;
    using GeletoCarDealer.Web.ViewModels;
    using GeletoCarDealer.Web.ViewModels.UsersArea.Vehicles;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IVehicleService vehicleService;

        public HomeController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            var viewModel = new AllVehiclesViewModel
            {
                Vehicles = await this.vehicleService.GetAll<VehiclesViewModel>(),
            };

            return this.View(viewModel);
        }

        [Route("contacts")]
        public IActionResult Contacts()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            var viewModel = this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });

            if (statusCode == 404)
            {
                return this.View("NotFound");
            }

            return this.View(viewModel);
        }
    }
}
