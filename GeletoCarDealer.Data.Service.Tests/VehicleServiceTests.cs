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
            var formFile = new Mock<IFormFile>();
            var PhysicalFile = new FileInfo(@"C:\Users\Liliya\Desktop\TestImages\0.jpg");
            var memory = new MemoryStream();
            var writer = new StreamWriter(memory);
            writer.Write(PhysicalFile.OpenRead());
            writer.Flush();
            memory.Position = 0;
            var fileName = PhysicalFile.Name;
            formFile.Setup(_ => _.FileName).Returns(fileName);
            formFile.Setup(_ => _.Length).Returns(memory.Length);
            formFile.Setup(_ => _.OpenReadStream()).Returns(memory);
            formFile.Verify();
            var images = new List<IFormFile>
            {
                formFile.Object,
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
            var id =  await this.vehicleService.RemoveVehicleMessageAsync(message.Id);
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

        private void InitializeMapping()
            => AutoMapperConfig.RegisterMappings(
                typeof(VehiclesViewModel).GetTypeInfo().Assembly,
                typeof(Vehicle).GetTypeInfo().Assembly,
                typeof(VehicleDetailsViewModel).GetTypeInfo().Assembly,
                typeof(AllVehiclesViewModel).GetTypeInfo().Assembly);

    }
}
