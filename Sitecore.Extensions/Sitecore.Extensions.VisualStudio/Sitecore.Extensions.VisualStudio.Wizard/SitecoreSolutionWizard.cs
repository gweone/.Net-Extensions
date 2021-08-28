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
        EnvDTE80.DTE2 DTE;
        string directorySolution;
        int smtpUCIndex = 1;
        public override void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            base.RunStarted(automationObject, replacementsDictionary, runKind, customParams);
            DTE = automationObject as EnvDTE80.DTE2;
            directorySolution = replacementsDictionary["$solutiondirectory$"];
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

        public override void RunFinished()
        {
            AddSolutionItems();

            base.RunFinished();
        }

        private void AddSolutionItems()
        {
            var solution = DTE.Solution as EnvDTE80.Solution2;
            var root = Path.GetDirectoryName(VSTemplatePath);
            var vstemplate = XDocument.Load(Path.Combine(root, "content", "content.vstemplate"));
            var _folders = vstemplate.Root
                .ElementsNoNamespace("TemplateContent")
                .ElementsNoNamespace("Project")
                .ElementsNoNamespace("Folder");

            foreach (var item in Directory.GetDirectories(Path.Combine(root, "content")))
            {
                var dirName = Path.GetFileName(item);
                var project = solution.AddSolutionFolder(dirName);
                SolutionFolder solFolder = (SolutionFolder)project.Object;
                AddSolutionItems(project, solFolder, _folders.FirstOrDefault(x => x.Attribute("Name").Value == dirName), directorySolution, item);
            }
        }

        private void AddSolutionItems(Project solutionFolderProject, SolutionFolder solFolder, XElement folderConfig, string localPath, string remotePath)
        {
            var dirName = Path.GetFileName(remotePath);
            if (!dirName.StartsWith("."))
            {
                localPath = Path.Combine(localPath, dirName);
                if (!Directory.Exists(localPath))
                    Directory.CreateDirectory(localPath);
            }

            foreach (var file in Directory.GetFiles(remotePath))
            {
                var filename = Path.GetFileName(file);
                var fileCopy = file;
                if (dirName.StartsWith("."))
                {
                    fileCopy = ReplaceParameter(folderConfig, fileCopy, file, directorySolution);
                }
                else
                {
                    fileCopy = ReplaceParameter(folderConfig, fileCopy, file, localPath);
                }
                solutionFolderProject.ProjectItems.AddFromFile(fileCopy);
            }
            foreach (var item in Directory.GetDirectories(remotePath))
            {
                dirName = Path.GetFileName(item);
                var project = solFolder.AddSolutionFolder(dirName);
                var childsolFolder = (SolutionFolder)project.Object;
                AddSolutionItems(project, childsolFolder, folderConfig.ElementsNoNamespace("Folder").FirstOrDefault(x => x.Attribute("Name").Value == dirName), localPath, item);
            }
        }

        private string ReplaceParameter(XElement folderConfig, string fileCopy, string file, string baseDirectory)
        {
            var filename = Path.GetFileName(file);
            var fileConfig = folderConfig.Elements().FirstOrDefault(x => x.Value == filename);
            fileCopy = Path.Combine(baseDirectory, filename);
            if (fileConfig.Attribute("ReplaceParameters").Value == "true")
            {
                string fileContent = File.ReadAllText(file);
                using (var fs = new FileStream(fileCopy, FileMode.Create))
                {
                    using (var sw = new StreamWriter(fs))
                    {
                        foreach (var item in GlobalDictionary)
                            fileContent = fileContent.Replace(item.Key, item.Value);
                        sw.Write(fileContent);
                    }
                }
            }
            else
                File.Copy(file, fileCopy);
            return fileCopy;
        }
    }
}
