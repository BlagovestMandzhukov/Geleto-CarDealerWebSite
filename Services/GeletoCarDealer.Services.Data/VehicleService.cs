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
        private readonly IRepository<VehicleSpecification> vehicleSpecRepository;

        public VehicleService(
            IDeletableEntityRepository<Vehicle> vehicleRepository,
            IImageService imageService,
            ICategoryService categoryService,
            IRepository<Specification> specRepository,
            IRepository<VehicleSpecification> vehicleSpecRepository)
        {
            this.vehicleRepository = vehicleRepository;
            this.imageService = imageService;
            this.categoryService = categoryService;
            this.specRepository = specRepository;
            this.vehicleSpecRepository = vehicleSpecRepository;
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
                var specification = await this.specRepository.All().FirstOrDefaultAsync(x => x.Name == spec);
                if (specification == null)
                {
                    var newSpec = new Specification
                    {
                        Name = spec,
                    };

                    await this.specRepository.AddAsync(newSpec);
                    await this.specRepository.SaveChangesAsync();
                    specification = newSpec;
                }

                var vehicleSpec = new VehicleSpecification
                {
                    SpecificationId = specification.Id,
                    VehicleId = vehicle.Id,
                };
                await this.vehicleSpecRepository.AddAsync(vehicleSpec);
            };

            await this.imageService.UploadImageAsync(vehicle, images);
            await this.vehicleSpecRepository.SaveChangesAsync();
            await this.vehicleRepository.SaveChangesAsync();
            return vehicle.Id;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<Vehicle> query = this.vehicleRepository.All().OrderBy(x => x.CreatedOn);

            return query.To<T>().ToList();
        }
    }
}
