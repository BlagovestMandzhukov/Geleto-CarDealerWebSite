namespace GeletoCarDealer.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using GeletoCarDealer.Data;
    using GeletoCarDealer.Data.Models;
    using GeletoCarDealer.Data.Models.Models;
    using GeletoCarDealer.Data.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class CategoryServiceTests
    {
        [Fact]
        public async Task CreateCategoryShouldCreateNewCategory()
        {
            var options = new DbContextOptionsBuilder<GeletoDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new GeletoDbContext(options);

            var categoryRepository = new EfRepository<Category>(context);
            var vehicleRepository = new EfDeletableEntityRepository<Vehicle>(context);

            var categoryService = new CategoryService(categoryRepository, vehicleRepository);

            int carId = await categoryService.CreateCategory("Car");
            int expectedResult = 1;

            Assert.Equal(expectedResult, carId);
        }

        [Fact]
        public async Task GetCarIdShouldReturnCarCategoryId()
        {
            var options = new DbContextOptionsBuilder<GeletoDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new GeletoDbContext(options);

            var categoryRepository = new EfRepository<Category>(context);
            var vehicleRepository = new EfDeletableEntityRepository<Vehicle>(context);

            var categoryService = new CategoryService(categoryRepository, vehicleRepository);

            int carId = await categoryService.CreateCategory("Car");
            int carCategoryId = categoryService.GetCarsId(carId);
            int expectedResult = 1;

            Assert.Equal(expectedResult, carCategoryId);
        }

        [Fact]
        public async Task GetBusIdShouldReturnBusCategoryId()
        {
            var options = new DbContextOptionsBuilder<GeletoDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new GeletoDbContext(options);

            var categoryRepository = new EfRepository<Category>(context);
            var vehicleRepository = new EfDeletableEntityRepository<Vehicle>(context);

            var categoryService = new CategoryService(categoryRepository, vehicleRepository);

            int busId = await categoryService.CreateCategory("Bus");
            int busCategoryId = categoryService.GetBusId(busId);

            Assert.Equal(busId, busCategoryId);
        }
        [Fact]
        public async Task GetMotorcycleIdShouldReturnMotorcycleCategoryId()
        {
            var options = new DbContextOptionsBuilder<GeletoDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new GeletoDbContext(options);

            var categoryRepository = new EfRepository<Category>(context);
            var vehicleRepository = new EfDeletableEntityRepository<Vehicle>(context);

            var categoryService = new CategoryService(categoryRepository, vehicleRepository);

            int motorcycleId = await categoryService.CreateCategory("Motorcycle");

            int motorcycleCategoryId = categoryService.GetMotorcycleId(motorcycleId);

            Assert.Equal(motorcycleId, motorcycleCategoryId);
        }
        [Fact]
        public async Task GetSuvsIdShouldReturnSuvsCategoryId()
        {
            var options = new DbContextOptionsBuilder<GeletoDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new GeletoDbContext(options);

            var categoryRepository = new EfRepository<Category>(context);
            var vehicleRepository = new EfDeletableEntityRepository<Vehicle>(context);

            var categoryService = new CategoryService(categoryRepository, vehicleRepository);

            int suvId = await categoryService.CreateCategory("SUVS");

            int suvCategoryId = categoryService.GetSuvId(suvId);

            Assert.Equal(suvId, suvCategoryId);
        }
        [Fact]
        public async Task CreateCategoryShouldNotCreateIfNull()
        {
            var options = new DbContextOptionsBuilder<GeletoDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new GeletoDbContext(options);

            var categoryRepository = new EfRepository<Category>(context);
            var vehicleRepository = new EfDeletableEntityRepository<Vehicle>(context);

            var categoryService = new CategoryService(categoryRepository, vehicleRepository);

            int cat = await categoryService.CreateCategory(null);
            int expected = 0;
            Assert.Equal(expected, cat);
        }
        [Fact]
        public async Task CreateCategoryShouldNotCreateIfstringIsEmpty()
        {
            var options = new DbContextOptionsBuilder<GeletoDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new GeletoDbContext(options);

            var categoryRepository = new EfRepository<Category>(context);
            var vehicleRepository = new EfDeletableEntityRepository<Vehicle>(context);

            var categoryService = new CategoryService(categoryRepository, vehicleRepository);

            int cat = await categoryService.CreateCategory("");
            int expected = 0;
            Assert.Equal(expected, cat);
        }
    }
}
