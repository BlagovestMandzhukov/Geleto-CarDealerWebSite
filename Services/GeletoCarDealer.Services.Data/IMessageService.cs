namespace GeletoCarDealer.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using GeletoCarDealer.Data.Models.Models;

    public interface IMessageService
    {
        Message CreateMessage(int id, string sentBy, string email, string phoneNumber, string messageContent);

        IEnumerable<T> AllMessages<T>();

        Task<Message> GetMessageAsync(int id);

        Task RemoveMessageAsync(int id);
    }
}
