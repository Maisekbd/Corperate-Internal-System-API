using AAAID.Common;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentsExplorer.API.App_Start
{
    public class ApplicationContext : IApplicationContext
    {
        public string GetUserId()
        {
            if (HttpContext.Current.User != null)
                return HttpContext.Current.User.Identity.GetUserId();
            return "";

        }
    }
}