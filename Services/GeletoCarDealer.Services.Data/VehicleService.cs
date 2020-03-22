namespace GeletoCarDealer.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using GeletoCarDealer.Data.Common.Repositories;
    using GeletoCarDealer.Data.Models;
    using GeletoCarDealer.Data.Models.Enums;
    using GeletoCarDealer.Data.Models.Models;
    using GeletoCarDealer.Web.ViewModels.Administration.Vehicles;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public class VehicleService : IVehicleService
    {
        private readonly IDeletableEntityRepository<Vehicle> vehicleRepository;
        private readonly IImageService imageService;
        private readonly ICategoryService categoryService;

        public VehicleService(
            IDeletableEntityRepository<Vehicle> vehicleRepository, 
            IImageService imageService,
            ICategoryService categoryService)
        {
            this.vehicleRepository = vehicleRepository;
            this.imageService = imageService;
            this.categoryService = categoryService;
        }

        public async Task<int> CreateVehicleAsync(string make, string model, int year, int milage, string category, string fuelType, decimal price, int horsePower, string transmission, IList<string> specifications, IList<IFormFile> images)
        {
            var vehicle = new Vehicle
            {
                Make = make,
                Model = model,
                Year = year,
                Milage = milage,
                FuelType = fuelType,
                Price = price,
                HorsePower = horsePower,
                TransmissionType = transmission,
            };

            var categoryId = await this.categoryService.CreateCategory(category);
            //await this.categoryService.AddVehicleToCategory(vehicle.Id, categoryId);
            vehicle.CategoryId = categoryId;

            await this.vehicleRepository.AddAsync(vehicle);
            await this.vehicleRepository.SaveChangesAsync();

            foreach (var spec in specifications)
            {
                var vs = new VehicleSpecification
                {
                    Name = spec,
                    VehicleId = vehicle.Id,
                };

                vehicle.Specifications.Add(vs);

            };
            await this.vehicleRepository.SaveChangesAsync();
            await this.imageService.UploadImageAsync(vehicle.Id, images);
            return vehicle.Id;
        }
    }
}
