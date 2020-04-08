namespace GeletoCarDealer.Web.ViewComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GeletoCarDealer.Data.Common.Repositories;
    using GeletoCarDealer.Data.Models;
    using GeletoCarDealer.Services.Data;
    using GeletoCarDealer.Web.ViewModels.UsersArea.Vehicles;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "SearchBar")]
    public class SearchBarViewComponent : ViewComponent
    {
        private readonly IDeletableEntityRepository<Vehicle> vehiclesRepository;
        private readonly IVehicleService vehicleService;

        public SearchBarViewComponent(IDeletableEntityRepository<Vehicle> vehiclesRepository,IVehicleService vehicleService)
        {
            this.vehiclesRepository = vehiclesRepository;
            this.vehicleService = vehicleService;
        }

        public IViewComponentResult Invoke()
        {
            var model = new SearchBarViewModel
            {
                Models = this.vehicleService.GetVehicleModels<VehicleModelsViewModel>(),

                Makes = this.vehicleService.GetVehicleMakes<VehicleMakeViewModel>(),
            };
            return this.View(model);
        }
    }
}
