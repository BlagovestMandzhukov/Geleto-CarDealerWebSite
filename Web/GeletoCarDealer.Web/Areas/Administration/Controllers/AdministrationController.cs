namespace GeletoCarDealer.Web.Areas.Administration.Controllers
{
    using GeletoCarDealer.Common;
    using GeletoCarDealer.Services.Data;
    using GeletoCarDealer.Web.Controllers;
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
            return this.View();
        }

      

        public IActionResult Edit()
        {
            return this.View();
        }
    }
}
