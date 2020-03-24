﻿namespace GeletoCarDealer.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using GeletoCarDealer.Data.Models;
    using GeletoCarDealer.Data.Models.Models;
    using Microsoft.AspNetCore.Http;

    public interface IImageService
    {
        Task UploadImageAsync(Vehicle vehicle, IList<IFormFile> files);

        Task<IEnumerable<Image>> GetImagesAsync(int id);

    }
}
