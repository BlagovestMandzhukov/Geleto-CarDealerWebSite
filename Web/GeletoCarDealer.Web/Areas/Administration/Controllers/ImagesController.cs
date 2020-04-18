namespace GeletoCarDealer.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GeletoCarDealer.Services.Data;
    using GeletoCarDealer.Web.ViewModels.Administration.Images;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class ImagesController : AdministrationController
    {
        private readonly IImageService imageService;
        private readonly IVehicleService vehicleService;

        public ImagesController(IImageService imageService, IVehicleService vehicleService)
        {
            this.imageService = imageService;
            this.vehicleService = vehicleService;
        }

        public IActionResult EditImages(int id)
        {
            var vehicle = this.vehicleService.GetVehicle(id);
            var viewModel = new AllImagesViewModel
            {
                VehicleId = vehicle.Id,
                Images = this.imageService.GetAllImages<ImagesViewModel>(id),
            };

            return this.View("EditImages", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddImages(int id, IList<IFormFile> images)
        {
            var vehicleId = this.vehicleService.GetVehicle(id);
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("EditImages", new { id = vehicleId.Id });
            }

            await this.vehicleService.AddVehicleImagesAsync(id, images);

            return this.RedirectToAction("VehicleById", "Vehicles", new { id = vehicleId.Id });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var vehicleId = await this.imageService.RemoveImageAsync(id);

            return this.RedirectToAction("EditImages", new { id = vehicleId });
        }
    }
}
