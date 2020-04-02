﻿namespace GeletoCarDealer.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using GeletoCarDealer.Services.Data;
    using GeletoCarDealer.Web.ViewModels.UsersArea.Messages;
    using Microsoft.AspNetCore.Mvc;

    public class MessagesController : BaseController
    {
        private readonly IMessageService messageService;

        public MessagesController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpPost]
        public IActionResult SendMessage(int id, MessageInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("ById", input);
            }

            this.messageService.CreateMessage(
                 id,
                 input.SendBy,
                 input.Email,
                 input.PhoneNumber,
                 input.MessageContent);

            return this.RedirectToAction("ById", new { _id = id });
        }
    }
}
