namespace GeletoCarDealer.Web.ViewModels.Administration.Vehicles
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllVehiclesViewModel
    {
        public IEnumerable<VehiclesViewModel> Vehicles { get; set; }
    }
}
