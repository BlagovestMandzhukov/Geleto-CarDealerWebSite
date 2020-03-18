namespace GeletoCarDealer.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using GeletoCarDealer.Common;
    using GeletoCarDealer.Data.Common.Repositories;
    using GeletoCarDealer.Data.Models.Enums;
    using GeletoCarDealer.Data.Models.Models;
    using GeletoCarDealer.Services.Data;
    using GeletoCarDealer.Web.Controllers;
    using GeletoCarDealer.Web.ViewModels.Administration.Vehicles;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    //[Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
        private readonly Data.Common.Repositories.IDeletableEntityRepository<Image> repository;

        public AdministrationController(IDeletableEntityRepository<Image> repository)
        {
            this.repository = repository;
        }

        [Route("/[controller]/Admin")]
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Create()
        {
            var model = new CreateInputModel();
            model.Specifications = new List<SpecificationsViewModel>();

            foreach (Specifications spec in Enum.GetValues(typeof(Specifications)))
            {
                model.Specifications.Add(new SpecificationsViewModel { Specification = spec, IsSelected = false });
            }

            return this.View(model);
        }

        [HttpGet]
        public IActionResult UploadImage()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IList<IFormFile> images)
        {
            foreach (var file in this.Request.Form.Files)
            {
                Image img = new Image();
                img.Title = file.FileName;

                MemoryStream ms = new MemoryStream();
                await file.CopyToAsync(ms);
                img.ImageData = ms.ToArray();

                ms.Close();
                await ms.DisposeAsync();

                await this.repository.AddAsync(img);
                await this.repository.SaveChangesAsync();
            }

            return this.Redirect("/Home/GetAll");
        }

        public IActionResult Edit()
        {
            return this.View();
        }
    }
}
