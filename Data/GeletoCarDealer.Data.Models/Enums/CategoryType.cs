namespace GeletoCarDealer.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum CategoryType
    {
        [Display(Name = "Автомобили")]
        Car = 1,

        [Display(Name = "Джипове")]
        SUVS = 2,

        [Display(Name = "Мотоциклети")]
        Motorcycle = 3,

        [Display(Name = "Бусове")]
        Bus = 4,

    }
}
