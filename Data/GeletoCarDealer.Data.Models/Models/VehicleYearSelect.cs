namespace GeletoCarDealer.Data.Models.Models
{
    public class VehicleYearSelect
    {
        public int Id { get; set; }

        public int Year { get; set; }

        public int VehicleMakeId { get; set; }

        public virtual VehicleMakeSelect VehicleMakeSelect { get; set; }
    }
}
