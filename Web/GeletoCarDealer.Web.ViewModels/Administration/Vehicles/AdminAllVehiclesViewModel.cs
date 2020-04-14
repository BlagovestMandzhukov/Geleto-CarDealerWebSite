namespace GeletoCarDealer.Web.ViewModels.Administration.Vehicles
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AdminAllVehiclesViewModel
    {
        public IEnumerable<AdminVehiclesViewModel> Vehicles { get; set; }
    }
}
