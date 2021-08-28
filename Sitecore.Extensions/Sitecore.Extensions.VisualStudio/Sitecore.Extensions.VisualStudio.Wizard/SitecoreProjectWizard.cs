using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using Sitecore.Extensions.VisualStudio.Wizard.Component;
using Sitecore.Extensions.VisualStudio.Wizard.Components;
using Sitecore.Extensions.VisualStudio.Wizard.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sitecore.Extensions.VisualStudio.Wizard.Components.TemplateWizardFrom;

namespace Sitecore.Extensions.VisualStudio.Wizard
{
    public class SitecoreProjectWizard : NuGetTemplateWizard
    {
        public static Dictionary<string, string> GlobalDictionary =
               new Dictionary<string, string>();
        protected SitecoreData sitecoreData = new SitecoreData();
        int setupUCIndex = 0;
        public override void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            if (automationObject is DTE)
            {
                TemplateWizardFrom wizard = GenerateWizardUI(automationObject, replacementsDictionary, runKind, customParams);
                wizard.ShowDialog();
            }
            PrepareStarting(automationObject, replacementsDictionary, runKind, customParams);
            base.RunStarted(automationObject, replacementsDictionary, runKind, customParams);
            GlobalDictionary = replacementsDictionary;
        }

        protected virtual void PrepareStarting(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            var solutionPath = replacementsDictionary["$safeprojectname$"];
            var projectPath = solutionPath;
            if (projectPath.Split('.').Length > 1)
            {
                projectPath = string.Join("\\", projectPath.Split('.').Take(2));
                solutionPath = projectPath.Split('.').FirstOrDefault();
            }                
            replacementsDictionary["$projectpathformat$"] = projectPath;
            replacementsDictionary["$solutionpathformat$"] = solutionPath;
        }

        protected virtual TemplateWizardFrom GenerateWizardUI(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            TemplateWizardFrom wizard = new TemplateWizardFrom();
            wizard.Text = "Sitecore Project MVC Setup";
            setupUCIndex = wizard.CreateNewPage("Sitecore", "Option to Setup Sitecore", new SitecoreProjectSetupUC()
            {
                Name = "setupUC"
            });
            wizard.OnStepChanged += (s, args) => Wizard_OnStepChanged(s as TemplateWizardFrom, replacementsDictionary, args);
            wizard.OnFinished += (s, args) =>  Wizard_OnFinished(s as TemplateWizardFrom, replacementsDictionary, args);
            return wizard;
        }

        protected virtual void Wizard_OnFinished(TemplateWizardFrom wizard, Dictionary<string, string> replacementsDictionary, EventArgs args)
        {
            var control = wizard.GetPage(setupUCIndex).Controls.Find("setupUC", false).FirstOrDefault() as SitecoreProjectSetupUC;
            sitecoreData.Config = control.Data;
            replacementsDictionary.Add("$sc_url$", sitecoreData.Config.Url);
            replacementsDictionary.Add("$sc_version$", sitecoreData.Config.Version);
            replacementsDictionary.Add("$sc_package$", sitecoreData.Config.PackageSource);
            
        }

        protected virtual void Wizard_OnStepChanged(TemplateWizardFrom wizard, Dictionary<string, string> replacementsDictionary, StepChangedEventArgs args)
        {
            switch (args.Step)
            {
                case WizardStep.Next:
                case WizardStep.Back:
                    break;
                case WizardStep.Cancel:
                    args.IsValid = false;
                    break;
                default:
                    break;
            }
        }

        protected override string ResolvePackageVersion(string packageid, string version)
        {
            if (packageid.StartsWith("Sitecore") && sitecoreData != null)
                return sitecoreData.Config.Version;
            return base.ResolvePackageVersion(packageid, version);
        }
    }
}
