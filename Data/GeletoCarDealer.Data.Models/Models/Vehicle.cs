namespace GeletoCarDealer.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using GeletoCarDealer.Data.Models.Enums;
    using GeletoCarDealer.Data.Models.Models;

    public class Vehicle
    {
        public int Id { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        public virtual ICollection<Image> Images { get; set; } = new HashSet<Image>();

        [Required]
        public DateTime AddedOn { get; set; }

        public virtual ICollection<Specification> Specifications { get; set; } = new HashSet<Specification>();

        public int Year { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public int Milage { get; set; }

        public FuelType FuelType { get; set; }

        public decimal Price { get; set; }

        public int HorsePower { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
