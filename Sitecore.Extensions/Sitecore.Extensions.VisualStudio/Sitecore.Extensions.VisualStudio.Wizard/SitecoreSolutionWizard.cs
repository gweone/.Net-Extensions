using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.TemplateWizard;
using Sitecore.Extensions.VisualStudio.Wizard.Component;
using Sitecore.Extensions.VisualStudio.Wizard.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sitecore.Extensions.VisualStudio.Wizard
{
    public class SitecoreSolutionWizard : SitecoreProjectWizard
    {
        int smtpUCIndex = 1;
        public override void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            base.RunStarted(automationObject, replacementsDictionary, runKind, customParams);
        }

        protected override TemplateWizardFrom GenerateWizardUI(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            var form = base.GenerateWizardUI(automationObject, replacementsDictionary, runKind, customParams);
            smtpUCIndex = form.CreateNewPage("Sitecore SMTP", "Mailing from Sitecore", new SitecoreSmtpUC() { Name = "smtpUC" });
            return form;
        }

        protected override void Wizard_OnFinished(TemplateWizardFrom wizard, Dictionary<string, string> replacementsDictionary, EventArgs args)
        {
            base.Wizard_OnFinished(wizard, replacementsDictionary, args);
            var control = wizard.GetPage(1).Controls.Find("smtpUC", false).FirstOrDefault() as SitecoreSmtpUC;
            sitecoreData.SmtpMail = control.Data;

            replacementsDictionary.Add("$sc_mail_host$", sitecoreData.SmtpMail.Host);
            replacementsDictionary.Add("$sc_mail_port$", sitecoreData.SmtpMail.Port);
            replacementsDictionary.Add("$sc_mail_ssl$", sitecoreData.SmtpMail.Ssl.ToString().ToLowerInvariant());
            replacementsDictionary.Add("$sc_mail_username$", sitecoreData.SmtpMail.Username);
            replacementsDictionary.Add("$sc_mail_password$", sitecoreData.SmtpMail.Password);
        }

    }
}
