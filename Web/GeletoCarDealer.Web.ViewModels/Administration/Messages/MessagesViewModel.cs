namespace GeletoCarDealer.Web.ViewModels.Administration.Messages
{
    using System;

    using GeletoCarDealer.Data.Models.Models;
    using GeletoCarDealer.Services.Mapping;
    using GeletoCarDealer.Web.ViewModels.Administration.Vehicles;

    public class MessagesViewModel : IMapFrom<Message>
    {
        public int Id { get; set; }

        public string SendBy { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string MessageContent { get; set; }

        public DateTime CreatedOn { get; set; }

        public int VehicleId { get; set; }

        public VehiclesViewModel Vehicle { get; set; }
    }
}
