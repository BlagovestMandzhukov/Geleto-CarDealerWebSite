namespace GeletoCarDealer.Web.ViewModels.Administration.Specifications
{
    using GeletoCarDealer.Data.Models.Enums;

    public class SpecificationsInputModel
    {
        public int Id { get; set; }

        public Specifications Specification { get; set; }

        public bool IsSelected { get; set; }
    }
}
