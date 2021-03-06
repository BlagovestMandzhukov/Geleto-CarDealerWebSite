﻿namespace GeletoCarDealer.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        Task<int> CreateCategory(string name);

        int GetCarsId();

        int GetSuvId();

        int GetMotorcycleId();

        int GetBusId();
    }
}
