using System.ComponentModel.DataAnnotations;

namespace GeletoCarDealer.Data.Models.Models
{
    public class VehicleSpecificationsSelect
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
