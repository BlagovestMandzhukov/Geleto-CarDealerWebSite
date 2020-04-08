namespace GeletoCarDealer.Web.Controllers
{
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

        public IActionResult GetAll([FromQuery]string orderBy = null, [FromQuery]int? category = null)
        {
            var viewModel = new AllVehiclesViewModel
            {
                Vehicles = this.vehicleService.GetAll<VehiclesViewModel>(orderBy, category),
            };
            return this.View("Vehicles", viewModel);
        }

        [HttpPost]
        public IActionResult Search(string make, string model)
        {

            var viewModel = new AllVehiclesViewModel
            {
                Vehicles = this.vehicleService.GetAll<VehiclesViewModel>(),
            };
            return this.RedirectToAction("GetAll", viewModel);

        }

        [Route("ById/{id:int}")]
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
