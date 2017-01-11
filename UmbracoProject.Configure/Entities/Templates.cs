using System;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using UmbracoProject.Configure.Models;

namespace UmbracoProject.Configure.Entities
{
    public class Templates
    {
        public static TemplatesCreationModel Create(ServiceContext context)
        {
            var result = new TemplatesCreationModel();
            var layoutTemplate = new Template("Layout", "_Layout");
            context.FileService.SaveTemplate(layoutTemplate);

            var frontPageTemplate = new Template("Frontpage", "Frontpage");
            context.FileService.SaveTemplate(frontPageTemplate);
            frontPageTemplate.SetMasterTemplate(layoutTemplate);
            result.FrontpageTemplate = frontPageTemplate;

            var textPageTemplate = new Template("Textpage", "Textpage");
            context.FileService.SaveTemplate(textPageTemplate);
            frontPageTemplate.SetMasterTemplate(layoutTemplate);
            result.TextpageTemplate = textPageTemplate;

            return result;
        }
    }
}