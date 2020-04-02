﻿namespace GeletoCarDealer.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IMessageService
    {
        int CreateMessage(int vehicleId, string sentBy, string email, string phoneNumber, string messageContent);

        IEnumerable<T> AllMessages<T>();
    }
}
