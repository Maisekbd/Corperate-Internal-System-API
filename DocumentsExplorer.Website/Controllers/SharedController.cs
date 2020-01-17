using DocumentsExplorer.BLL.Interface;
using DocumentsExplorer.Model.Models;
using DocumentsExplorer.Website.Models;
using IdentityManagement.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocumentsExplorer.Website.Controllers
{
    public class SharedController : Controller
    {
        public readonly ICouncilTypeBLL _councilTypeBLL;

        public SharedController() { }
        public SharedController(ICouncilTypeBLL councilTypeBLL)
        {
            _councilTypeBLL = councilTypeBLL;
           
        }
        /// <summary>
        /// Application DB context
        /// </summary>
        protected ApplicationDbContext ApplicationDbContext { get; set; }

        /// <summary>
        /// User manager - attached to application DB context
        /// </summary>
        //protected UserManager<ApplicationUser> UserManager { get; set; }

        public ApplicationUser CurrentUser { get {
                this.ApplicationDbContext = new ApplicationDbContext();
                //this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
                return System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            } }

       

        #region Language 
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {


            string lang = null;
            HttpCookie langCookie = Request.Cookies["culture"];
            if (langCookie != null)
            {
                lang = langCookie.Value;
            }
            else
            {
                var userLanguage = Request.UserLanguages;
                var userLang = userLanguage != null ? userLanguage[0] : "";
                if (userLang != "")
                {
                    lang = userLang;
                }
                else
                {
                    lang = LanguageHelper.GetDefaultLanguage();
                }
            }
            new LanguageHelper().SetLanguage(lang);
            return base.BeginExecuteCore(callback, state);
        }

        public ActionResult ChangeLanguage(string lang)
        {
            new LanguageHelper().SetLanguage(lang);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SideBar()
        {
            List<CouncilType> councilTypes = _councilTypeBLL.GetCouncilTypes().ToList();
            return PartialView("_SideBar", councilTypes);
        }
        #endregion

    }
}