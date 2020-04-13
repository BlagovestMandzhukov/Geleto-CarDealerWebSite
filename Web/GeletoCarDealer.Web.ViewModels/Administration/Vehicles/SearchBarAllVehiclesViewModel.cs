namespace GeletoCarDealer.Web.ViewModels.Administration.Vehicles
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SearchBarAllVehiclesViewModel
    {
        public IEnumerable<VehiclesViewModel> Vehicles { get; set; }

        public IEnumerable<SearchBarVehiclesMakesViewModel> Makes { get; set; }

        public IEnumerable<SearchBarVehiclesModelsViewModel> Models { get; set; }

        public int Category { get; set; }

        public string OrderBy { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }
    }
}