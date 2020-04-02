namespace GeletoCarDealer.Data.Models.Models
{
    using System.ComponentModel.DataAnnotations;

    using GeletoCarDealer.Data.Common.Models;

    public class Message : BaseDeletableModel<int>
    {
        [Required]
        public string SendBy { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public int PhoneNumber { get; set; }

        public int VehicleId { get; set; }

        public virtual Vehicle Vehicle { get; set; }

        public string Content { get; set; }
    }
}
