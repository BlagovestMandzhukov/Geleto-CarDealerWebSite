namespace GeletoCarDealer.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GeletoCarDealer.Data.Common.Repositories;
    using GeletoCarDealer.Data.Models;
    using GeletoCarDealer.Services.Data;
    using GeletoCarDealer.Web.ViewModels.UsersArea.Vehicles;
    using Microsoft.AspNetCore.Mvc;

    public class VehiclesController : BaseController
    {
        private readonly IVehicleService vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> GetAll(string orderBy, int category, string make, string model)
        {
            var viewModel = new AllVehiclesViewModel();

            viewModel = await this.vehicleService.GetAllFiltered(orderBy, category, make, model);
            return this.View("Vehicles", viewModel);
        }

        [Route("car/{id:int}")]
        public IActionResult ById(int id)
        {
            var vehicle = this.vehicleService.GetById<VehicleDetailsViewModel>(id);

            if (vehicle == null)
            {
                return this.NotFound();
            }

            return this.View(vehicle);
        }
    }
}
