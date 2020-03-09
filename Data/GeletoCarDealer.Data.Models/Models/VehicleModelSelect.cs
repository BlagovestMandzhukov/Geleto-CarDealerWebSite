namespace GeletoCarDealer.Data.Models.Models
{
    public class VehicleModelSelect
    {
        public int Id { get; set; }

        public string Model { get; set; }

        public int VehicleMakeSelectId { get; set; }

        public virtual VehicleMakeSelect VehicleMakeSelect { get; set; }
    }
}
