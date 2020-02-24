namespace GeletoCarDealer.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(GeletoDbContext dbContext, IServiceProvider serviceProvider);
    }
}
