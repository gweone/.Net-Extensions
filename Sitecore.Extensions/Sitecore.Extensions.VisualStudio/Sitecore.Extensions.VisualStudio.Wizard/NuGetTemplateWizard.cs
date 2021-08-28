using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EnvDTE;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TemplateWizard;
using NuGet.VisualStudio;
namespace Sitecore.Extensions.VisualStudio.Wizard
{
    public class NuGetTemplateWizard : IWizard
    {
        protected string VSTemplatePath;
        protected const string PACKAGE_REFERENCE_KEY = "$packagereference$";
        protected const string METHOD_APPEND = "Append";
        public virtual void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            if (customParams.Length > 0)
                VSTemplatePath = customParams[0] as string;
            SetPackageNodes(GeneratePackageNodes(VSTemplatePath), replacementsDictionary);
        }

        protected virtual void SetPackageNodes(string package, Dictionary<string, string> replacementsDictionary)
        {
            var info = GetPackagesInfo(VSTemplatePath);
            if (info != null && info.Attribute("method") != null && info.Attribute("method").Value == METHOD_APPEND)
            {
                if (replacementsDictionary.ContainsKey(PACKAGE_REFERENCE_KEY))
                {
                    replacementsDictionary[PACKAGE_REFERENCE_KEY] += package;
                    return;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(package) || !replacementsDictionary.ContainsKey(PACKAGE_REFERENCE_KEY))
                    replacementsDictionary[PACKAGE_REFERENCE_KEY] = package;
            }
        }

        protected XElement GetPackagesInfo(string vsTemplatePath)
        {
            var vstemplate = XDocument.Load(vsTemplatePath);
            var _packages = vstemplate.Root
                .ElementsNoNamespace("WizardData")
                .ElementsNoNamespace("packages").FirstOrDefault();
            return _packages;
        }

        protected IEnumerable<XElement> GetPackages(string vsTemplatePath)
        {
            var vstemplate = XDocument.Load(vsTemplatePath);
            var _packages = vstemplate.Root
                .ElementsNoNamespace("WizardData")
                .ElementsNoNamespace("packages")
                .ElementsNoNamespace("package").ToList();
            return _packages;
        }

        protected virtual string GeneratePackageNodes(string vsTemplatePath)
        {
            StringBuilder packageGenerator = new StringBuilder();
            foreach (var package in GetPackages(vsTemplatePath))
            {
                packageGenerator.AppendLine("<PackageReference");
                packageGenerator.AppendFormat(" Include=\"{0}\"", package.Attribute("id").Value);

                var version = ResolvePackageVersion(package.Attribute("id").Value, package.Attribute("version")?.Value);
                if (!string.IsNullOrEmpty(version))
                    packageGenerator.AppendFormat(" Version=\"{0}\"", ResolvePackageVersion(package.Attribute("id").Value, version));

                var condition = package.Attribute("condition")?.Value;
                if (!string.IsNullOrEmpty(condition))
                    packageGenerator.AppendFormat(" Condition=\"{0}\"", condition);
                packageGenerator.Append(">").AppendLine();
                var privateassets = package.Attribute("privateassets")?.Value;
                if (!string.IsNullOrEmpty(privateassets))
                    packageGenerator.AppendFormat("<PrivateAssets>{0}</PrivateAssets>", privateassets).AppendLine();
                packageGenerator.Append("</PackageReference>");
            }
            return packageGenerator.ToString();
        }

        protected virtual string ResolvePackageVersion(string packageid, string version)
        {
            return version;
        }

        public virtual void BeforeOpeningFile(ProjectItem projectItem)
        {

        }

        public virtual void ProjectFinishedGenerating(Project project)
        {

        }

        public virtual void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {

        }

        public virtual void RunFinished()
        {

        }

        public virtual bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }
    }
}
