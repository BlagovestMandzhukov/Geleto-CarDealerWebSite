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

    [ViewComponent(Name = "Sidebar")]
    public class SideBarViewComponent : ViewComponent
    {
        private readonly IDeletableEntityRepository<Vehicle> vehiclesRepository;

        public SideBarViewComponent(IDeletableEntityRepository<Vehicle> vehiclesRepository)
        {
            this.vehiclesRepository = vehiclesRepository;
        }

        public IViewComponentResult Invoke(int id)
        {
            var model = new SideBarViewModel
            {
                 Vehicles = this.vehiclesRepository.All().Where(x => x.Id != id).Take(2)
                                    .To<SideBarVehiclesViewModel>().ToList(),
            };

            return this.View(model);
        }
    }
}
