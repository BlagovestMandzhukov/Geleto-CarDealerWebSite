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
    using GeletoCarDealer.Web.ViewModels.UsersArea.Vehicles;
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

            foreach (var spec in specifications)
            {
                var newSpec = new Specification
                {
                    Name = spec,
                };
                vehicle.Specifications.Add(newSpec);
            }

            var urls = await this.imageService.UploadImageAsync(vehicle, images);
            foreach (var url in urls)
            {
                var image = new Image
                {
                    ImageUrl = url,
                };
                vehicle.Images.Add(image);
            }

            await this.vehicleRepository.AddAsync(vehicle);
            await this.vehicleRepository.SaveChangesAsync();
            return vehicle.Id;
        }

        public async Task<bool> Delete(int id)
        {
            var vehicle = await this.vehicleRepository.All().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (vehicle == null)
            {
                return false;
            }

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

            var urls = await this.imageService.UploadImageAsync(vehicle, images);
            foreach (var url in urls)
            {
                var image = new Image
                {
                    ImageUrl = url,
                };
                vehicle.Images.Add(image);
            }

            await this.vehicleRepository.SaveChangesAsync();
            return vehicle.Id;
        }

        public async Task<IEnumerable<T>> GetAll<T>()
        {
            IQueryable<Vehicle> query = this.vehicleRepository.All().OrderBy(x => x.CreatedOn);
            return await query.To<T>().ToListAsync();
        }

        public IEnumerable<T> GetAllByOrder<T>(string orderBy = null)
        {
            switch (orderBy)
            {
                case GlobalConstants.OrderByPriceAscending: return this.GetAllByPriceAscending<T>();
                case GlobalConstants.OrderByPriceDescending: return this.GetAllByPriceDescending<T>();
                case GlobalConstants.OrderByMilage: return this.GetAllByMilage<T>();
            }

            IQueryable<Vehicle> query = this.vehicleRepository.All().OrderBy(x => x.CreatedOn);
            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllFromCategory<T>(int category)
        {
            IQueryable<Vehicle> query = this.vehicleRepository.All().Where(x => x.CategoryId == category);

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
            IQueryable<Vehicle> vehicleMakes = this.vehicleRepository.All().OrderBy(x => x.Make);

            return vehicleMakes.To<T>().ToList();
        }

        public IEnumerable<T> GetVehicleModels<T>()
        {
            IQueryable<Vehicle> vehicleModels = this.vehicleRepository.All().OrderBy(x => x.Model);

            return vehicleModels.Distinct().To<T>().ToList();
        }

        public IEnumerable<T> GetAllByMake<T>(string make)
        {
            IQueryable<Vehicle> vehiclesByMake = this.vehicleRepository.All().Where(x => x.Make == make);

            return vehiclesByMake.To<T>().ToList();
        }

        public IEnumerable<T> GetAllByModel<T>(string model)
        {
            IQueryable<Vehicle> vehiclesByModel = this.vehicleRepository.All().Where(x => x.Model == model);

            return vehiclesByModel.To<T>().ToList();
        }

        public async Task<AllVehiclesViewModel> GetAllFiltered(string orderBy, int category, string make, string model)
        {
            var viewModel = new AllVehiclesViewModel();
            int vehicleCategory = 0;

            if (category == 0)
            {
                viewModel.Makes = this.GetVehicleMakes<VehicleMakeViewModel>();
                viewModel.Models = this.GetVehicleModels<VehicleModelsViewModel>();
            }
            else
            {
                vehicleCategory = this.GetCurrentCategoryId(category);
                viewModel.Makes = this.GetVehicleMakes<VehicleMakeViewModel>().Where(x => x.CategoryId == vehicleCategory);
                viewModel.Models = this.GetVehicleModels<VehicleModelsViewModel>().Where(x => x.CategoryId == vehicleCategory);
            }

            if (string.IsNullOrEmpty(orderBy) && category == 0)
            {
                viewModel.Vehicles = await this.GetAll<VehiclesViewModel>();
                if (!string.IsNullOrEmpty(model))
                {
                    viewModel.Vehicles = this.GetAllByModel<VehiclesViewModel>(model);
                    viewModel.Makes = this.GetVehicleMakes<VehicleMakeViewModel>();
                    if (!string.IsNullOrEmpty(make))
                    {
                        viewModel.Models = this.GetVehicleModels<VehicleModelsViewModel>()
                                                       .Where(x => x.Make
                                                       == make);
                        viewModel.Vehicles = this.GetAllByMake<VehiclesViewModel>(make).Where(x => x.Model == model);
                        if (viewModel.Vehicles.Count() == 0)
                        {
                            viewModel.Vehicles = this.GetAllByMake<VehiclesViewModel>(make);
                        }
                    }

                    return viewModel;
                }
            }
            else if (!string.IsNullOrEmpty(orderBy) && !string.IsNullOrEmpty(model) && category == 0 && !string.IsNullOrEmpty(make))
            {
                viewModel.Vehicles = this.GetAllByOrder<VehiclesViewModel>(orderBy).Where(x => x.Model == model && x.Make == make);
                if (viewModel.Vehicles.Count() == 0)
                {
                    viewModel.Vehicles = this.GetAllByOrder<VehiclesViewModel>(orderBy).Where(x => x.Make == make);
                }

                viewModel.Makes = this.GetVehicleMakes<VehicleMakeViewModel>();
                viewModel.Models = this.GetVehicleModels<VehicleModelsViewModel>().Where(x => x.Make == make);
                return viewModel;
            }
            else if (!string.IsNullOrEmpty(orderBy) && string.IsNullOrEmpty(model) && category == 0 && string.IsNullOrEmpty(make))
            {
                viewModel.Vehicles = this.GetAllByOrder<VehiclesViewModel>(orderBy);
                return viewModel;
            }
            else if (string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(model) && !string.IsNullOrEmpty(orderBy) && category == 0)
            {
                viewModel.Vehicles = this.GetAllByOrder<VehiclesViewModel>(orderBy).Where(x => x.Model == model);
                viewModel.Makes = this.GetVehicleMakes<VehicleMakeViewModel>();
                viewModel.Models = this.GetVehicleModels<VehicleModelsViewModel>();
                return viewModel;
            }
            else if (string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(model) && string.IsNullOrEmpty(orderBy) && category > 0)
            {
                vehicleCategory = this.GetCurrentCategoryId(category);
                viewModel.Vehicles = this.GetAllFromCategory<VehiclesViewModel>(vehicleCategory).Where(x => x.Model == model);
                if (viewModel.Vehicles.Count() == 0)
                {
                    viewModel.Vehicles = this.GetAllFromCategory<VehiclesViewModel>(vehicleCategory);
                }

                viewModel.Makes = this.GetVehicleMakes<VehicleMakeViewModel>().Where(x => x.CategoryId == vehicleCategory);
                viewModel.Models = this.GetVehicleModels<VehicleModelsViewModel>().Where(x => x.CategoryId == vehicleCategory);
                return viewModel;
            }
            else if (string.IsNullOrEmpty(make) && string.IsNullOrEmpty(model) && string.IsNullOrEmpty(orderBy) && category > 0)
            {
                vehicleCategory = this.GetCurrentCategoryId(category);
                viewModel.Vehicles = this.GetAllFromCategory<VehiclesViewModel>(vehicleCategory);
                viewModel.Makes = this.GetVehicleMakes<VehicleMakeViewModel>().Where(x => x.CategoryId == vehicleCategory);
                viewModel.Models = this.GetVehicleModels<VehicleModelsViewModel>().Where(x => x.CategoryId == vehicleCategory);
                return viewModel;
            }
            else if (string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(model) && !string.IsNullOrEmpty(orderBy) && category > 0)
            {
                vehicleCategory = this.GetCurrentCategoryId(category);
                viewModel.Vehicles = this.GetAllByOrder<VehiclesViewModel>(orderBy).Where(x => x.Model == model && x.CategoryId == vehicleCategory);
                if (viewModel.Vehicles.Count() == 0)
                {
                    viewModel.Vehicles = this.GetAllByOrder<VehiclesViewModel>(orderBy).Where(x => x.CategoryId == vehicleCategory);
                }

                viewModel.Makes = this.GetVehicleMakes<VehicleMakeViewModel>().Where(x => x.CategoryId == vehicleCategory);
                viewModel.Models = this.GetVehicleModels<VehicleModelsViewModel>().Where(x => x.CategoryId == vehicleCategory);
                return viewModel;
            }
            else if (!string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(model) && !string.IsNullOrEmpty(orderBy) && category > 0)
            {
                vehicleCategory = this.GetCurrentCategoryId(category);
                viewModel.Vehicles = this.GetAllByOrder<VehiclesViewModel>(orderBy).Where(x => x.Make == make && x.CategoryId == vehicleCategory && x.Model == model);
                if (viewModel.Vehicles.Count() == 0)
                {
                    viewModel.Vehicles = this.GetAllByOrder<VehiclesViewModel>(orderBy).Where(x => x.CategoryId == vehicleCategory && x.Make == make);
                    if (viewModel.Vehicles.Count() == 0)
                    {
                        viewModel.Vehicles = this.GetAllFromCategory<VehiclesViewModel>(vehicleCategory);
                    }
                }

                viewModel.Makes = this.GetVehicleMakes<VehicleMakeViewModel>().Where(x => x.CategoryId == vehicleCategory);
                viewModel.Models = this.GetVehicleModels<VehicleModelsViewModel>().Where(x => x.Make == make && x.CategoryId == vehicleCategory);

                return viewModel;
            }
            else if (!string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(model) && string.IsNullOrEmpty(orderBy) && category > 0)
            {
                vehicleCategory = this.GetCurrentCategoryId(category);
                viewModel.Vehicles = this.GetAllByMake<VehiclesViewModel>(make).Where(x => x.CategoryId == vehicleCategory && x.Model == model);
                if (viewModel.Vehicles.Count() == 0)
                {
                    viewModel.Vehicles = this.GetAllFromCategory<VehiclesViewModel>(vehicleCategory).Where(x => x.Make == make);
                    if (viewModel.Vehicles.Count() == 0)
                    {
                        viewModel.Vehicles = this.GetAllFromCategory<VehiclesViewModel>(vehicleCategory);
                    }
                }

                viewModel.Models = this.GetVehicleModels<VehicleModelsViewModel>().Where(x => x.CategoryId == category && x.Make == make);
                viewModel.Makes = this.GetVehicleMakes<VehicleMakeViewModel>().Where(x => x.CategoryId == vehicleCategory);
                return viewModel;
            }
            else if (!string.IsNullOrEmpty(make) && string.IsNullOrEmpty(model) && !string.IsNullOrEmpty(orderBy) && category > 0)
            {
                vehicleCategory = this.GetCurrentCategoryId(category);
                viewModel.Vehicles = this.GetAllByOrder<VehiclesViewModel>(orderBy).Where(x => x.CategoryId == vehicleCategory && x.Make == make);
                if (viewModel.Vehicles.Count() == 0)
                {
                    viewModel.Vehicles = this.GetAllByOrder<VehiclesViewModel>(orderBy).Where(x => x.CategoryId == vehicleCategory);
                }

                viewModel.Models = this.GetVehicleModels<VehicleModelsViewModel>().Where(x => x.CategoryId == vehicleCategory && x.Make == make);
                viewModel.Makes = this.GetVehicleMakes<VehicleMakeViewModel>().Where(x => x.CategoryId == vehicleCategory);
                return viewModel;
            }

            if (!string.IsNullOrEmpty(make))
            {
                viewModel.Models = this.GetVehicleModels<VehicleModelsViewModel>()
                                                            .Where(x => x.Make == make);
                viewModel.Vehicles = this.GetAllByMake<VehiclesViewModel>(make);
                if (category > 0)
                {
                    vehicleCategory = this.GetCurrentCategoryId(category);
                    viewModel.Vehicles = this.GetAllByMake<VehiclesViewModel>(make)
                        .Where(x => x.CategoryId == vehicleCategory);
                    if (viewModel.Vehicles.Count() == 0)
                    {
                        viewModel.Vehicles = this.GetAllFromCategory<VehiclesViewModel>(vehicleCategory);
                    }

                    viewModel.Models = this.GetVehicleModels<VehicleModelsViewModel>()
                                                            .Where(x => x.Make == make && x.CategoryId == vehicleCategory);
                    return viewModel;
                }

                if (!string.IsNullOrEmpty(orderBy))
                {
                    viewModel.Vehicles = this.GetAllByOrder<VehiclesViewModel>(orderBy)
                        .Where(x => x.Make == make);
                    if (category > 0)
                    {
                        vehicleCategory = this.GetCurrentCategoryId(category);
                        viewModel.Vehicles = this.GetAllByMake<VehiclesViewModel>(make)
                            .Where(x => x.CategoryId == vehicleCategory);
                        return viewModel;
                    }
                }

                if (!string.IsNullOrEmpty(model))
                {
                    viewModel.Vehicles = this.GetAllByMake<VehiclesViewModel>(make)
                        .Where(x => x.Model == model);
                }

                return viewModel;
            }

            if (!string.IsNullOrEmpty(orderBy))
            {
                viewModel.Vehicles = this.GetAllByOrder<VehiclesViewModel>(orderBy);
                if (category > 0)
                {
                    vehicleCategory = this.GetCurrentCategoryId(category);
                    viewModel.Vehicles = this.GetAllByOrder<VehiclesViewModel>(orderBy).Where(x => x.CategoryId == vehicleCategory);
                    if (!string.IsNullOrEmpty(model))
                    {
                        viewModel.Models = this.GetVehicleModels<VehicleModelsViewModel>().Where(x => x.CategoryId == vehicleCategory);
                    }

                    if (!string.IsNullOrEmpty(make))
                    {
                        viewModel.Makes = this.GetVehicleMakes<VehicleMakeViewModel>().Where(x => x.CategoryId == vehicleCategory);
                    }
                }

                return viewModel;
            }

            if (viewModel.Vehicles == null)
            {
                viewModel.Vehicles = await this.GetAll<VehiclesViewModel>();
            }

            return viewModel;
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

        private IEnumerable<T> GetAllByMilage<T>()
        {
            IQueryable<Vehicle> query = this.vehicleRepository.All().OrderBy(x => x.Milage);
            return query.To<T>().ToList();
        }

        private int GetCurrentCategoryId(int id)
        {
            switch (id)
            {
                case 1: return this.categoryService.GetCarsId();
                case 2: return this.categoryService.GetSuvId();
                case 3: return this.categoryService.GetMotorcycleId();
                case 4: return this.categoryService.GetBusId();
            }

            return 0;
        }
    }
}
