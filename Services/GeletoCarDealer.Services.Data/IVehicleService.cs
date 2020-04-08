namespace GeletoCarDealer.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using GeletoCarDealer.Data.Models;
    using GeletoCarDealer.Data.Models.Enums;
    using GeletoCarDealer.Data.Models.Models;
    using GeletoCarDealer.Web.ViewModels.Administration.Vehicles;
    using Microsoft.AspNetCore.Http;

    public interface IVehicleService
    {
        Task<int> CreateVehicleAsync(string make, string model, int year, int milage, string category, string fuelType, decimal price, int horsePower, string transmission, IList<string> specifications, IList<IFormFile> images, string description);

        IEnumerable<T> GetAll<T>(string orderBy = null, int? category = null);

        IEnumerable<T> GetAllDeleted<T>();

        T GetById<T>(int id);

        Task<int> EditVehicle(int id, string make, string model, int year, int milage, string category, string fuelType, decimal price, int horsePower, string transmission, string description);

        Task<bool> Delete(int id);

        Task<int> AddVehicleImagesAsync(int id, IList<IFormFile> images);

        Vehicle GetVehicle(int id);

        Task<int> RemoveVehicleMessageAsync(int id);

        int AddMessageToVehicle(int id, string sentBy, string email, string phoneNumber, string messageContent);

        IEnumerable<T> GetVehicleModels<T>();

        IEnumerable<T> GetVehicleCategories<T>();

        IEnumerable<T> GetVehicleMakes<T>();
    }
}
