using System.Collections.Generic;

namespace GeletoCarDealer.Services.Data
{
    public interface IVehicleService
    {
        IEnumerable<T> GetModels<T>();

        IEnumerable<T> GetMakes<T>();

        IEnumerable<T> GetYear<T>();

        string AddVehicle();

        string EditVehicle(string id);
    }
}
