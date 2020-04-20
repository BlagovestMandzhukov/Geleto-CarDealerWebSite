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

        public IEnumerable<Message> GetAll()
        {
            return this.messagesRepository.All();
        }

        public Message CreateMessage(int id, string sentBy, string email, string phoneNumber, string messageContent)
        {
            var message = new Message
            {
                SendBy = sentBy,
                Email = email,
                PhoneNumber = phoneNumber,
                MessageContent = messageContent,
                VehicleId = id,
            };
            this.messagesRepository.AddAsync(message);
            this.messagesRepository.SaveChangesAsync();
            return message;
        }

        public async Task CreateContactsMessage(string sentBy, string email, string phoneNumber, string messageContent)
        {
            var message = new Message
            {
                SendBy = sentBy,
                Email = email,
                PhoneNumber = phoneNumber,
                MessageContent = messageContent,
            };
            await this.messagesRepository.AddAsync(message);
            await this.messagesRepository.SaveChangesAsync();
        }

        public Task<Message> GetMessageAsync(int id)
        {
            var message = this.messagesRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            return message;
        }

        public async Task RemoveMessageAsync(int id)
        {
            var message = this.messagesRepository.All().FirstOrDefault(x => x.Id == id);
            this.messagesRepository.Delete(message);
            await this.messagesRepository.SaveChangesAsync();
        }
    }
}
