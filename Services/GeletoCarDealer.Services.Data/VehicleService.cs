namespace GeletoCarDealer.Services.Data
{
    using GeletoCarDealer.Data.Common.Repositories;
    using GeletoCarDealer.Data.Models;
    using GeletoCarDealer.Data.Models.Enums;
    using GeletoCarDealer.Web.ViewModels.Administration.Vehicles;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class VehicleService
    {
        private readonly IDeletableEntityRepository<Vehicle> vehicleRepository;

        public VehicleService(IDeletableEntityRepository<Vehicle> vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;
        }
        //public Task<int> CreateVehicleAsync(CreateInputModel model)
        //{

        //}
    }
}
