using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sitecore.Extensions.VisualStudio.Wizard.Components
{
    public partial class TemplateWizardFrom : MetroForm
    {
        public TemplateWizardFrom()
        {
            InitializeComponent();
        }

        public enum WizardStep
        {
            Next = 0,
            Back,
            Cancel
        }

        public class StepChangedEventArgs : EventArgs
        {
            public StepChangedEventArgs(WizardStep step, int page)
            {
                Step = step;
                Page = page;
                IsValid = true;
            }

            public WizardStep Step { get; protected set; }
            public int Page { get; protected set; }
            public bool IsValid { get; set; }

        }

        public delegate void StepChanged(object sender, StepChangedEventArgs args);
        public delegate void Finished(object sender, EventArgs args);

        public event StepChanged OnStepChanged;
        public event Finished OnFinished;

        public int CreateNewPage(string title, string description)
        {
            var panel = new AdvancedWizardControl.WizardPages.AdvancedWizardPage()
            {
                Name = Guid.NewGuid().ToString(),
                Dock = DockStyle.Fill,
                Header = false,
                HeaderImageVisible = true,
                HeaderTitle = title,
                SubTitle = description,
                Margin = new Padding(0, 150, 0, 0),
                
            };
            wizardPanel.Controls.Add(panel);
            var index = wizardPanel.WizardPages.Add(panel);
            wizardPanel.NextButtonEnabled = wizardPanel.WizardPages.Count > 1;
            return index;
        }

        public int CreateNewPage(string title, string description, params Control[] controls)
        {
            var index = CreateNewPage(title, description);
            wizardPanel.WizardPages[index].Controls.AddRange(controls);
            return index;
        }

        public Panel GetPage(int page)
        {
            return wizardPanel.WizardPages[page];
        }

        public int CurrentPage
        {
            get
            {
                return wizardPanel.WizardPages.IndexOf(wizardPanel.CurrentPage);
            }
        }

        private void wizardPanel_Back(object sender, EventArgs e)
        {
            if (OnStepChanged != null)
                OnStepChanged(this, new StepChangedEventArgs(WizardStep.Back, wizardPanel.WizardPages.IndexOf(wizardPanel.CurrentPage)));
        }

        private void wizardPanel_Cancel(object sender, EventArgs e)
        {
            var args = new StepChangedEventArgs(WizardStep.Cancel, wizardPanel.WizardPages.IndexOf(wizardPanel.CurrentPage));
            args.IsValid = true;
            if (OnStepChanged != null)
                OnStepChanged(this, args);
            if (args.IsValid)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }            
        }

        private void wizardPanel_Finish(object sender, EventArgs e)
        {
            if (OnFinished != null)
                OnFinished(this, e);
            this.Close();
        }

        private void wizardPanel_Next(object sender, AdvancedWizardControl.EventArguments.WizardEventArgs e)
        {
            if (OnStepChanged != null)
                OnStepChanged(this, new StepChangedEventArgs(WizardStep.Next, e.NextPageIndex));
        }
    }
}
