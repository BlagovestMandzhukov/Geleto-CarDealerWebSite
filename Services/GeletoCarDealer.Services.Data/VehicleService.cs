﻿namespace GeletoCarDealer.Services.Data
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

        public IEnumerable<T> GetAll<T>()
        {
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

        public ICollection<T> SearchByMake<T>(string make)
        {
            IQueryable<Vehicle> vehiclesByMake = this.vehicleRepository.All()
                .Where(x => x.Make == make);

            return vehiclesByMake.To<T>().ToList();
        }

        public ICollection<T> SearchByCategory<T>(int category)
        {
            IQueryable<Vehicle> vehiclesByMake = this.vehicleRepository.All()
                .Where(x => x.CategoryId == category);

            return vehiclesByMake.To<T>().ToList();
        }

        public ICollection<T> SearchByModel<T>(string model)
        {
            IQueryable<Vehicle> vehiclesByMake = this.vehicleRepository.All()
                .Where(x => x.Model == model);

            return vehiclesByMake.To<T>().ToList();
        }

    }
}
