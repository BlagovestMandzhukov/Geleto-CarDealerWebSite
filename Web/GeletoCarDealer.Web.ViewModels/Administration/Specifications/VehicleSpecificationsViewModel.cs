namespace GeletoCarDealer.Web.ViewModels.Administration.Specifications
{
    using GeletoCarDealer.Data.Models.Models;
    using GeletoCarDealer.Services.Mapping;

    public class VehicleSpecificationsViewModel : IMapFrom<Specification>
    {
        public string Name { get; set; }
    }
}
