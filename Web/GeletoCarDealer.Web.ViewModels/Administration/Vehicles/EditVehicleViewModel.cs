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
    using GeletoCarDealer.Web.ViewModels.Administration.Images;
    using GeletoCarDealer.Web.ViewModels.Administration.Specifications;
    using Microsoft.AspNetCore.Http;

    public class EditVehicleViewModel : IMapFrom<Vehicle>, IMapTo<Vehicle>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Полето \"{0}\" трябва да бъде текст с минимална дължина {2} и максимална дължина {1}.")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Полето \"{0}\" трябва да бъде текст с минимална дължина {2} и максимална дължина {1}.")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        [Range(1, int.MaxValue, ErrorMessage = "Полето е задължително")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        [StringLength(50, MinimumLength = 1)]
        public string Category { get; set; }

        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        [Range(1, int.MaxValue, ErrorMessage = "Полето е задължително")]
        public int Milage { get; set; }

        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        public string FuelType { get; set; }

        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        [Range(1, int.MaxValue, ErrorMessage = "Полето е задължително")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        [Range(1, int.MaxValue, ErrorMessage = "Полето е задължително")]
        public int HorsePower { get; set; }

        [Required]
        public string TransmissionType { get; set; }

        [StringLength(500, MinimumLength = 1, ErrorMessage = "Полето \"{0}\" трябва да бъде текст с минимална дължина {2} и максимална дължина {1}.")]
        public string Description { get; set; }
    }
}
