using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sitecore.Extensions.VisualStudio.Wizard
{
    public static class XDocumentExtensions
    {
        public static IEnumerable<XElement> ElementsNoNamespace(this XElement source, string localName)
        {
            return source.Elements().Where(e => e.Name.LocalName == localName);
        }

        public static IEnumerable<XElement> ElementsNoNamespace(this IEnumerable<XContainer> source, string localName)
        {
            return source.Elements().Where(e => e.Name.LocalName == localName);
        }
    }
}
