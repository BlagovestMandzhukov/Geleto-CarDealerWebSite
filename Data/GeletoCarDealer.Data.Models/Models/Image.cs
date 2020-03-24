namespace GeletoCarDealer.Data.Models.Models
{
    using GeletoCarDealer.Data.Common.Models;

    public class Image : BaseDeletableModel<int>
    {
        public string ImageUrl { get; set; }

        public int VehicleId { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
