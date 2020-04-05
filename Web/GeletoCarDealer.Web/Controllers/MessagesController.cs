namespace GeletoCarDealer.Web.Controllers
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
        private readonly IVehicleService vehicleService;

        public MessagesController(IMessageService messageService, IVehicleService vehicleService)
        {
            this.messageService = messageService;
            this.vehicleService = vehicleService;
        }

        [HttpPost]
        public IActionResult SendMessage(int id, MessageInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("ById", "Vehicles", new { _id = id });
            }

            var vehicleId = this.vehicleService.AddMessageToVehicle(
                 id,
                 input.SendBy,
                 input.Email,
                 input.PhoneNumber,
                 input.MessageContent);

            this.TempData["MessageSent"] = "Вашето запитване беше изпратено успешно!";
            return this.RedirectToAction("ById", "Vehicles", new { id = vehicleId });
        }
    }
}
