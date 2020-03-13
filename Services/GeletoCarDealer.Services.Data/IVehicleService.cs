namespace GeletoCarDealer.Services.Data
{
    using System.Collections.Generic;

    public interface IVehicleService
    {
        IEnumerable<T> GetMakesWithModels<T>(int id);

        string AddVehicle();

        string EditVehicle(string id);
    }
}
