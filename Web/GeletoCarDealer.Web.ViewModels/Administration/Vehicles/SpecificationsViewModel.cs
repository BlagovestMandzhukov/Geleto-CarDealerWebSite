namespace GeletoCarDealer.Web.ViewModels.Administration.Vehicles
{
    using GeletoCarDealer.Data.Models.Enums;

    public class SpecificationsViewModel
    {
        public int Id { get; set; }

        public Specifications Specification { get; set; }

        public bool IsSelected { get; set; }
    }
}
