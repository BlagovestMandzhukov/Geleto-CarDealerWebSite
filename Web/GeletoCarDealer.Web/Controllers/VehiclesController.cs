namespace GeletoCarDealer.Web.Controllers
{
    using GeletoCarDealer.Data.Common.Repositories;
    using GeletoCarDealer.Data.Models;
    using GeletoCarDealer.Services.Data;
    using GeletoCarDealer.Web.ViewModels.UsersArea.Vehicles;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class VehiclesController : BaseController
    {
        private readonly IVehicleService vehicleService;
        private readonly IDeletableEntityRepository<Vehicle> vehicleRepository;

        public VehiclesController(IVehicleService vehicleService, IDeletableEntityRepository<Vehicle> vehicleRepository)
        {
            this.vehicleService = vehicleService;
            this.vehicleRepository = vehicleRepository;
        }

        [HttpGet]
        public IActionResult GetAll(AllVehiclesViewModel model)
        {
            var viewModel = new AllVehiclesViewModel();

            viewModel.Vehicles = this.vehicleService.GetAll<VehiclesViewModel>();

            if (!string.IsNullOrEmpty(model.OrderBy) || model.Category > 0)
            {
                viewModel.Vehicles = this.vehicleService.GetAllByOrder<VehiclesViewModel>(model.OrderBy).Where(x => x.CategoryId == model.Category);
            }
            if (model.Category == 0 && !string.IsNullOrEmpty(model.OrderBy))
            {
                viewModel.Vehicles = this.vehicleService.GetAllByOrder<VehiclesViewModel>(model.OrderBy);
            }

            return this.View("Vehicles", viewModel);
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
