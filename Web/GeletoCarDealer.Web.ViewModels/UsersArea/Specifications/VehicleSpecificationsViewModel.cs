namespace GeletoCarDealer.Web.ViewModels.UsersArea.Specifications
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using GeletoCarDealer.Data.Models.Models;
    using GeletoCarDealer.Services.Mapping;

    public class VehicleSpecificationsViewModel : IMapFrom<Specification>
    {
        public string Name { get; set; }
    }
}
