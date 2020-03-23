namespace GeletoCarDealer.Data.Models.Models
{
    using System.ComponentModel.DataAnnotations;

    public class VehicleSpecification
    {
        public int SpecificationId { get; set; }

        public virtual Specification Specification { get; set; }

        public int VehicleId { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
