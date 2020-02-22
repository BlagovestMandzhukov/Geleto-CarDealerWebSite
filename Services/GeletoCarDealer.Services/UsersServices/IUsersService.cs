namespace GeletoCarDealer.Services.UsersServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IUsersService
    {
        string GetUsername(string username, string password);
    }
}
