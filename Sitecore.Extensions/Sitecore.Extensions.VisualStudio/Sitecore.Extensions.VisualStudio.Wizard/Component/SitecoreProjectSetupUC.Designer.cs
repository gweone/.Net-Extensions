
namespace Sitecore.Extensions.VisualStudio.Wizard.Component
{
    partial class SitecoreProjectSetupUC
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblDataProvider = new MetroFramework.Controls.MetroLabel();
            this.txtSiteCoreUrl = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.cboSitecoreVersion = new MetroFramework.Controls.MetroComboBox();
            this.txtNugetV3 = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.txtFramework = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.cbFrameworkOverride = new MetroFramework.Controls.MetroCheckBox();
            this.SuspendLayout();
            // 
            // lblDataProvider
            // 
            this.lblDataProvider.AutoSize = true;
            this.lblDataProvider.Location = new System.Drawing.Point(12, 52);
            this.lblDataProvider.Name = "lblDataProvider";
            this.lblDataProvider.Size = new System.Drawing.Size(84, 19);
            this.lblDataProvider.TabIndex = 17;
            this.lblDataProvider.Text = "Sitecore Url :";
            // 
            // txtSiteCoreUrl
            // 
            this.txtSiteCoreUrl.Location = new System.Drawing.Point(168, 52);
            this.txtSiteCoreUrl.Name = "txtSiteCoreUrl";
            this.txtSiteCoreUrl.Size = new System.Drawing.Size(356, 23);
            this.txtSiteCoreUrl.TabIndex = 18;
            this.txtSiteCoreUrl.Text = "https://localhost";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(12, 90);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(109, 19);
            this.metroLabel1.TabIndex = 19;
            this.metroLabel1.Text = "Sitecore Version :";
            // 
            // cboSitecoreVersion
            // 
            this.cboSitecoreVersion.FormattingEnabled = true;
            this.cboSitecoreVersion.ItemHeight = 23;
            this.cboSitecoreVersion.Location = new System.Drawing.Point(168, 90);
            this.cboSitecoreVersion.Name = "cboSitecoreVersion";
            this.cboSitecoreVersion.Size = new System.Drawing.Size(356, 29);
            this.cboSitecoreVersion.TabIndex = 20;
            this.cboSitecoreVersion.SelectedIndexChanged += new System.EventHandler(this.cboSitecoreVersion_SelectedIndexChanged);
            // 
            // txtNugetV3
            // 
            this.txtNugetV3.Location = new System.Drawing.Point(168, 14);
            this.txtNugetV3.Name = "txtNugetV3";
            this.txtNugetV3.Size = new System.Drawing.Size(356, 23);
            this.txtNugetV3.TabIndex = 22;
            this.txtNugetV3.Text = "https://sitecore.myget.org/F/sc-packages/api/v3/index.json";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(12, 14);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(134, 19);
            this.metroLabel2.TabIndex = 21;
            this.metroLabel2.Text = "Sitecore Package V3 :";
            // 
            // txtFramework
            // 
            this.txtFramework.Location = new System.Drawing.Point(168, 134);
            this.txtFramework.Name = "txtFramework";
            this.txtFramework.Size = new System.Drawing.Size(282, 23);
            this.txtFramework.TabIndex = 24;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(12, 134);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(110, 19);
            this.metroLabel3.TabIndex = 23;
            this.metroLabel3.Text = ".Net Framework :";
            // 
            // cbFrameworkOverride
            // 
            this.cbFrameworkOverride.AutoSize = true;
            this.cbFrameworkOverride.Checked = true;
            this.cbFrameworkOverride.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFrameworkOverride.Location = new System.Drawing.Point(456, 142);
            this.cbFrameworkOverride.Name = "cbFrameworkOverride";
            this.cbFrameworkOverride.Size = new System.Drawing.Size(68, 15);
            this.cbFrameworkOverride.TabIndex = 25;
            this.cbFrameworkOverride.Text = "Override";
            this.cbFrameworkOverride.UseVisualStyleBackColor = true;
            // 
            // SitecoreProjectSetupUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbFrameworkOverride);
            this.Controls.Add(this.txtFramework);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.txtNugetV3);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.cboSitecoreVersion);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.txtSiteCoreUrl);
            this.Controls.Add(this.lblDataProvider);
            this.Name = "SitecoreProjectSetupUC";
            this.Size = new System.Drawing.Size(550, 179);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroLabel lblDataProvider;
        private MetroFramework.Controls.MetroTextBox txtSiteCoreUrl;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroComboBox cboSitecoreVersion;
        private MetroFramework.Controls.MetroTextBox txtNugetV3;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroTextBox txtFramework;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroCheckBox cbFrameworkOverride;
    }
}
