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

        public VehicleService(IDeletableEntityRepository<Vehicle> vehicleRepository, IImageService imageService)
        {
            this.vehicleRepository = vehicleRepository;
            this.imageService = imageService;
        }

        public async Task<int> CreateVehicleAsync(string make, string model, int year, int milage, int category, string fuelType, decimal price, int horsePower, string transmission, IList<string> specifications, IList<IFormFile> images)
        {
            var vehicle = new Vehicle
            {
                Make = make,
                Model = model,
                Year = year,
                Milage = milage,
                CategoryId = category,
                FuelType = fuelType,
                Price = price,
                HorsePower = horsePower,
                TransmissionType = transmission,
            };

            foreach (var spec in specifications)
            {
                var vs = new VehicleSpecification
                {
                    Name = spec,
                };

                vehicle.Specifications.Add(vs);
            };

            await this.imageService.UploadImageAsync(vehicle.Id, images);
            await this.vehicleRepository.SaveChangesAsync();
            return vehicle.Id;
        }
    }
}
