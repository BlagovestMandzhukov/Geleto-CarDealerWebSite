namespace GeletoCarDealer.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum CategoryType
    {
        [Display(Name = "Автомобили")]
        Car = 1,

        [Display(Name = "Мотоциклети")]
        Motorcycle = 2,

        [Display(Name = "Бусове")]
        Bus = 3,

        [Display(Name = "Камиони")]
        Truck = 4,

        [Display(Name = "Джипове")]
        SUVS = 5,
    }
}
