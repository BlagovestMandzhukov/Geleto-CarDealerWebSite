namespace GeletoCarDealer.Web.ViewComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GeletoCarDealer.Data.Common.Repositories;
    using GeletoCarDealer.Data.Models;
    using GeletoCarDealer.Web.ViewModels.UsersArea.Vehicles;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "SearchBar")]
    public class SearchBarViewComponent : ViewComponent
    {
        private readonly IDeletableEntityRepository<Vehicle> vehiclesRepository;

        public SearchBarViewComponent(IDeletableEntityRepository<Vehicle> vehiclesRepository)
        {
            this.vehiclesRepository = vehiclesRepository;
        }

        public IViewComponentResult Invoke()
        {
            var model = new SearchBarViewModel
            {
                Models = this.vehiclesRepository.All().Select(x => x.Model)
                                    .ToList(),
                Makes = this.vehiclesRepository.All().Select(x => x.Make)
                                    .ToList(),
                Categories = this.vehiclesRepository.All().Select(x => x.Category.Name)
                                    .Distinct()
                                    .ToList(),
            };

            return this.View(model);
        }
    }
}
