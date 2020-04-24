using CloudinaryDotNet;
using GeletoCarDealer.Data;
using GeletoCarDealer.Data.Models;
using GeletoCarDealer.Data.Models.Models;
using GeletoCarDealer.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GeletoCarDealer.Services.Data.Tests
{
    public class ImagesServiceTests
    {
        //[Fact]
        //public async Task UploadImageAsyncShouldAddImagesToVehicle()
        //{
        //    var options = new DbContextOptionsBuilder<GeletoDbContext>()
        //        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        //        .Options;
        //    var context = new GeletoDbContext(options);
        //    var imageRepository = new EfDeletableEntityRepository<Image>(context);
        //    var vehicleRepository = new EfDeletableEntityRepository<Vehicle>(context);
        //    Account account = new Account(
        //                        "dtq8zwbyi",
        //                        "261259893366574",
        //                        "RhRIxDhZAE9p0lm2vrEB8_imPqU");
        //    var cloudinary = new Mock<Cloudinary>(account);
            //var imageService = new ImageService(imageRepository, vehicleRepository);

            //var vehicle = new Vehicle
            //{
            //    CategoryId = 1,
            //    Make = "Audi",
            //    Model = "A4",
            //    HorsePower = 200,
            //    Price = 12000,
            //    Description = "asdasd",
            //    Milage = 123456,
            //    TransmissionType = "Automatic",
            //    FuelType = "Diesel",
            //    Year = 2005,
            //};
            //await vehicleRepository.AddAsync(vehicle);
            //await vehicleRepository.SaveChangesAsync();
            //IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("dummy image")), 0, 0, "Data", "image.png");
            //var files = new List<IFormFile>();
            //files.Add(file);
            //var currentVehicle = vehicleRepository.All().Where(x => x.Id == 1).FirstOrDefault();
            //await imageService.UploadImageAsync(currentVehicle,files);
            //var images = await imageRepository.All().ToListAsync();

            //Assert.Equal(2, images.Count);
        
    }
}
