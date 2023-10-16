using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HtmlParser.Extensions
{
    public static class HtmlDocumentExtension
    {
        public static HtmlElement FirstElementByTagname(this HtmlElement element, string name)
        {
            if (element.Name == name)
                return element;

            foreach (var child in element.ChildNodes)
            {
                var found = FirstElementByTagname(child, name);
                if (found != null)
                    return found;
            }

            return null;
        }

        public static string? GetValue(this HtmlElement htmlElement) =>
            htmlElement.ChildNodes.FirstOrDefault(x => x.Name == "text")?.Value?.Replace("&nbsp;", " ");
    }
}
