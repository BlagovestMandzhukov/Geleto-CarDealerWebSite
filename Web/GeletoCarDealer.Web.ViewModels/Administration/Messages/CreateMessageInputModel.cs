namespace GeletoCarDealer.Web.ViewModels.Administration.Messages
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class CreateMessageInputModel
    {
        [Required]
        [EmailAddress]
        public string SendToEmail { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "Полето \"{0}\" трябва да бъде текст с минимална дължина {2} и максимална дължина {1}.")]
        public string Subject { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 1, ErrorMessage = "Полето \"{0}\" трябва да бъде текст с минимална дължина {2} и максимална дължина {1}.")]
        public string Content { get; set; }
    }
}
