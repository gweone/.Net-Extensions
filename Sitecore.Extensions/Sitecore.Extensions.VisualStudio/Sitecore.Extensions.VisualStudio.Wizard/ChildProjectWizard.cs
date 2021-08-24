using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Extensions.VisualStudio.Wizard
{
    public class ChildProjectWizard : NuGetTemplateWizard
    {
        public override void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            foreach (var key in SitecoreProjectWizard.GlobalDictionary.Keys)
            {
                if (!replacementsDictionary.Keys.Contains(key))
                {
                    string value = SitecoreProjectWizard.GlobalDictionary[key];
                    replacementsDictionary.Add(key, value);
                }
            }
            var projectPath = replacementsDictionary["$safeprojectname$"];
            if (projectPath.Split('.').Length > 1)
                projectPath = string.Join("\\", projectPath.Split('.').Take(2).Reverse());
            replacementsDictionary["$projectpathformat$"] = projectPath;

            base.RunStarted(automationObject, replacementsDictionary, runKind, customParams);
        }
    }
}
