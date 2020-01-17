using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DocumentsExplorer.Website.Startup))]
namespace DocumentsExplorer.Website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            new LanguageHelper().SetLanguage("ar-AE");
        }
    }
}
