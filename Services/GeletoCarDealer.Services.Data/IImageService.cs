namespace GeletoCarDealer.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using GeletoCarDealer.Data.Models.Models;
    using Microsoft.AspNetCore.Http;

    public interface IImageService
    {
        Task UploadImageAsync(int id, IList<IFormFile> files);

        Task<IEnumerable<Image>> GetImagesAsync(int id);
    }
}
