using GeletoCarDealer.Data;
using GeletoCarDealer.Data.Models.Models;
using GeletoCarDealer.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GeletoCarDealer.Services.Data.Tests
{
    public class MessagesServiceTests
    {
        [Fact]
        public void GetAllShouldReturnAll()
        {
            var options = new DbContextOptionsBuilder<GeletoDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new GeletoDbContext(options);
            var messageRepository = new EfDeletableEntityRepository<Message>(context);

            var messageService = new MessageService(messageRepository);
            var message = new Message
            {
                Email = "asd@asd.asd",
                PhoneNumber = "0854325512",
                SendBy = "asd",
                MessageContent = "asdkhasdkjhagsjdkhgasda",
            };
            messageRepository.AddAsync(message);
            messageRepository.SaveChangesAsync();
            var messages = messageService.GetAll();
            Assert.Single(messages);
        }

        [Fact]
        public void CreateMessageShouldCreateMessage()
        {
            var options = new DbContextOptionsBuilder<GeletoDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new GeletoDbContext(options);
            var messageRepository = new EfDeletableEntityRepository<Message>(context);

            var messageService = new MessageService(messageRepository);

            var message = messageService.CreateMessage(1, "asd", "asd@asd.asd", "0854325512", "asdkhasdkjhagsjdkhgasda");
            var actualMessagesCount = messageService.GetAll().Count();
            Assert.Equal(1, actualMessagesCount);
        }

        [Fact]
        public void CreateContactsMessageShouldCreateMessage()
        {
            var options = new DbContextOptionsBuilder<GeletoDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new GeletoDbContext(options);
            var messageRepository = new EfDeletableEntityRepository<Message>(context);

            var messageService = new MessageService(messageRepository);

            var message = messageService.CreateContactsMessage(
               "asd", "asd@asd.asd", "0854325512", "asdkhasdkjhagsjdkhgasda");
            var actualMessagesCount = messageService.GetAll().Count();
            Assert.Equal(1, actualMessagesCount);
        }
        [Fact]
        public async Task GetMessageAsyncShouldReturnMessage()
        {
            var options = new DbContextOptionsBuilder<GeletoDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new GeletoDbContext(options);
            var messageRepository = new EfDeletableEntityRepository<Message>(context);

            var messageService = new MessageService(messageRepository);

            var message = messageService.CreateMessage(1, "asd", "asd@asd.asd", "0854325512", "asdkhasdkjhagsjdkhgasda");
            var actualMessageResult = await messageService.GetMessageAsync(1);

            Assert.Equal(message, actualMessageResult);
        }

        [Fact]
        public async Task GetMessageAsyncShouldReturnNullIfMessageRepositoryIsEmpty()
        {
            var options = new DbContextOptionsBuilder<GeletoDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new GeletoDbContext(options);
            var messageRepository = new EfDeletableEntityRepository<Message>(context);

            var messageService = new MessageService(messageRepository);

            var actualMessageResult = await messageService.GetMessageAsync(1);

            Assert.Null(actualMessageResult);
        }

        [Fact]
        public async Task RemoveMessageAsyncShouldRemoveMEssagesFromRepository()
        {

            var options = new DbContextOptionsBuilder<GeletoDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new GeletoDbContext(options);
            var messageRepository = new EfDeletableEntityRepository<Message>(context);

            var messageService = new MessageService(messageRepository);

            var testMessage1 = messageService.CreateMessage(1, "asd", "asd@asd.asd", "0854325512", "asdkhasdkjhagsjdkhgasda");
            var testMessage2 = messageService.CreateMessage(2, "asds", "asd@asd.asds", "0854325612", "asdkhasdkjhagsjdkhgassda");
            var testMessage3 = messageService.CreateMessage(3, "assds", "assd@asd.asds", "0855325612", "asdkhasdkjhagssjdkhgassda");

            await messageService.RemoveMessageAsync(1);
            var actualCount = messageService.GetAll().Count();
            Assert.Equal(2, actualCount);
        }
    }
}