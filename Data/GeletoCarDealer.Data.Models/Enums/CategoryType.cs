namespace GeletoCarDealer.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum CategoryType
    {
        [Display(Name = "Автомобили и джипове")]
        Car = 1,

        [Display(Name = "Мотоциклет")]
        Motorcycle = 2,

        [Display(Name = "Бус")]
        Bus = 3,

        [Display(Name = "Камион")]
        Truck = 4,
    }
}
