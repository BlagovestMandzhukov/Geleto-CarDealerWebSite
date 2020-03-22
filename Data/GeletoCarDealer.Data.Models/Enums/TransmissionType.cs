using System.ComponentModel.DataAnnotations;

namespace GeletoCarDealer.Data.Models.Enums
{
    public enum TransmissionType
    {
        [Display(Name = "Ръчна")]
        Manual = 1,

        [Display(Name = "Автоматична")]
        Automatic = 2,

        [Display(Name = "Полуавтоматична")]
        SemiAutomatic = 3,
    }
}
