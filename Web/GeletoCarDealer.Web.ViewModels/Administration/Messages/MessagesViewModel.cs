namespace GeletoCarDealer.Web.ViewModels.Administration.Messages
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using GeletoCarDealer.Data.Models.Models;
    using GeletoCarDealer.Services.Mapping;

    public class MessagesViewModel : IMapFrom<Message>
    {
        public string SendBy { get; set; }

        public string Email { get; set; }

        public int PhoneNumber { get; set; }

        public string MessageContent { get; set; }
    }
}
