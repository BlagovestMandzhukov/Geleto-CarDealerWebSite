namespace GeletoCarDealer.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using GeletoCarDealer.Data.Common.Repositories;
    using GeletoCarDealer.Data.Models;
    using GeletoCarDealer.Data.Models.Models;
    using Microsoft.EntityFrameworkCore;

    public class MessageService : IMessageService
    {
        private readonly IDeletableEntityRepository<Message> messagesRepository;
        private readonly IVehicleService vehicleService;
        private readonly IDeletableEntityRepository<Vehicle> vehicleRepository;

        public MessageService(
            IDeletableEntityRepository<Message> messagesRepository,
            IVehicleService vehicleService,
            IDeletableEntityRepository<Vehicle> vehicleRepository)
        {
            this.messagesRepository = messagesRepository;
            this.vehicleService = vehicleService;
            this.vehicleRepository = vehicleRepository;
        }

        public bool CreateMessage(int id, string sentBy, string email, string phoneNumber, string messageContent)
        {
            var vehicle = this.vehicleService.GetById<Vehicle>(id);
            var message = new Message
            {
                SendBy = sentBy,
                Email = email,
                PhoneNumber = phoneNumber,
                MessageContent = messageContent,
            };

            vehicle.Messages.Add(message);
            this.vehicleRepository.SaveChangesAsync();
            return true;
        }
    }
}
