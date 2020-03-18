namespace GeletoCarDealer.Services.Data
{
    using System.Threading.Tasks;

    using GeletoCarDealer.Web.ViewModels.Administration.Vehicles;

    public interface IVehicleService
    {
        Task<int> CreateVehicleAsync();
    }
}
