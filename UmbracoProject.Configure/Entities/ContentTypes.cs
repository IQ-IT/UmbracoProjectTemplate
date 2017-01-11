using System;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using UmbracoProject.Configure.Models;

namespace UmbracoProject.Configure.Entities
{
    public class ContentTypes
    {
        public static ContentTypesCreationModel Create(TemplatesCreationModel templates, ServiceContext context)
        {
            var compositionsFolder = context.ContentTypeService.CreateContentTypeContainer(-1, "Compositions");
            var pageContentTypeFolder = context.ContentTypeService.CreateContentTypeContainer(-1, "Pages");

            var createAttempt = context.ContentTypeService.CreateContentTypeContainer(-1, "Containers");
            //if(createAttempt.Success)
            //{
            //    var f = createAttempt.Result.Entity;
            //    var root = context.ContentTypeService.
            //    var siteContentType = new ContentType()
            //    {
            //        Alias = "Website",
            //        AllowedAsRoot = true,
            //        //AllowedContentTypes = 
            //        Description = "Container for a website with content root, and shared global content",
            //        Icon = "icon-globe",
            //        Name = "Website",
            //    };
            //}
            throw new NotImplementedException();
        }
    }
}