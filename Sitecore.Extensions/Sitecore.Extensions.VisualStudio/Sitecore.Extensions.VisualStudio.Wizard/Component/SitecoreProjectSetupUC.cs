using MetroFramework.Controls;
using NuGet.Common;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;
using Sitecore.Extensions.VisualStudio.Wizard.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sitecore.Extensions.VisualStudio.Wizard.Component
{
    public partial class SitecoreProjectSetupUC : MetroUserControl
    {
        public SitecoreProjectSetupUC()
        {
            InitializeComponent();
            BindSitecoreVersion();
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

        public SitecoreData Data
        {
            get
            {
                var data = new SitecoreData() {
                    Url = txtSiteCoreUrl.Text,
                    Version = cboSitecoreVersion.Text,
                    PackageSource = txtNugetV3.Text
                };
                return data;
            }
        }

    }
}
