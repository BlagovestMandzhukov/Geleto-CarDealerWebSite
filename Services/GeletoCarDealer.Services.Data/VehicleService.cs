namespace GeletoCarDealer.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using GeletoCarDealer.Common;
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
        private readonly IMessageService messageService;

        public VehicleService(
            IDeletableEntityRepository<Vehicle> vehicleRepository,
            IImageService imageService,
            ICategoryService categoryService,
            IRepository<Specification> specRepository,
            IMessageService messageService)
        {
            this.vehicleRepository = vehicleRepository;
            this.imageService = imageService;
            this.categoryService = categoryService;
            this.specRepository = specRepository;
            this.messageService = messageService;
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
            }

            await this.imageService.UploadImageAsync(vehicle, images);
            await this.vehicleRepository.SaveChangesAsync();
            return vehicle.Id;
        }

        public async Task<bool> Delete(int id)
        {
            var vehicle = await this.vehicleRepository.All().Where(x => x.Id == id).FirstOrDefaultAsync();
            this.vehicleRepository.Delete(vehicle);
            await this.vehicleRepository.SaveChangesAsync();
            return true;
        }

        public async Task<int> EditVehicle(int id, string make, string model, int year, int milage, string category, string fuelType, decimal price, int horsePower, string transmission, string description)
        {
            var vehicle = await this.vehicleRepository.All().Where(x => x.Id == id).FirstOrDefaultAsync();
            var categoryId = await this.categoryService.CreateCategory(category);

            vehicle.Make = make;
            vehicle.Model = model;
            vehicle.Year = year;
            vehicle.Milage = milage;
            vehicle.CategoryId = categoryId;
            vehicle.FuelType = fuelType;
            vehicle.Price = price;
            vehicle.HorsePower = horsePower;
            vehicle.TransmissionType = transmission;
            vehicle.Description = description;

            this.vehicleRepository.Update(vehicle);
            await this.vehicleRepository.SaveChangesAsync();

            return vehicle.Id;
        }

        public async Task<int> AddVehicleImagesAsync(int id, IList<IFormFile> images)
        {
            var vehicle = await this.vehicleRepository.All().Where(x => x.Id == id).FirstOrDefaultAsync();

            await this.imageService.UploadImageAsync(vehicle, images);
            await this.vehicleRepository.SaveChangesAsync();
            return vehicle.Id;
        }

        public IEnumerable<T> GetAll<T>(string orderBy = null, int? category = null)
        {
            switch (orderBy)
            {
                case GlobalConstants.OrderByPriceAscending: return this.GetAllByPriceAscending<T>();
                case GlobalConstants.OrderByPriceDescending: return this.GetAllByPriceDescending<T>();
                case GlobalConstants.OrderByYearAscending: return this.GetAllByYear<T>();
            }

            switch (category)
            {
                case GlobalConstants.CarCategory: return this.GetAllInCarCategory<T>(category.Value);
                case GlobalConstants.SUVCategory: return this.GetAllInSuvCategory<T>(category.Value);
                case GlobalConstants.MotorcycleCategory: return this.GetAllInMotorcycleCategory<T>(category.Value);
                case GlobalConstants.BusCategory: return this.GetAllInBusCategory<T>(category.Value);
            }

            IQueryable<Vehicle> query = this.vehicleRepository.All().OrderBy(x => x.CreatedOn);
            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllDeleted<T>()
        {
            IQueryable<Vehicle> deletedVehiclesQuery = this.vehicleRepository.AllWithDeleted().Where(x => x.IsDeleted == true);

            return deletedVehiclesQuery.To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            var vehicle = this.vehicleRepository.All()
                .Include(x => x.Images)
                .Include(x => x.Messages)
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return vehicle;
        }

        public Vehicle GetVehicle(int id)
        {
            var vehicle = this.vehicleRepository.All().FirstOrDefault(x => x.Id == id);

            return vehicle;
        }

        public async Task<int> RemoveVehicleMessageAsync(int messageId)
        {
            var message = await this.messageService.GetMessageAsync(messageId);
            var vehicle = await this.vehicleRepository.All().FirstOrDefaultAsync(x => x.Id == message.VehicleId);
            vehicle.Messages.Remove(message);
            await this.vehicleRepository.SaveChangesAsync();

            return vehicle.Id;
        }

        public int AddMessageToVehicle(int id, string sentBy, string email, string phoneNumber, string messageContent)
        {
            var message = this.messageService.CreateMessage(id, sentBy, email, phoneNumber, messageContent);
            this.vehicleRepository.SaveChangesAsync();
            var vehiceId = this.vehicleRepository.All().Where(x => x.Id == id).Select(x => x.Id).FirstOrDefault();
            return vehiceId;
        }

        public IEnumerable<T> GetVehicleMakes<T>()
        {
            IQueryable<Vehicle> vehicleMakes = this.vehicleRepository.All();

            return vehicleMakes.To<T>().ToList();
        }

        public IEnumerable<T> GetVehicleModels<T>()
        {
            IQueryable<Vehicle> vehicleModels = this.vehicleRepository.All();

            return vehicleModels.To<T>().ToList();
        }

        public IEnumerable<T> GetVehicleCategories<T>()
        {
            IQueryable<Vehicle> vehicleCategories = this.vehicleRepository.All();

            return vehicleCategories.To<T>().ToList();
        }

        public IEnumerable<T> GetAllByMake<T>(string make)
        {
            IQueryable<Vehicle> vehiclesByMake = this.vehicleRepository.All().Where(x => x.Make == make);

            return vehiclesByMake.To<T>().ToList();
        }

        private IEnumerable<T> GetAllByPriceAscending<T>()
        {
            IQueryable<Vehicle> query = this.vehicleRepository.All().OrderBy(x => x.Price);
            return query.To<T>().ToList();
        }

        private IEnumerable<T> GetAllByPriceDescending<T>()
        {
            IQueryable<Vehicle> query = this.vehicleRepository.All().OrderByDescending(x => x.Price);
            return query.To<T>().ToList();
        }

        private IEnumerable<T> GetAllByYear<T>()
        {
            IQueryable<Vehicle> query = this.vehicleRepository.All().OrderByDescending(x => x.Year);
            return query.To<T>().ToList();
        }

        private IEnumerable<T> GetAllInCarCategory<T>(int id)
        {
            var categoryId = this.categoryService.GetAllCars(id);

            IQueryable<Vehicle> query = this.vehicleRepository.All().Where(x => x.CategoryId == categoryId);

            return query.To<T>().ToList();
        }

        private IEnumerable<T> GetAllInSuvCategory<T>(int id)
        {
            var categoryId = this.categoryService.GetAllSuvs(id);

            IQueryable<Vehicle> query = this.vehicleRepository.All().Where(x => x.CategoryId == categoryId);

            return query.To<T>().ToList();
        }

        private IEnumerable<T> GetAllInMotorcycleCategory<T>(int id)
        {
            var categoryId = this.categoryService.GetAllMotorcycles(id);

            IQueryable<Vehicle> query = this.vehicleRepository.All().Where(x => x.CategoryId == categoryId);

            return query.To<T>().ToList();
        }

        private IEnumerable<T> GetAllInBusCategory<T>(int id)
        {
            var categoryId = this.categoryService.GetAllBuses(id);

            IQueryable<Vehicle> query = this.vehicleRepository.All().Where(x => x.CategoryId == categoryId);

            return query.To<T>().ToList();
        }
    }
}
