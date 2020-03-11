namespace GeletoCarDealer.Web.ViewModels.Administration.Vehicles
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using GeletoCarDealer.Data.Models.Models;

    public class CreateInputModel
    {
        public int Id { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public IEnumerable<VehicleModelSelect> Models { get; set; }

        [Required]
        public IEnumerable<Image> Images { get; set; }

        [Required]
        public IEnumerable<Specification> Specifications { get; set; }

        public int Year { get; set; }

        [Required]
        public string Category { get; set; }

        public int Milage { get; set; }

        public string FuelType { get; set; }

        public decimal Price { get; set; }

        public int HorsePower { get; set; }

        [Required]
        public string TransmissionType { get; set; }
    }
}
