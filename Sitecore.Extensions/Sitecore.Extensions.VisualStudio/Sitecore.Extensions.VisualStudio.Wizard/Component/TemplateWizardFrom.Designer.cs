namespace Sitecore.Extensions.VisualStudio.Wizard.Components
{
    partial class TemplateWizardFrom
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplateWizardFrom));
            this.wizardPanel = new AdvancedWizardControl.Wizard.AdvancedWizard();
            this.wizardPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardPanel
            // 
            this.wizardPanel.BackButtonEnabled = false;
            this.wizardPanel.BackButtonText = "< Back";
            this.wizardPanel.ButtonLayout = AdvancedWizardControl.Enums.ButtonLayoutKind.Default;
            this.wizardPanel.ButtonsVisible = true;
            this.wizardPanel.CancelButtonText = "&Cancel";
            this.wizardPanel.CurrentPageIsFinishPage = false;
            this.wizardPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardPanel.FinishButton = true;
            this.wizardPanel.FinishButtonEnabled = true;
            this.wizardPanel.FinishButtonText = "&Finish";
            this.wizardPanel.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.wizardPanel.HelpButton = false;
            this.wizardPanel.HelpButtonText = "&Help";
            this.wizardPanel.Location = new System.Drawing.Point(20, 60);
            this.wizardPanel.Name = "wizardPanel";
            this.wizardPanel.NextButtonEnabled = false;
            this.wizardPanel.NextButtonText = "Next >";
            this.wizardPanel.ProcessKeys = false;
            this.wizardPanel.Size = new System.Drawing.Size(577, 377);
            this.wizardPanel.TabIndex = 0;
            this.wizardPanel.TouchScreen = false;
            this.wizardPanel.Cancel += new System.EventHandler(this.wizardPanel_Cancel);
            this.wizardPanel.Next += new System.EventHandler<AdvancedWizardControl.EventArguments.WizardEventArgs>(this.wizardPanel_Next);
            this.wizardPanel.Back += new System.EventHandler(this.wizardPanel_Back);
            this.wizardPanel.Finish += new System.EventHandler(this.wizardPanel_Finish);
            // 
            // ECSWizardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 457);
            this.Controls.Add(this.wizardPanel);
            this.Name = "ECSWizardForm";
            this.Text = "Wizard";
            this.wizardPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AdvancedWizardControl.Wizard.AdvancedWizard wizardPanel;
    }
}