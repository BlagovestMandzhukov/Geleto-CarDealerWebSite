namespace GeletoCarDealer.Data.Models.Models
{
    public class VehicleCategory
    {
        public int VehicleId { get; set; }

        public virtual Vehicle Vehicle { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
