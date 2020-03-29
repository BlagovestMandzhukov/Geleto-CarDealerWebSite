namespace GeletoCarDealer.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using GeletoCarDealer.Web.ViewModels.UsersArea.Vehicles;

    public class SideBarViewModel
    {
       public IEnumerable<SideBarVehiclesViewModel> Vehicles { get; set; }
    }
}
