using MetroFramework.Controls;
using Sitecore.Extensions.VisualStudio.Wizard.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sitecore.Extensions.VisualStudio.Wizard.Component
{
    public partial class SitecoreSmtpUC : MetroUserControl
    {
        public SitecoreSmtpUC()
        {
            InitializeComponent();
        }

        public SitecoreData.Smtp Data
        {
            get
            {
                var data = new SitecoreData.Smtp()
                {
                    Host = txtHost.Text,
                    Port = txtPort.Text,
                    Ssl = cbSsl.Checked,
                    Username = txtUsername.Text,
                    Password = txtPassword.Text
                };
                return data;
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    client.Host = txtHost.Text;
                    client.Port = Int32.Parse(txtPort.Text);
                    client.EnableSsl = cbSsl.Checked;
                    client.Credentials = new NetworkCredential(txtUsername.Text, txtPassword.Text);
                    client.Send("sample@mail.com", "destination@mail.com", "sample of smtp testing", "Your succesfully setup smtp configuration");
                }

                MessageBox.Show("Mail smtp succesfully configured", "Horay!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
