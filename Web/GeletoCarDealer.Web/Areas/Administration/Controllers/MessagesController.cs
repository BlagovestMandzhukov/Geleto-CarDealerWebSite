namespace GeletoCarDealer.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GeletoCarDealer.Services.Data;
    using GeletoCarDealer.Services.Messaging;
    using GeletoCarDealer.Web.ViewModels.Administration.Messages;
    using Microsoft.AspNetCore.Mvc;

    public class MessagesController : AdministrationController
    {
        private readonly IMessageService messageService;
        private readonly IEmailSender sender;

        public MessagesController(IMessageService messageService, IEmailSender sender)
        {
            this.messageService = messageService;
            this.sender = sender;
        }
        public IActionResult MyMessages()
        {
            var viewModel = new AllMessagesViewModel
            {
                Messages = this.messageService.AllMessages<MessagesViewModel>(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(CreateMessageInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("MyMessages", model);
            }

            await this.sender.SendEmailAsync(model.SendToEmail, model.Subject, model.Content);
            return this.RedirectToAction("MyMessages");
        }

        public async Task<IActionResult> RemoveMessage(int id)
        {
            await this.messageService.RemoveMessageAsync(id);
            var viewModel = new AllMessagesViewModel
            {
                Messages = this.messageService.AllMessages<MessagesViewModel>(),
            };
            return this.View("MyMessages", viewModel);
        }
    }
}
