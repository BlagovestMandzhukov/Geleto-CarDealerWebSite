namespace GeletoCarDealer.Web.ViewModels.UsersArea.Vehicles
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllVehiclesViewModel
    {
        public IEnumerable<VehiclesViewModel> Vehicles { get; set; }

        public IEnumerable<VehicleMakeViewModel> Makes { get; set; }

        public IEnumerable<VehicleModelsViewModel> Models { get; set; }

        public int Category { get; set; }

        public string OrderBy { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }
    }
}
