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
    using GeletoCarDealer.Web.ViewModels.UsersArea.Vehicles;
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
            var viewModel = new AllVehiclesViewModel();

            viewModel = await this.vehicleService.GetAllFiltered(orderBy, category, make, model);

            return this.View("AllVehicles", viewModel);
        }

        [HttpGet]
        [Route("VehicleById/{id:int}")]
        public IActionResult VehicleById(int id)
        {
            var vehicle = this.vehicleService.GetById<AdminVehicleDetailsViewModel>(id);

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
            var vehicle = this.vehicleService.GetById<AdminVehicleDetailsViewModel>(id);

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
            var viewModel = new AdminAllVehiclesViewModel
            {
                Vehicles = this.vehicleService.GetAllDeleted<AdminVehiclesViewModel>(),
            };

            return this.View("AllDeletedVehicles", viewModel);
        }
    }
}
