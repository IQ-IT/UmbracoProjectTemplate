using System.IO;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Logging;
using UmbracoProject.Configure.Entities;
using UmbracoProject.Configure.Models;

namespace UmbracoProject.Configure
{
    public class StartupHandler : IApplicationEventHandler
    {
        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            var createFile = new FileInfo(HttpContext.Current.Server.MapPath("~/App_Data/baseSetup.lck"));
            if (createFile.Exists) return;

            LogHelper.Info<StartupHandler>("Creating base setup");

            var templates = Templates.Create(applicationContext.Services);
            var contentTypes = ContentTypes.Create(templates, applicationContext.Services);
            
            using(var fs = File.CreateText(createFile.FullName))
            {
                fs.Write("Basesetup created");
                fs.Flush();
                fs.Close();
            }
        }

        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
        }

        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
        }
    }
}