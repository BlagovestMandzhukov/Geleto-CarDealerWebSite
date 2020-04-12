namespace GeletoCarDealer.Web.ViewModels.UsersArea.Vehicles
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllVehiclesViewModel
    {
        public IEnumerable<VehiclesViewModel> Vehicles { get; set; }

        public int Category { get; set; }

        public string OrderBy { get; set; }
    }
}