using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using GeletoCarDealer.Data.Common.Repositories;
using GeletoCarDealer.Data.Models;
using GeletoCarDealer.Data.Models.Models;
using GeletoCarDealer.Data.Repositories;
using GeletoCarDealer.Services.Data;
using GeletoCarDealer.Services.Mapping;
using GeletoCarDealer.Web.ViewModels.UsersArea.Vehicles;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GeletoCarDealer.Data.Service.Tests
{
    public class VehicleServiceTests
    {
        private readonly EfDeletableEntityRepository<Message> messageRepository;
        private readonly EfDeletableEntityRepository<Vehicle> vehicleRepository;
        private readonly EfDeletableEntityRepository<Image> imageRepository;
        private readonly EfRepository<Specification> specificationsRepository;
        private readonly EfRepository<Category> categoryRepository;

        private readonly GeletoDbContext context;

        private readonly MessageService messageService;
        private readonly Mock<IImageService> imageService;
        private readonly CategoryService categoryService;
        private readonly VehicleService vehicleService;
        public VehicleServiceTests()
        {
            var options = new DbContextOptionsBuilder<GeletoDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;
            this.InitializeMapping();
            this.context = new GeletoDbContext(options);

            this.imageRepository = new EfDeletableEntityRepository<Image>(context);
            this.vehicleRepository = new EfDeletableEntityRepository<Vehicle>(context);
            this.specificationsRepository = new EfRepository<Specification>(context);
            this.categoryRepository = new EfRepository<Category>(context);

            this.messageRepository = new EfDeletableEntityRepository<Message>(this.context);
            this.messageService = new MessageService(messageRepository);
            this.imageService = new Mock<IImageService>();
            this.categoryService = new CategoryService(categoryRepository, vehicleRepository);
            this.vehicleService = new VehicleService(vehicleRepository, imageService.Object, categoryService, specificationsRepository, messageService);
        }

        [Fact]
        public async Task GetAllShouldReturnAllVehiclesInRepository()
        {
            var vehicle = new Vehicle
            {
                Make = "Audi",
                Model = "A4",
                HorsePower = 123,
                CategoryId = 1,
                Description = "asdasdasd",
                FuelType = "Diesel",
                Milage = 20000,
                Year = 2010,
                Price = 12313,
                TransmissionType = "Manual",
            };
            await this.vehicleRepository.AddAsync(vehicle);
            await this.vehicleRepository.SaveChangesAsync();
            var vehicles = await this.vehicleService.GetAll<VehiclesViewModel>();
            Assert.Single(vehicles);

        }

        [Fact]
        public async Task CreateVehicleAsyncShoutCreateVechicle()
        {
            var spec = new List<string>
            {
                "4x4",
                "ElectricSeats",
            };

            var vehicleId = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");

            Assert.Equal(1, vehicleId);
        }

        [Fact]
        public async Task DeleteShouldRemoveVehicleFromRepository()
        {
            var spec = new List<string>
            {
                "4x4",
                "ElectricSeats",
            };

            var vehicleId = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            await this.vehicleService.Delete(vehicleId);
            var vehicles = this.vehicleRepository.All();
            Assert.Equal(0, vehicles.Count());
        }

        [Fact]
        public async Task DeleteShouldReturnFalseIfVehicleIsNull()
        {
            var nullVehicleId = 0;

            var isDeleted = await this.vehicleService.Delete(nullVehicleId);
            Assert.False(isDeleted);
        }

        [Fact]
        public async Task EditVehicleShouldEditCurrentVehicle()
        {
            var spec = new List<string>
            {
                "4x4",
                "ElectricSeats",
            };

            var vehicleId = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleBeforeEdit = this.vehicleService.GetById<VehiclesViewModel>(vehicleId);

            var editedVehicleId = await this.vehicleService.EditVehicle(vehicleId, "Audi", "A5", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", "asdasdakjsdh");

            var vehicleAfterEdit = this.vehicleService.GetById<VehiclesViewModel>(editedVehicleId);

            Assert.NotEqual(vehicleBeforeEdit.Model, vehicleAfterEdit.Model);
        }

        [Fact]
        public async Task AddImagesToVehicleShouldAddImagesToCurrentVehicle()
        {
            var moqFormFile = new Mock<IFormFile>();

            var images = new List<IFormFile>
            {
                moqFormFile.Object,
            };

            var spec = new List<string>
            {
                "4x4",
                "ElectricSeats",
            };

            var vehicleId = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");

            var acutalVehicleId = await this.vehicleService.AddVehicleImagesAsync(vehicleId, images);

            var vehicle = this.vehicleService.GetById<VehicleDetailsViewModel>(acutalVehicleId);
            var actualImagesCount = vehicle.Images.Count();
            Assert.Equal(1, actualImagesCount);
        }

        [Fact]
        public async Task GetAllByOrderShouldReturnVehiclesSorted()
        {
            var spec = new List<string>
            {
                "4x4",
                "ElectricSeats",
            };
            var vehicleId1 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");

            var vehicleId2 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 16000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var notSortedVehicles = await this.vehicleService.GetAll<VehiclesViewModel>();
            var sortedVehicles = this.vehicleService.GetAllByOrder<VehiclesViewModel>("upPrice");

            Assert.NotEqual(notSortedVehicles.First(), sortedVehicles.First());
        }

        [Fact]
        public async Task GetAllFromCategoryShouldReturnVehiclesFromGivenCategory()
        {
            var spec = new List<string>
            {
                "4x4",
                "ElectricSeats",
            };
            var vehicleId1 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");

            var vehicleId2 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Bus", "Diesel", 16000, 150, "Automatic", spec, null, "asdasdakjsdh");
            this.InitializeMapping();
            var category = await this.categoryService.CreateCategory("Car");
            var vehicles = this.vehicleService.GetAllFromCategory<VehiclesViewModel>(category);

            Assert.Single(vehicles);
        }

        [Fact]
        public async Task GetAllDeletedShouldReturnVehiclesWhereIsDeletedIsTrue()
        {
            var spec = new List<string>
            {
                "4x4",
                "ElectricSeats",
            };
            var vehicleId1 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");

            var vehicleId2 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Bus", "Diesel", 16000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var isDeleted = this.vehicleService.Delete(vehicleId1);
            var deletedVehicles = this.vehicleService.GetAllDeleted<VehiclesViewModel>();
            Assert.Single(deletedVehicles);
        }

        [Fact]
        public async Task GetByIdShouldReturnIdOfVehicle()
        {
            var spec = new List<string>
            {
                "4x4",
                "ElectricSeats",
            };
            var vehicleId1 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");

            var vehicle = this.vehicleService.GetById<VehicleDetailsViewModel>(1);

            Assert.Equal(vehicleId1, vehicle.Id);
        }

        [Fact]
        public void GetVehicleShouldReturnVehicle()
        {
            var spec = new List<string>
            {
                "4x4",
                "ElectricSeats",
            };
            var vehicleId1 = this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicle = this.vehicleService.GetVehicle(1);

            Assert.Equal(vehicleId1.Result, vehicle.Id);
        }

        [Fact]
        public async Task RemoveVehicleMessageShouldDeleteMessage()
        {

            var spec = new List<string>
            {
                "4x4",
                "ElectricSeats",
            };
            var vehicleId1 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var message = this.messageService.CreateMessage(vehicleId1, "asd", "asd@asd.asd", "0854325512", "asdkhasdkjhagsjdkhgasda");
            var message1 = this.messageService.CreateMessage(vehicleId1, "asd", "asd@asd.asd", "0854325512", "asdkhasdkjhagsjdkhgasda");
            var vehicle = this.vehicleService.GetVehicle(vehicleId1);
            var id = await this.vehicleService.RemoveVehicleMessageAsync(message.Id);
            Assert.Single(vehicle.Messages);
        }
        [Fact]
        public async Task AddMessageToVehicleShouldAsignMessageToGivenVehicle()
        {
            var spec = new List<string>
            {
                "4x4",
                "ElectricSeats",
            };
            var vehicleId1 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var message = this.vehicleService.AddMessageToVehicle(vehicleId1, "asd", "asd@asd.asd", "0854325512", "asdkhasdkjhagsjdkhgasda");
            var vehicle = this.vehicleService.GetVehicle(vehicleId1);

            Assert.Single(vehicle.Messages);
        }

        [Fact]
        public async Task GetAllByModelShouldReturnAllVehiclesWithGivenModel()
        {
            var spec = new List<string>
            {
                "4x4",
                "ElectricSeats",
            };
            var vehicleId1 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId2 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicles = this.vehicleService.GetAllByModel<VehiclesViewModel>("A4");

            Assert.Equal(2, vehicles.Count());
        }

        [Fact]
        public async Task GetAllByMakeShouldReturnAllVehiclesWithGivenMake()
        {
            var spec = new List<string>
            {
                "4x4",
                "ElectricSeats",
            };
            var vehicleId1 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId2 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicles = this.vehicleService.GetAllByMake<VehiclesViewModel>("Audi");

            Assert.Equal(2, vehicles.Count());
        }

        [Fact]
        public async Task GetAllFilteredShouldSortAllByPriceAscending()
        {
            var spec = new List<string>
            {
                "4x4",
                "ElectricSeats",
            };
            var vehicleId1 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId2 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicles = this.vehicleService.GetAllFiltered("upPrice", 0, null, null);
            var actualVehicle = vehicles.Result.Vehicles.First();
            Assert.Equal(1, actualVehicle.Id);
        }

        [Fact]
        public async Task GetAllFilteredShouldReturnAllFromCarCategory()
        {
            var spec = new List<string>
            {
                "4x4",
                "ElectricSeats",
            };
            var vehicleId1 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId2 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId5 = await this.vehicleService.CreateVehicleAsync("Audi", "Q7", 2010, 202020, "SUVS", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId4 = await this.vehicleService.CreateVehicleAsync("Honda", "Hornet", 2010, 202020, "Motorcycle", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId3 = await this.vehicleService.CreateVehicleAsync("Fiat", "Ducato", 2010, 202020, "Bus", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");

            var vehicles = this.vehicleService.GetAllFiltered(null, 1, null, null);
            var vehicles1 = this.vehicleService.GetAllFiltered(null, 2, null, null);
            var vehicles2 = this.vehicleService.GetAllFiltered(null, 3, null, null);
            var vehicles3 = this.vehicleService.GetAllFiltered(null, 4, null, null);

            Assert.Equal(2, vehicles.Result.Vehicles.Count());
            Assert.Single(vehicles1.Result.Vehicles);
            Assert.Single(vehicles2.Result.Vehicles);
            Assert.Single(vehicles3.Result.Vehicles);
        }

        [Fact]
        public async Task GetAllFilteredShouldReturnAllFromCarCategoryAndSortedUpPrice()
        {
            var spec = new List<string>
            {
                "4x4",
                "ElectricSeats",
            };
            var vehicleId1 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 14000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId2 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId5 = await this.vehicleService.CreateVehicleAsync("Audi", "Q7", 2010, 202020, "SUVS", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId4 = await this.vehicleService.CreateVehicleAsync("Honda", "Hornet", 2010, 202020, "Motorcycle", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId3 = await this.vehicleService.CreateVehicleAsync("Fiat", "Ducato", 2010, 202020, "Bus", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");

            var vehicles = this.vehicleService.GetAllFiltered("upPrice", 1, null, null);
            var actualVehicle = vehicles.Result.Vehicles.First();

            Assert.Equal(1, actualVehicle.Id);
            Assert.Equal(2, vehicles.Result.Vehicles.Count());
        }
        [Fact]
        public async Task GetAllFilteredShouldReturnAllFromCarCategoryAndSortedUpPriceWithMakeAudi()
        {
            var spec = new List<string>
            {
                "4x4",
                "ElectricSeats",
            };
            var vehicleId1 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 14000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId2 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId5 = await this.vehicleService.CreateVehicleAsync("Audi", "Q7", 2010, 202020, "SUVS", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId4 = await this.vehicleService.CreateVehicleAsync("Honda", "Hornet", 2010, 202020, "Motorcycle", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId3 = await this.vehicleService.CreateVehicleAsync("Fiat", "Ducato", 2010, 202020, "Bus", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId6 = await this.vehicleService.CreateVehicleAsync("Ford", "Focus", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");

            var vehicles = this.vehicleService.GetAllFiltered("upPrice", 1, "Audi", null);
            var actualVehicle = vehicles.Result.Vehicles.First();
            var vehicleMakes = this.vehicleService.GetAll<VehiclesViewModel>().Result.Where(x => x.Make == "Audi" && x.CategoryId == 1);
            Assert.Equal(vehicleMakes.Count(), vehicles.Result.Vehicles.Count());
            Assert.Equal(1, actualVehicle.Id);
            Assert.Equal(2, vehicles.Result.Vehicles.Count());
        }

        [Fact]
        public async Task GetAllFilteredShouldReturnAllFromCarCategoryAndSortedUpPriceWithMakeAndModel()
        {
            var spec = new List<string>
            {
                "4x4",
                "ElectricSeats",
            };
            var vehicleId1 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 14000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId2 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId5 = await this.vehicleService.CreateVehicleAsync("Audi", "Q7", 2010, 202020, "SUVS", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId4 = await this.vehicleService.CreateVehicleAsync("Honda", "Hornet", 2010, 202020, "Motorcycle", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId3 = await this.vehicleService.CreateVehicleAsync("Fiat", "Ducato", 2010, 202020, "Bus", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId6 = await this.vehicleService.CreateVehicleAsync("Ford", "Focus", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");

            var vehicles = this.vehicleService.GetAllFiltered("upPrice", 1, "Audi", "A4");
            var actualVehicle = vehicles.Result.Vehicles.First();
            var vehicleMakes = this.vehicleService.GetAll<VehiclesViewModel>().Result.Where(x => x.Make == "Audi" && x.CategoryId == 1 && x.Model == "A4");
            Assert.Equal(vehicleMakes.Count(), vehicles.Result.Vehicles.Count());
            Assert.Equal(1, actualVehicle.Id);
            Assert.Equal(2, vehicles.Result.Vehicles.Count());
        }

        [Fact]
        public async Task GetAllFilteredShouldReturnAllFromCarCategoryWithMakeAndModel()
        {
            var spec = new List<string>
            {
                "4x4",
                "ElectricSeats",
            };
            var vehicleId1 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 14000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId2 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId5 = await this.vehicleService.CreateVehicleAsync("Audi", "Q7", 2010, 202020, "SUVS", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId4 = await this.vehicleService.CreateVehicleAsync("Honda", "Hornet", 2010, 202020, "Motorcycle", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId3 = await this.vehicleService.CreateVehicleAsync("Fiat", "Ducato", 2010, 202020, "Bus", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId6 = await this.vehicleService.CreateVehicleAsync("Ford", "Focus", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");

            var vehicles = this.vehicleService.GetAllFiltered(null, 1, "Audi", "A4");
            var actualVehicle = vehicles.Result.Vehicles.First();
            var vehicleMakes = this.vehicleService.GetAll<VehiclesViewModel>().Result.Where(x => x.Make == "Audi" && x.CategoryId == 1 && x.Model == "A4");
            Assert.Equal(vehicleMakes.Count(), vehicles.Result.Vehicles.Count());
            Assert.Equal(1, actualVehicle.Id);
            Assert.Equal(2, vehicles.Result.Vehicles.Count());
        }

        [Fact]
        public async Task GetAllFilteredShouldReturnAllWithGivenMakeAndModel()
        {
            var spec = new List<string>
            {
                "4x4",
                "ElectricSeats",
            };
            var vehicleId1 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 14000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId2 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId5 = await this.vehicleService.CreateVehicleAsync("Audi", "Q7", 2010, 202020, "SUVS", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId4 = await this.vehicleService.CreateVehicleAsync("Honda", "Hornet", 2010, 202020, "Motorcycle", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId3 = await this.vehicleService.CreateVehicleAsync("Fiat", "Ducato", 2010, 202020, "Bus", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId6 = await this.vehicleService.CreateVehicleAsync("Ford", "Focus", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");

            var vehicles = this.vehicleService.GetAllFiltered(null, 0, "Audi", "A4");
            var actualVehicle = vehicles.Result.Vehicles.First();
            var vehicleMakes = this.vehicleService.GetAll<VehiclesViewModel>().Result.Where(x => x.Make == "Audi" && x.Model == "A4");
            Assert.Equal(vehicleMakes.Count(), vehicles.Result.Vehicles.Count());
        }

        [Fact]
        public async Task GetAllFilteredShouldReturnAllWithGivenMake()
        {
            var spec = new List<string>
            {
                "4x4",
                "ElectricSeats",
            };
            var vehicleId1 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 14000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId2 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId5 = await this.vehicleService.CreateVehicleAsync("Audi", "Q7", 2010, 202020, "SUVS", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId4 = await this.vehicleService.CreateVehicleAsync("Honda", "Hornet", 2010, 202020, "Motorcycle", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId3 = await this.vehicleService.CreateVehicleAsync("Fiat", "Ducato", 2010, 202020, "Bus", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId6 = await this.vehicleService.CreateVehicleAsync("Ford", "Focus", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");

            var vehicles = this.vehicleService.GetAllFiltered(null, 0 , "Audi", null);
            var actualVehicle = vehicles.Result.Vehicles.First();
            //var vehicleMakes = this.vehicleService.GetAll<VehiclesViewModel>().Result.Where(x => x.Make == "Audi" );
            Assert.Equal(3, vehicles.Result.Vehicles.Count());
        }

        [Fact]
        public async Task GetAllFilteredShouldReturnAllWithGivenModel()
        {
            var spec = new List<string>
            {
                "4x4",
                "ElectricSeats",
            };
            var vehicleId1 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 14000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId2 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId5 = await this.vehicleService.CreateVehicleAsync("Audi", "Q7", 2010, 202020, "SUVS", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId4 = await this.vehicleService.CreateVehicleAsync("Honda", "Hornet", 2010, 202020, "Motorcycle", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId3 = await this.vehicleService.CreateVehicleAsync("Fiat", "Ducato", 2010, 202020, "Bus", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId6 = await this.vehicleService.CreateVehicleAsync("Ford", "Focus", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");

            var vehicles = this.vehicleService.GetAllFiltered(null, 0, null, "A4");
            var actualVehicle = vehicles.Result.Vehicles.First();
            //var vehicleMakes = this.vehicleService.GetAll<VehiclesViewModel>().Result.Where(x => x.Make == "Audi" );
            Assert.Equal(2, vehicles.Result.Vehicles.Count());
        }

        [Fact]
        public async Task GetAllFilteredShouldReturnAllWithGivenModelAndCategory()
        {
            var spec = new List<string>
            {
                "4x4",
                "ElectricSeats",
            };
            var vehicleId1 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 14000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId2 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId5 = await this.vehicleService.CreateVehicleAsync("Audi", "Q7", 2010, 202020, "SUVS", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId4 = await this.vehicleService.CreateVehicleAsync("Honda", "Hornet", 2010, 202020, "Motorcycle", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId3 = await this.vehicleService.CreateVehicleAsync("Fiat", "Ducato", 2010, 202020, "Bus", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId6 = await this.vehicleService.CreateVehicleAsync("Ford", "Focus", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");

            var vehicles = this.vehicleService.GetAllFiltered(null, 1, null, "A4");
            var vehicles2 = this.vehicleService.GetAllFiltered(null, 2, null, "Q7");
            //var actualVehicle = vehicles.Result.Vehicles.First();
            //var vehicleMakes = this.vehicleService.GetAll<VehiclesViewModel>().Result.Where(x => x.Make == "Audi" );
            Assert.Equal(2, vehicles.Result.Vehicles.Count());
            Assert.Single(vehicles2.Result.Vehicles);
        }

        [Fact]
        public async Task GetAllFilteredShouldReturnAllWithGivenCategoryIfVehiclesCountIsNull()
        {
            var spec = new List<string>
            {
                "4x4",
                "ElectricSeats",
            };
            var vehicleId1 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 14000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId2 = await this.vehicleService.CreateVehicleAsync("Audi", "A4", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId5 = await this.vehicleService.CreateVehicleAsync("Audi", "Q7", 2010, 202020, "SUVS", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId4 = await this.vehicleService.CreateVehicleAsync("Honda", "Hornet", 2010, 202020, "Motorcycle", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId3 = await this.vehicleService.CreateVehicleAsync("Fiat", "Ducato", 2010, 202020, "Bus", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");
            var vehicleId6 = await this.vehicleService.CreateVehicleAsync("Ford", "Focus", 2010, 202020, "Car", "Diesel", 15000, 150, "Automatic", spec, null, "asdasdakjsdh");

            var vehicles = this.vehicleService.GetAllFiltered(null, 1, null, "Q7");
            var vehicles1 = this.vehicleService.GetAllFiltered(null, 1, "Honda", null);
            //var actualVehicle = vehicles.Result.Vehicles.First();
            //var vehicleMakes = this.vehicleService.GetAll<VehiclesViewModel>().Result.Where(x => x.Make == "Audi" );
            Assert.Equal(3, vehicles.Result.Vehicles.Count());
            Assert.Equal(3, vehicles1.Result.Vehicles.Count());
        }
        private void InitializeMapping()
            => AutoMapperConfig.RegisterMappings(
                typeof(VehiclesViewModel).GetTypeInfo().Assembly,
                typeof(Vehicle).GetTypeInfo().Assembly,
                typeof(VehicleDetailsViewModel).GetTypeInfo().Assembly,
                typeof(AllVehiclesViewModel).GetTypeInfo().Assembly);

    }
}
