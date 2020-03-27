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
    using GeletoCarDealer.Services.Mapping;
    using GeletoCarDealer.Web.ViewModels.Administration.Vehicles;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public class VehicleService : IVehicleService
    {
        private readonly IDeletableEntityRepository<Vehicle> vehicleRepository;
        private readonly IImageService imageService;
        private readonly ICategoryService categoryService;
        private readonly IRepository<Specification> specRepository;

        public VehicleService(
            IDeletableEntityRepository<Vehicle> vehicleRepository,
            IImageService imageService,
            ICategoryService categoryService,
            IRepository<Specification> specRepository)
        {
            this.vehicleRepository = vehicleRepository;
            this.imageService = imageService;
            this.categoryService = categoryService;
            this.specRepository = specRepository;
        }

        public async Task<int> CreateVehicleAsync(string make, string model, int year, int milage, string category, string fuelType, decimal price, int horsePower, string transmission, IList<string> specifications, IList<IFormFile> images, string description)
        {
            var categoryId = await this.categoryService.CreateCategory(category);

            var vehicle = new Vehicle
            {
                Make = make,
                Model = model,
                Year = year,
                Milage = milage,
                FuelType = fuelType,
                Price = price,
                HorsePower = horsePower,
                CategoryId = categoryId,
                TransmissionType = transmission,
                Description = description,
            };

            await this.vehicleRepository.AddAsync(vehicle);
            await this.vehicleRepository.SaveChangesAsync();

            foreach (var spec in specifications)
            {
                var newSpec = new Specification
                {
                    Name = spec,
                };
                vehicle.Specifications.Add(newSpec);
            };

            await this.imageService.UploadImageAsync(vehicle, images);
            await this.vehicleRepository.SaveChangesAsync();
            return vehicle.Id;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<Vehicle> query = this.vehicleRepository.All().OrderBy(x => x.CreatedOn);
            return query.To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            var vehicle = this.vehicleRepository.All()
                .Include(x => x.Images)
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return vehicle;
        }
    }
}
