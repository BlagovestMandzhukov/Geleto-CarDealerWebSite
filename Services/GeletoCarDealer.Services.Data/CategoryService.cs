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

        public CategoryService(
            IRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
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
