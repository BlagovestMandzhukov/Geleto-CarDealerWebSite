namespace GeletoCarDealer.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum FuelType
    {
        [Display(Name = "Дизел")]
        Diesel = 1,

        [Display(Name = "Бензин")]
        Petrol = 2,

        [Display(Name = "Електрическа")]
        Electric = 3,

        [Display(Name = "Хибрид")]
        Hybrid = 4,
    }
}
