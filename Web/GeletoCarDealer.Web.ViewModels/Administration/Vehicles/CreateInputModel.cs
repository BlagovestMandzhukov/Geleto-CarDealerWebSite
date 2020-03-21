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

    using Microsoft.AspNetCore.Http;

    public class CreateInputModel : IMapTo<Vehicle>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Полето \"{0}\" трябва да бъде текст с минимална дължина {2} и максимална дължина {1}.")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Полето \"{0}\" трябва да бъде текст с минимална дължина {2} и максимална дължина {1}.")]
        public string Model { get; set; }

        public ICollection<IFormFile> Images { get; set; }

        public IList<SpecificationsViewModel> Specifications { get; set; }

        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        [Range(1, int.MaxValue)]
        public int Year { get; set; }

        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        [StringLength(50, MinimumLength = 1)]
        public CategoryType Category { get; set; }

        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        [Range(1, int.MaxValue)]
        public int Milage { get; set; }

        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        public FuelType FuelType { get; set; }

        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        [Range(1, int.MaxValue)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        [Range(1, int.MaxValue)]
        public int HorsePower { get; set; }

        [Required]
        public TransmissionType TransmissionType { get; set; }
    }
}
