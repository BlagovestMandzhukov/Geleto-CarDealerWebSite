namespace GeletoCarDealer.Web.ViewModels.Administration.Vehicles
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using GeletoCarDealer.Data.Models;
    using GeletoCarDealer.Data.Models.Models;
    using GeletoCarDealer.Services.Mapping;
    using GeletoCarDealer.Web.ViewModels.Administration.Images;

    public class VehicleDetailsViewModel : IMapFrom<Vehicle>, IMapFrom<Image>, IMapFrom<Specification>
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

        //public ICollection<string> Specifications { get; set; }

        //public void CreateMappings(IProfileExpression configuration)
        //{
        //    configuration.CreateMap<Vehicle, VehicleDetailsViewModel>()
        //        .ForMember(x => x.ImageUrl, cfg => cfg.MapFrom(x => x.Images.FirstOrDefault().ImageUrl));
        //}
    }
}
