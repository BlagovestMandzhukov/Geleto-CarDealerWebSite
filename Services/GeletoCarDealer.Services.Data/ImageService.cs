namespace GeletoCarDealer.Services.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using GeletoCarDealer.Data.Common.Repositories;
    using GeletoCarDealer.Data.Models;
    using GeletoCarDealer.Data.Models.Models;
    using GeletoCarDealer.Services.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public class ImageService : IImageService
    {
        private readonly IDeletableEntityRepository<Image> imageRepository;
        private readonly IDeletableEntityRepository<Vehicle> vehiclesRepository;
        private readonly Cloudinary cloudinary;

        public ImageService(IDeletableEntityRepository<Image> imageRepository, IDeletableEntityRepository<Vehicle> vehiclesRepository, Cloudinary cloudinary)
        {
            this.imageRepository = imageRepository;
            this.vehiclesRepository = vehiclesRepository;
            this.cloudinary = cloudinary;
        }

        public async Task UploadImageAsync(Vehicle vehicle, IList<IFormFile> files)
        {
            var list = new List<string>();

            foreach (var file in files)
            {
                byte[] imasgeDestination;

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    imasgeDestination = memoryStream.ToArray();
                }

                using (var destinationStream = new MemoryStream(imasgeDestination))
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, destinationStream),
                    };

                    var res = await this.cloudinary.UploadAsync(uploadParams);
                    list.Add(res.Uri.AbsoluteUri);

                }

                foreach (var url in list)
                {
                    var image = new Image
                    {
                        ImageUrl = url,
                    };

                    vehicle.Images.Add(image);
                }
            }
        }

        public async Task<IEnumerable<Image>> GetImagesAsync(int id)
        {
            var vehicle = await this.vehiclesRepository.All().Include(x => x.Images).FirstOrDefaultAsync(x => x.Id == id);

            if (vehicle == null)
            {
                return null;
            }

            return vehicle.Images.ToList();
        }

        public IEnumerable<T> GetAllImages<T>(int id)
        {
            IQueryable<Image> getImagesQuery = this.imageRepository.All().Where(x => x.VehicleId == id);

            return getImagesQuery.To<T>().ToList();
        }
    }
}
