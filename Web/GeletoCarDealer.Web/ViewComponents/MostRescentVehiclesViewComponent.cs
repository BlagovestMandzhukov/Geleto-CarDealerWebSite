namespace GeletoCarDealer.Web.ViewComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GeletoCarDealer.Data.Common.Repositories;
    using GeletoCarDealer.Data.Models;
    using GeletoCarDealer.Services.Mapping;
    using GeletoCarDealer.Web.ViewModels;
    using GeletoCarDealer.Web.ViewModels.UsersArea.Vehicles;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "MostRescentVehicles")]
    public class MostRescentVehiclesViewComponent : ViewComponent
    {
        private readonly IDeletableEntityRepository<Vehicle> vehiclesRepository;

        public MostRescentVehiclesViewComponent(IDeletableEntityRepository<Vehicle> vehiclesRepository)
        {
            this.vehiclesRepository = vehiclesRepository;
        }

        public IViewComponentResult Invoke()
        {
            var model = new MostRescentVehiclesViewModel
            {
                Vehicles = this.vehiclesRepository.All().OrderByDescending(x => x.CreatedOn).Take(3)
                                    .To<VehiclesViewModel>().ToList(),
            };

            return this.View(model);
        }
    }
}
