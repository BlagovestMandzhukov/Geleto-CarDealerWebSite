namespace GeletoCarDealer.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using GeletoCarDealer.Data.Common.Repositories;
    using GeletoCarDealer.Data.Models;
    using GeletoCarDealer.Data.Models.Models;
    using GeletoCarDealer.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> categoryRepository;
        private readonly IDeletableEntityRepository<Vehicle> vehicleRepository;

        public CategoryService(
            IRepository<Category> categoryRepository,
            IDeletableEntityRepository<Vehicle> vehicleRepository)
        {
            this.categoryRepository = categoryRepository;
            this.vehicleRepository = vehicleRepository;
        }

        public async Task<int> CreateCategory(string name)
        {
            var cat = await this.categoryRepository.All().FirstOrDefaultAsync(x => x.Name == name);
            if (cat == null)
            {
                cat = new Category
                {
                    Name = name,
                };

                await this.categoryRepository.AddAsync(cat);
                await this.categoryRepository.SaveChangesAsync();
            }

            return cat.Id;
        }

        public int GetAllCars(int id)
        {
            var categoryId = this.categoryRepository.All().Where(x => x.Name == "Car").Select(x => x.Id).FirstOrDefault();

            if (categoryId == 0)
            {
                return 0;
            }

            return categoryId;
        }

        public int GetAllSuvs(int id)
        {
            var categoryId = this.categoryRepository.All().Where(x => x.Name == "SUVS").Select(x => x.Id).FirstOrDefault();

            if (categoryId == 0)
            {
                return 0;
            }

            return categoryId;
        }

        public int GetAllMotorcycles(int id)
        {
            var categoryId = this.categoryRepository.All().Where(x => x.Name == "Motorcycle").Select(x => x.Id).FirstOrDefault();

            if (categoryId == 0)
            {
                return 0;
            }

            return categoryId;
        }

        public int GetAllBuses(int id)
        {
            var categoryId = this.categoryRepository.All().Where(x => x.Name == "Motorcycle").Select(x => x.Id).FirstOrDefault();

            if (categoryId == 0)
            {
                return 0;
            }

            return categoryId;
        }
    }
}
