namespace GeletoCarDealer.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        Task<int> CreateCategory(string name);

        int GetCarsId(int id);

        int GetSuvId(int id);

        int GetMotorcycleId(int id);

        int GetBusId(int id);
    }
}
