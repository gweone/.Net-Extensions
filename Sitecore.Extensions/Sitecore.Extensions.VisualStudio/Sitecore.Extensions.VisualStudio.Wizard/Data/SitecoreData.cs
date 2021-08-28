using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Extensions.VisualStudio.Wizard.Data
{
    public class SitecoreData
    {
        public Configuration Config { get; set; }
        public Smtp SmtpMail { get; set; }


        public class Configuration
        {
            public string PackageSource { get; set; }
            public string Url { get; set; }
            public string Version { get; set; }
        }

        public class Smtp
        {
            public string Host { get; set; }
            public string Port { get; set; }
            public bool Ssl { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }

    
}
