namespace GeletoCarDealer.Web.ViewModels.Administration.Vehicles
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using GeletoCarDealer.Data.Models;
    using GeletoCarDealer.Services.Mapping;

    public class SearchBarVehiclesModelsViewModel : IMapFrom<Vehicle>
    {
        public string Make { get; set; }

        public string Model { get; set; }

        public int CategoryId { get; set; }
    }
}
