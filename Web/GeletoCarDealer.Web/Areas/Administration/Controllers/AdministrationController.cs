namespace GeletoCarDealer.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GeletoCarDealer.Common;
    using GeletoCarDealer.Data.Models.Enums;
    using GeletoCarDealer.Services.Data;
    using GeletoCarDealer.Web.Controllers;
    using GeletoCarDealer.Web.ViewModels.Administration.Vehicles;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    //[Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {

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

        public IActionResult Edit()
        {
            return this.View();
        }
    }
}
