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

            if (category == 0)
            {
                viewModel.Makes = this.vehicleService.GetVehicleMakes<VehicleMakeViewModel>();
                viewModel.Models = this.vehicleService.GetVehicleModels<VehicleModelsViewModel>();
            }
            else
            {
                viewModel.Makes = this.vehicleService.GetVehicleMakes<VehicleMakeViewModel>().Where(x => x.CategoryId == category);
                viewModel.Models = this.vehicleService.GetVehicleModels<VehicleModelsViewModel>()
                                                       .Where(x => x.CategoryId
                                                       == category);
            }

            if (string.IsNullOrEmpty(orderBy) && category == 0)
            {
                viewModel.Vehicles = await this.vehicleService.GetAll<VehiclesViewModel>();
            }

            if (!string.IsNullOrEmpty(make))
            {
                viewModel.Models = this.vehicleService.GetVehicleModels<VehicleModelsViewModel>()
                                                            .Where(x => x.Make == make);
                viewModel.Vehicles = this.vehicleService.GetAllByMake<VehiclesViewModel>(make);
                if (category > 0)
                {
                    viewModel.Vehicles = this.vehicleService.GetAllByMake<VehiclesViewModel>(make)
                        .Where(x => x.CategoryId == category);
                    viewModel.Models = this.vehicleService.GetVehicleModels<VehicleModelsViewModel>()
                                                            .Where(x => x.Make == make && x.CategoryId == category);
                }
                if (!string.IsNullOrEmpty(orderBy))
                {
                    viewModel.Vehicles = this.vehicleService.GetAllByOrder<VehiclesViewModel>(orderBy)
                        .Where(x => x.Make == make);
                    if (category > 0)
                    {
                        viewModel.Vehicles = this.vehicleService.GetAllByMake<VehiclesViewModel>(make)
                            .Where(x => x.CategoryId == category);
                    }
                }
                if (!string.IsNullOrEmpty(model))
                {
                    viewModel.Vehicles = this.vehicleService.GetAllByMake<VehiclesViewModel>(make)
                        .Where(x => x.Model == model);

                }
                return this.View("Vehicles", viewModel);
            }

            if (!string.IsNullOrEmpty(orderBy) || category > 0)
            {
                viewModel.Vehicles = this.vehicleService.GetAllByOrder<VehiclesViewModel>(orderBy).Where(x => x.CategoryId == category);
            }

            if (category == 0 && !string.IsNullOrEmpty(orderBy))
            {
                viewModel.Vehicles = this.vehicleService.GetAllByOrder<VehiclesViewModel>(orderBy);
            }

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
