namespace GeletoCarDealer.Data.Models.Models
{
    using System.Collections.Generic;

    public class VehicleMakeSelect
    {
        public VehicleMakeSelect()
        {
            this.Models = new HashSet<VehicleModelSelect>();
            this.Years = new HashSet<VehicleYearSelect>();
        }

        public int Id { get; set; }

        public string Make { get; set; }

        public virtual ICollection<VehicleModelSelect> Models { get; set; }

        public virtual ICollection<VehicleYearSelect> Years { get; set; }
    }
}
