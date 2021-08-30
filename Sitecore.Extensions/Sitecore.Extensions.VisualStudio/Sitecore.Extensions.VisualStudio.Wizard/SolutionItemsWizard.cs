using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.TemplateWizard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sitecore.Extensions.VisualStudio.Wizard
{
    public class SolutionItemsWizard : IWizard
    {
        protected EnvDTE80.DTE2 DTE;
        protected string directorySolution;
        protected Dictionary<string, string> CurrentDictionary =
                new Dictionary<string, string>();
        protected string VSTemplatePath;
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
            AddSolutionItems();
        }

        public virtual void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            DTE = automationObject as EnvDTE80.DTE2;
            CurrentDictionary = replacementsDictionary;
            directorySolution = replacementsDictionary["$solutiondirectory$"];
            if (customParams.Length > 0)
                VSTemplatePath = customParams[0] as string;
        }

        public virtual bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }

        protected virtual void AddSolutionItems()
        {
            var solution = DTE.Solution as EnvDTE80.Solution2;
            var root = Path.GetDirectoryName(VSTemplatePath);
            var vstemplate = XDocument.Load(Path.Combine(root, "items", "solutionitems.vstemplate"));
            var _folders = vstemplate.Root
                .ElementsNoNamespace("TemplateContent")
                .ElementsNoNamespace("Project")
                .ElementsNoNamespace("Folder");

            foreach (var item in Directory.GetDirectories(Path.Combine(root, "items")))
            {
                var dirName = Path.GetFileName(item);
                var project = solution.AddSolutionFolder(dirName);
                SolutionFolder solFolder = (SolutionFolder)project.Object;
                AddSolutionItems(project, solFolder, _folders.FirstOrDefault(x => x.Attribute("Name").Value == dirName), directorySolution, item);
            }
        }

        protected virtual void AddSolutionItems(Project solutionFolderProject, SolutionFolder solFolder, XElement folderConfig, string localPath, string remotePath)
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

        protected virtual string ReplaceParameter(XElement folderConfig, string fileCopy, string file, string baseDirectory)
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
                        foreach (var item in CurrentDictionary)
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
