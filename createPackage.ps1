# Powershell script to create nuget package

# Create pp file
copy .\UmbracoProjectTemplate.Web\Helpers\HtmlHelpers.cs .\HtmlHelpers.cs.pp
(Get-Content .\HtmlHelpers.cs.pp).replace('namespace UmbracoProjectTemplate.Web', 'namespace $rootnamespace$') | Set-Content .\HtmlHelpers.cs.pp

# Create package
nuget pack Creuna.UmbracoBasePackage.nuspec

# Delete pp file
del .\HtmlHelpers.cs.pp