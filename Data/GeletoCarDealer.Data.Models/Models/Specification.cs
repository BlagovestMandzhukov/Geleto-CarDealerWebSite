namespace GeletoCarDealer.Data.Models.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Specification
    {
        public Specification()
        {
            this.Vehicles = new HashSet<VehicleSpecification>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<VehicleSpecification> Vehicles { get; set; }
    }
}
