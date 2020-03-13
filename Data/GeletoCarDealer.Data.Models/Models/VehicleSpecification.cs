namespace GeletoCarDealer.Data.Models.Models
{
    using System.ComponentModel.DataAnnotations;

    public class VehicleSpecification
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int VehicleId { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
