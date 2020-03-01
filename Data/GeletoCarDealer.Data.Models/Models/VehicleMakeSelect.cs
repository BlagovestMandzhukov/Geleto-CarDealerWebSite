namespace GeletoCarDealer.Data.Models.Models
{
    using System.Collections.Generic;

    public class VehicleMakeSelect
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public virtual ICollection<VehicleModelSelect> Models { get; set; } = new HashSet<VehicleModelSelect>();

        public virtual ICollection<VehicleYearSelect> Years { get; set; } = new HashSet<VehicleYearSelect>();
    }
}
