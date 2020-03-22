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
    using Microsoft.EntityFrameworkCore;

    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> categoryRepository;
        private readonly IRepository<VehicleCategory> vehicleCategoryRepository;

        public CategoryService(
            IRepository<Category> categoryRepository,
            IRepository<VehicleCategory> vehicleCategoryRepository)
        {
            this.categoryRepository = categoryRepository;
            this.vehicleCategoryRepository = vehicleCategoryRepository;
        }

        public async Task AddVehicleToCategory(int vehicleId, int categoryId)
        {
            var vehicle = await this.vehicleCategoryRepository.All().FirstOrDefaultAsync(x => x.VehicleId == vehicleId);
            var category = await this.vehicleCategoryRepository.All().FirstOrDefaultAsync(x => x.CategoryId == categoryId);

            if (vehicle == null && category == null)
            {
                var vc = new VehicleCategory
                {
                    CategoryId = categoryId,
                    VehicleId = vehicleId,
                };

                await this.vehicleCategoryRepository.AddAsync(vc);
                await this.vehicleCategoryRepository.SaveChangesAsync();
            }
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
    }
}
