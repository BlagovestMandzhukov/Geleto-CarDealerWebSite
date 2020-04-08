namespace GeletoCarDealer.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        Task<int> CreateCategory(string name);

        int GetAllCars(int id);

        int GetAllSuvs(int id);

        int GetAllMotorcycles(int id);

        int GetAllBuses(int id);
    }
}
