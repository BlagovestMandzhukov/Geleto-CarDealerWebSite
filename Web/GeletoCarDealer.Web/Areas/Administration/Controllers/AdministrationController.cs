namespace GeletoCarDealer.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using GeletoCarDealer.Common;
    using GeletoCarDealer.Data.Common.Repositories;
    using GeletoCarDealer.Data.Models;
    using GeletoCarDealer.Data.Models.Enums;
    using GeletoCarDealer.Data.Models.Models;
    using GeletoCarDealer.Services.Data;
    using GeletoCarDealer.Services.Mapping;
    using GeletoCarDealer.Web.Controllers;
    using GeletoCarDealer.Web.ViewModels.Administration.Images;
    using GeletoCarDealer.Web.ViewModels.Administration.Specifications;
    using GeletoCarDealer.Web.ViewModels.Administration.Vehicles;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    //[Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
        private readonly IImageService imageService;
        private readonly IVehicleService vehicleService;

        public AdministrationController(
            IImageService imageService,
            IVehicleService vehicleService)
        {
            this.imageService = imageService;
            this.vehicleService = vehicleService;
        }

        [Route("/[controller]/Admin")]
        public IActionResult Index()
        {
            return this.View();
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
        public IActionResult AllVehicles()
        {
            var viewModel = new AllVehiclesViewModel
            {
                Vehicles = this.vehicleService.GetAll<VehiclesViewModel>(),
            };

            return this.View(viewModel);
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
            var vehicleId = await this.vehicleService.GetVehicleId(inputModel.Id);

            var vehicle = await this.vehicleService.EditVehicle(
                vehicleId,
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

            return this.RedirectToAction("VehicleById", new { id = vehicleId });
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

        public async Task<IActionResult> EditImages(int id)
        {
            var vehicleId = await this.vehicleService.GetVehicleId(id);
            var viewModel = new AllImagesViewModel
            {
                VehicleId = vehicleId,
                Images = this.imageService.GetAllImages<ImagesViewModel>(id),
            };

            return this.View("EditImages", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddImages(int id, IList<IFormFile> images)
        {
            var vehicleId = await this.vehicleService.GetVehicleId(id);
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("EditImages", new { id = vehicleId });
            }

            await this.vehicleService.AddVehicleImagesAsync(id, images);

            return this.RedirectToAction("VehicleById", new { id = vehicleId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var vehicleId = await this.imageService.RemoveImageAsync(id);

            return this.RedirectToAction("EditImages", new { id = vehicleId });
        }
    }
}
