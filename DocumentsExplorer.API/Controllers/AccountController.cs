using IdentityManagement.Model.Controller;
using IdentityManagement.Model.Managers;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DocumentsExplorer.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : BaseAccountAPIController
    {

        public AccountController()
        {
        }

        public AccountController(
            ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            //AccessTokenFormat = accessTokenFormat;
        }




        //[HttpGet]
        //[AllowAnonymous]
        //[Route("GetEmployee")]
        //public string GetEmployee(string userId)
        //{
        //    try
        //    {
        //        var s = _employeeService.GetByUserId("817f4880-90bf-40be-aeb4-2f7bb6dda32b");
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }


        //}
    }
}