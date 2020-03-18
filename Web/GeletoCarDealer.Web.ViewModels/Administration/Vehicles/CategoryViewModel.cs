namespace GeletoCarDealer.Web.ViewModels.Administration.Vehicles
{
    using GeletoCarDealer.Data.Models.Models;
    using GeletoCarDealer.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
