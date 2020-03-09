namespace GeletoCarDealer.Data.Models.Models
{
    using System.ComponentModel.DataAnnotations;

    public class VehicleSpecificationsSelect
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
