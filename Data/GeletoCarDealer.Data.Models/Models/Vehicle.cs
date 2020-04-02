namespace GeletoCarDealer.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using GeletoCarDealer.Data.Common.Models;
    using GeletoCarDealer.Data.Models.Models;

    public class Vehicle : BaseDeletableModel<int>
    {
        public Vehicle()
        {
            this.Images = new HashSet<Image>();
            this.Specifications = new HashSet<Specification>();
            this.Messages = new HashSet<Message>();
        }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Range(1, int.MaxValue)]
        public int Year { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public int Milage { get; set; }

        public string FuelType { get; set; }

        [Range(1, int.MaxValue)]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue)]
        public int HorsePower { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public string TransmissionType { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Specification> Specifications { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
