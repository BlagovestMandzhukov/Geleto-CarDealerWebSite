namespace GeletoCarDealer.Web.Areas.Administration.Controllers
{
    using GeletoCarDealer.Common;
    using GeletoCarDealer.Web.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    //[Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
