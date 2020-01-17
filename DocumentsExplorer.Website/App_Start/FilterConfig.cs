using System.Web;
using System.Web.Mvc;

namespace DocumentsExplorer.Website
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //I just added this line below.
            filters.Add(new AuthorizeAttribute());
        }
    }
}
