namespace GeletoCarDealer.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GeletoCarDealer.Data.Models.Enums;
    using GeletoCarDealer.Services.Data;
    using GeletoCarDealer.Web.ViewModels.Administration.Specifications;
    using GeletoCarDealer.Web.ViewModels.Administration.Vehicles;
    using Microsoft.AspNetCore.Mvc;

    public class VehiclesController : AdministrationController
    {
        private readonly IVehicleService vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        public IActionResult Create()
        {
            var model = new CreateInputModel();
            model.Specifications = new List<SpecificationsInputModel>();

            foreach (Specifications spec in Enum.GetValues(typeof(Specifications)))
            {
                model.Specifications.Add(new SpecificationsInputModel { Specification = spec, IsSelected = false });
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateInputModel inputModel)
        {
            IList<string> specs = new List<string>();

            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            foreach (var spec in inputModel.Specifications.Where(x => x.IsSelected == true))
            {
                specs.Add(spec.Specification.ToString());
            }

            var category = Enum.GetName(typeof(CategoryType), int.Parse(inputModel.Category));
            var fuelType = Enum.GetName(typeof(FuelType), int.Parse(inputModel.FuelType));
            var transmissionType = Enum.GetName(typeof(TransmissionType), int.Parse(inputModel.TransmissionType));

            var vehicle = await this.vehicleService.CreateVehicleAsync(
                inputModel.Make,
                inputModel.Model,
                inputModel.Year,
                inputModel.Milage,
                category,
                fuelType,
                inputModel.Price,
                inputModel.HorsePower,
                transmissionType,
                specs,
                inputModel.Images,
                inputModel.Description);

            return this.RedirectToAction("AllVehicles");
        }

        [Route("All")]
        public async Task<IActionResult> AllVehicles(string orderBy, int category, string make, string model)
        {
            var viewModel = new SearchBarAllVehiclesViewModel();

            if (category == 0)
            {
                viewModel.Makes = this.vehicleService.GetVehicleMakes<SearchBarVehiclesMakesViewModel>();
                viewModel.Models = this.vehicleService.GetVehicleModels<SearchBarVehiclesModelsViewModel>();
            }
            else
            {
                viewModel.Makes = this.vehicleService.GetVehicleMakes<SearchBarVehiclesMakesViewModel>().Where(x => x.CategoryId == category);
                viewModel.Models = this.vehicleService.GetVehicleModels<SearchBarVehiclesModelsViewModel>()
                                                       .Where(x => x.CategoryId
                                                       == category);
            }

            if (string.IsNullOrEmpty(orderBy) && category == 0)
            {
                viewModel.Vehicles = await this.vehicleService.GetAll<VehiclesViewModel>();
                if (!string.IsNullOrEmpty(model))
                {
                    viewModel.Vehicles = this.vehicleService.GetAllByModel<VehiclesViewModel>(model);
                }
            }

            if (!string.IsNullOrEmpty(make))
            {
                viewModel.Models = this.vehicleService.GetVehicleModels<SearchBarVehiclesModelsViewModel>()
                                                            .Where(x => x.Make == make);
                viewModel.Vehicles = this.vehicleService.GetAllByMake<VehiclesViewModel>(make);
                if (category > 0)
                {
                    viewModel.Vehicles = this.vehicleService.GetAllByMake<VehiclesViewModel>(make)
                        .Where(x => x.CategoryId == category);
                    viewModel.Models = this.vehicleService.GetVehicleModels<SearchBarVehiclesModelsViewModel>()
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
                        return this.View("AllVehicles", viewModel);
                    }
                }

                if (!string.IsNullOrEmpty(model))
                {
                    viewModel.Vehicles = this.vehicleService.GetAllByMake<VehiclesViewModel>(make)
                        .Where(x => x.Model == model);
                }
                return this.View("AllVehicles", viewModel);
            }

            if (!string.IsNullOrEmpty(orderBy) || category > 0)
            {
                viewModel.Vehicles = this.vehicleService.GetAllByOrder<VehiclesViewModel>(orderBy).Where(x => x.CategoryId == category);
            }

            if (category == 0 && !string.IsNullOrEmpty(orderBy))
            {
                viewModel.Vehicles = this.vehicleService.GetAllByOrder<VehiclesViewModel>(orderBy);
            }

            return this.View("AllVehicles", viewModel);
        }

        [HttpGet]
        [Route("VehicleById/{id:int}")]
        public IActionResult VehicleById(int id)
        {
            var vehicle = this.vehicleService.GetById<VehicleDetailsViewModel>(id);

            if (vehicle == null)
            {
                return this.NotFound();
            }

            return this.View(vehicle);
        }

        [Route("Edit/{id:int}")]
        public IActionResult EditVehicle(int id)
        {
            var model = this.vehicleService.GetById<EditVehicleViewModel>(id);

            return this.View(model);
        }

        [HttpPost]
        [Route("Edit/{id:int}")]
        public async Task<IActionResult> EditVehicle(EditVehicleViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var category = Enum.GetName(typeof(CategoryType), int.Parse(inputModel.Category));
            var fuelType = Enum.GetName(typeof(FuelType), int.Parse(inputModel.FuelType));
            var transmissionType = Enum.GetName(typeof(TransmissionType), int.Parse(inputModel.TransmissionType));
            var vehicle = this.vehicleService.GetVehicle(inputModel.Id);

            var currentVehicle = await this.vehicleService.EditVehicle(
                vehicle.Id,
                inputModel.Make,
                inputModel.Model,
                inputModel.Year,
                inputModel.Milage,
                category,
                fuelType,
                inputModel.Price,
                inputModel.HorsePower,
                transmissionType,
                inputModel.Description);

            return this.RedirectToAction("VehicleById", new { id = vehicle.Id });
        }

        [HttpGet]
        [Route("Delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            var vehicle = this.vehicleService.GetById<VehicleDetailsViewModel>(id);

            if (vehicle == null)
            {
                return this.NotFound();
            }

            return this.View(vehicle);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var deleted = await this.vehicleService.Delete(id);

            return this.RedirectToAction("AllVehicles");
        }

        [Route("Deleted")]
        public IActionResult AllDeleted()
        {
            var viewModel = new AllVehiclesViewModel
            {
                Vehicles = this.vehicleService.GetAllDeleted<VehiclesViewModel>(),
            };

            return this.View("AllDeletedVehicles", viewModel);
        }
    }
}
