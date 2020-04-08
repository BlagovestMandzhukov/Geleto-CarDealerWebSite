namespace GeletoCarDealer.Web.ViewModels.UsersArea.Vehicles
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using GeletoCarDealer.Data.Models;

    public class SearchBarViewModel
    {
        public IEnumerable<VehicleMakeViewModel> Makes { get; set; }

        public IEnumerable<VehicleModelsViewModel> Models { get; set; }

    }
}
