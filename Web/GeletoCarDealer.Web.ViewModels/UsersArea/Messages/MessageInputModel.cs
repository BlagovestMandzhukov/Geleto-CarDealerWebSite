namespace GeletoCarDealer.Web.ViewModels.UsersArea.Messages
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using GeletoCarDealer.Data.Models;
    using GeletoCarDealer.Data.Models.Models;
    using GeletoCarDealer.Services.Mapping;

    public class MessageInputModel : IMapTo<Message>
    {
        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Полето \"{0}\" трябва да бъде текст с минимална дължина {2} и максимална дължина {1}.")]
        [Display(Name = "Име")]
        public string SendBy { get; set; }

        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        [EmailAddress(ErrorMessage ="Невалиден емайл адрес!")]
        [Display(Name = "Емайл")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        [Display(Name = "Телефон")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Невалиден телефонен номер.")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Невалиден телефонен номер.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Полето \"{0}\" e задължително.")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "Полето \"{0}\" трябва да бъде текст с минимална дължина {2} и максимална дължина {1}.")]
        [Display(Name = "Съдържание")]
        public string MessageContent { get; set; }
    }
}
