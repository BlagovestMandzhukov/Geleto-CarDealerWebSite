namespace GeletoCarDealer.Web.Areas.Administration.Controllers
{
    using GeletoCarDealer.Common;
    using GeletoCarDealer.Services.Data;
    using GeletoCarDealer.Web.Controllers;
    using GeletoCarDealer.Web.ViewModels.Administration.Vehicles;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    //[Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
        private readonly IVehicleService vehicleService;

        public AdministrationController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        [Route("/[controller]/Admin")]
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Create()
        {
            var makes = this.vehicleService.GetMakes<MakeViewModel>();
            return this.View(makes);
        }

        //[HttpPost]
        //public IActionResult Create()
        //{
        //    return this.View();
        //}

        public IActionResult Edit()
        {
            return this.View();
        }
    }
}
