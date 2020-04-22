using GeletoCarDealer.Data;
using GeletoCarDealer.Data.Models.Models;
using GeletoCarDealer.Data.Repositories;
using GeletoCarDealer.Services.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace GeletoCarDealer.Services.Data.Tests
{
    public class MessagesServiceTests
    {
        private readonly EfDeletableEntityRepository<Message> messageRepository;
        private readonly MessageService messageService;
        public MessagesServiceTests()
        {
            var options = new DbContextOptionsBuilder<GeletoDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;
            var context = new GeletoDbContext(options);
            this.messageRepository = new EfDeletableEntityRepository<Message>(context);
            this.messageService = new MessageService(messageRepository);
            this.InitializeMapping();
        }

        [Fact]
        public void AllMessagesShouldReturnAll()
        {
            var message = new Message
            {
                Email = "asd@asd.asd",
                PhoneNumber = "0854325512",
                SendBy = "asd",
                MessageContent = "asdkhasdkjhagsjdkhgasda",
            };
            this.messageRepository.AddAsync(message);
            this.messageRepository.SaveChangesAsync();
            this.InitializeMapping();
            var messages = this.messageService.AllMessages<TestMessageViewModel>();
            Assert.Single(messages);
        }

        [Fact]
        public void GetAllShouldReturnAll()
        {

            var message = new Message
            {
                Email = "asd@asd.asd",
                PhoneNumber = "0854325512",
                SendBy = "asd",
                MessageContent = "asdkhasdkjhagsjdkhgasda",
            };
            this.messageRepository.AddAsync(message);
            this.messageRepository.SaveChangesAsync();
            var messages = this.messageService.GetAll();
            Assert.Single(messages);
        }

        [Fact]
        public void CreateMessageShouldCreateMessage()
        {
            var message = messageService.CreateMessage(1, "asd", "asd@asd.asd", "0854325512", "asdkhasdkjhagsjdkhgasda");
            var actualMessagesCount = this.messageService.GetAll().Count();
            Assert.Equal(1, actualMessagesCount);
        }

        [Fact]
        public void CreateContactsMessageShouldCreateMessage()
        {
            var message = messageService.CreateContactsMessage(
               "asd", "asd@asd.asd", "0854325512", "asdkhasdkjhagsjdkhgasda");
            var actualMessagesCount = this.messageService.GetAll().Count();
            Assert.Equal(1, actualMessagesCount);
        }

        [Fact]
        public async Task GetMessageAsyncShouldReturnMessage()
        {
            var message = await messageService.CreateMessage(1, "asd", "asd@asd.asd", "0854325512", "asdkhasdkjhagsjdkhgasda");
            var actualMessageResult = await this.messageService.GetMessageAsync(1);

            Assert.Equal(message, actualMessageResult);
        }

        [Fact]
        public async Task GetMessageAsyncShouldReturnNullIfMessageRepositoryIsEmpty()
        {
            var actualMessageResult = await this.messageService.GetMessageAsync(1);

            Assert.Null(actualMessageResult);
        }

        [Fact]
        public async Task RemoveMessageAsyncShouldRemoveMEssagesFromRepository()
        {
            var testMessage1 = this.messageService.CreateMessage(1, "asd", "asd@asd.asd", "0854325512", "asdkhasdkjhagsjdkhgasda");
            var testMessage2 = this.messageService.CreateMessage(2, "asds", "asd@asd.asds", "0854325612", "asdkhasdkjhagsjdkhgassda");
            var testMessage3 = this.messageService.CreateMessage(3, "assds", "assd@asd.asds", "0855325612", "asdkhasdkjhagssjdkhgassda");

            await this.messageService.RemoveMessageAsync(1);
            var actualCount = this.messageService.GetAll().Count();
            Assert.Equal(2, actualCount);
        }
        private void InitializeMapping()
            => AutoMapperConfig.RegisterMappings(
                typeof(Message).GetTypeInfo().Assembly,
                typeof(TestMessageViewModel).GetTypeInfo().Assembly);
    }
    public class TestMessageViewModel : IMapFrom<Message>
    {
        public int Id { get; set; }

        public string SendBy { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string MessageContent { get; set; }

        public DateTime CreatedOn { get; set; }

    }
}
