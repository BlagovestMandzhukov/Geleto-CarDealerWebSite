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
    using GeletoCarDealer.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class MessageService : IMessageService
    {
        private readonly IDeletableEntityRepository<Message> messagesRepository;
        private readonly IVehicleService vehicleService;
        private readonly IDeletableEntityRepository<Vehicle> vehicleRepository;

        public MessageService(
            IDeletableEntityRepository<Message> messagesRepository)
        {
            this.messagesRepository = messagesRepository;
        }

        public IEnumerable<T> AllMessages<T>()
        {
            IQueryable<Message> query = this.messagesRepository.All().OrderBy(x => x.CreatedOn);
            return query.To<T>().ToList();
        }

        public int CreateMessage(int id, string sentBy, string email, string phoneNumber, string messageContent)
        {
            var vehicle = this.vehicleService.GetVehicle(id);
            var message = new Message
            {
                SendBy = sentBy,
                Email = email,
                PhoneNumber = phoneNumber,
                MessageContent = messageContent,
            };

            vehicle.Messages.Add(message);
            this.messagesRepository.SaveChangesAsync();
            this.vehicleRepository.SaveChangesAsync();
            return vehicle.Id;
        }

        public Task<Message> GetMessageAsync(int id)
        {
            var message = this.messagesRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            return message;
        }

        //public void RemoveMessage(int id)
        //{
        //    var message = this.messagesRepository.All().FirstOrDefault(x => x.Id == id);
        //    this.messagesRepository.HardDelete(message);
        //    this.messagesRepository.SaveChangesAsync();
        //}
    }
}
