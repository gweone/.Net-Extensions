using MetroFramework.Controls;
using NuGet.Common;
using NuGet.Packaging;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;
using Sitecore.Extensions.VisualStudio.Wizard.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Sitecore.Extensions.VisualStudio.Wizard.Component
{
    public partial class SitecoreProjectSetupUC : MetroUserControl
    {
        public SitecoreProjectSetupUC() : this(AppDomain.CurrentDomain.BaseDirectory)
        {
        }

        public SitecoreProjectSetupUC(string solutionPath)
        {
            SolutionPath = solutionPath;
            InitializeComponent();
            BindPackageSource();
            BindSitecoreVersion();
        }

        public string SolutionPath { get; set; }

        void BindPackageSource()
        {
            var nugetPath = Path.Combine(SolutionPath, "nuget.config");
            if (!File.Exists(nugetPath))
                return;
            var nuget = XDocument.Load(nugetPath);
            var sitecorePackage = nuget.Root
                .ElementsNoNamespace("packageSources")
                .ElementsNoNamespace("add")
                .FirstOrDefault(x => x.Attribute("key").Value == "Sitecore");

            if (sitecorePackage == null)
                return;
            txtNugetV3.Text = sitecorePackage.Attribute("value").Value;

        }

        async void BindSitecoreVersion()
        {
            ILogger logger = NullLogger.Instance;
            CancellationToken cancellationToken = CancellationToken.None;
            SourceCacheContext cache = new SourceCacheContext();
            SourceRepository repository = Repository.Factory.GetCoreV3(txtNugetV3.Text);
            FindPackageByIdResource resource = await repository.GetResourceAsync<FindPackageByIdResource>();

            IEnumerable<NuGetVersion> sitecoreVersion = await resource.GetAllVersionsAsync(
            "Sitecore.Mvc",
            cache,
            logger,
            cancellationToken);

            cboSitecoreVersion.DataSource = new BindingSource(sitecoreVersion
                .OrderByDescending(x => x.Version.Major)
                .ThenByDescending(x => x.Version.Minor)
                .ThenByDescending(x => x.Version.Build)
                .ThenByDescending(x => x.Version.Revision).ToList(), "");
            cboSitecoreVersion.ValueMember = cboSitecoreVersion.DisplayMember = "OriginalVersion";
        }

        public SitecoreData.Configuration Data
        {
            get
            {
                var data = new SitecoreData.Configuration()
                {
                    Url = txtSiteCoreUrl.Text,
                    Version = cboSitecoreVersion.Text,
                    PackageSource = txtNugetV3.Text,
                    TargetFramework = cbFrameworkOverride.Checked ? txtFramework.Text : ""
                };
                return data;
            }
        }

        private async void cboSitecoreVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            ILogger logger = NullLogger.Instance;
            CancellationToken cancellationToken = CancellationToken.None;
            SourceCacheContext cache = new SourceCacheContext();
            SourceRepository repository = Repository.Factory.GetCoreV3(txtNugetV3.Text);
            FindPackageByIdResource resource = await repository.GetResourceAsync<FindPackageByIdResource>();

            var info = await resource.GetDependencyInfoAsync("Sitecore.Mvc", cboSitecoreVersion.SelectedItem as NuGetVersion,
                cache, logger, cancellationToken);
            foreach (var item in info.FrameworkReferenceGroups.Where(x => x.TargetFramework.Framework == ".NETFramework"))
            { 
                var v = item.TargetFramework.Version;
                if(v.Build > 0)
                    txtFramework.Text = string.Format("{0}.{1}.{2}", v.Major, v.Minor, v.Build);                
                else
                    txtFramework.Text = string.Format("{0}.{1}", v.Major, v.Minor);
            }
        }
    }
}
