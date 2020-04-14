namespace GeletoCarDealer.Web.ViewModels.Administration.Vehicles
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using GeletoCarDealer.Data.Models;
    using GeletoCarDealer.Data.Models.Models;
    using GeletoCarDealer.Services.Mapping;
    using GeletoCarDealer.Web.ViewModels.Administration.Images;
    using GeletoCarDealer.Web.ViewModels.Administration.Specifications;

    public class AdminVehicleDetailsViewModel : IMapFrom<Vehicle>, IMapFrom<Image>, IMapFrom<Specification>
    {
        public int Id { get; set; }

        public string Model { get; set; }

        public string Make { get; set; }

        public int Year { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string TransmissionType { get; set; }

        public string FuelType { get; set; }

        public int HorsePower { get; set; }

        public int Milage { get; set; }

        public IList<ImagesViewModel> Images{ get; set; }

        public ICollection<VehicleSpecificationsViewModel> Specifications { get; set; }

    }
}
