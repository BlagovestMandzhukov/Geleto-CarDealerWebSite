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
            if (string.IsNullOrEmpty(name))
            {
                return 0;
            }

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

        public int GetCarsId(int id)
        {
            var categoryId = this.categoryRepository.All().Where(x => x.Name == "Car").Select(x => x.Id).FirstOrDefault();

            if (categoryId == 0)
            {
                return 0;
            }

            return categoryId;
        }

        public int GetSuvId(int id)
        {
            var categoryId = this.categoryRepository.All().Where(x => x.Name == "SUVS").Select(x => x.Id).FirstOrDefault();

            if (categoryId == 0)
            {
                return 0;
            }

            return categoryId;
        }

        public int GetMotorcycleId(int id)
        {
            var categoryId = this.categoryRepository.All().Where(x => x.Name == "Motorcycle").Select(x => x.Id).FirstOrDefault();

            if (categoryId == 0)
            {
                return 0;
            }

            return categoryId;
        }

        public int GetBusId(int id)
        {
            var categoryId = this.categoryRepository.All().Where(x => x.Name == "Bus").Select(x => x.Id).FirstOrDefault();

            if (categoryId == 0)
            {
                return 0;
            }

            return categoryId;
        }
    }
}
