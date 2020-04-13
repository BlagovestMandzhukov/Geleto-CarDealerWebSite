namespace GeletoCarDealer.Web.ViewModels.UsersArea.Vehicles
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using GeletoCarDealer.Data.Models;
    using GeletoCarDealer.Services.Mapping;

    public class VehicleModelsViewModel : IMapFrom<Vehicle>
    {
        public string Make { get; set; }

        public string Model { get; set; }

        public int CategoryId { get; set; }
    }
}
