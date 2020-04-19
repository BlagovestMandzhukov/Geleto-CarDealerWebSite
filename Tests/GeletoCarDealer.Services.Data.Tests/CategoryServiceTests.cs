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

            int catId = await categoryService.CreateCategory("Car");
            int expectedResult = 1;

            Assert.Equal(expectedResult, catId);
        }
    }
}
