namespace GeletoCarDealer.Web.ViewModels.Administration.Images
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using GeletoCarDealer.Data.Models;
    using GeletoCarDealer.Services.Mapping;

    public class AllImagesViewModel : IMapFrom<Vehicle>
    {
        public int VehicleId { get; set; }

        public IEnumerable<ImagesViewModel> Images { get; set; }
    }
}
