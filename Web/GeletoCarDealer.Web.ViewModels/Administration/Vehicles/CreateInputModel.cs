namespace GeletoCarDealer.Web.ViewModels.Administration.Vehicles
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using GeletoCarDealer.Data.Models;
    using GeletoCarDealer.Data.Models.Enums;
    using GeletoCarDealer.Data.Models.Models;
    using GeletoCarDealer.Services.Mapping;

    public class CreateInputModel
    {
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Make { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Model { get; set; }

        public IEnumerable<Image> Images { get; set; }

        public IList<SpecificationsViewModel> Specifications { get; set; }

        [Range(1, int.MaxValue)]
        public int Year { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public CategoryType Category { get; set; }

        [Range(1, int.MaxValue)]
        public int Milage { get; set; }

        [Required]
        public FuelType FuelType { get; set; }

        [Range(1, int.MaxValue)]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue)]
        public int HorsePower { get; set; }

        [Required]
        public TransmissionType TransmissionType { get; set; }
    }
}
