using HtmlParser.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HtmlParser.Extensions
{
    internal static class ElementExtension
    {
        public static List<string> selfClosingTags = new List<string>() {
            "area", "base", "br", "col", "command", "embed", "hr", "img", "input",
             "link", "menuitem", "meta", "param", "source", "track", "wbr"
        };

        public static string GetTagName(this string token)
        {
            var span = token.AsSpan();
            bool isClosedTag = span.IndexOf("<\\") != -1;
            int start = isClosedTag ? span.IndexOf("<\\") : span.IndexOf('<');
            bool hasAttribute = span.IndexOf(' ') != -1;
            int end = hasAttribute ? span.IndexOf(' ') : span.IndexOf('>');
            return span.Slice(start + 1, end - 1).Trim().ToString();
        }

        public static TagType GetTagType(this string token, string tagName)
        {
            if (selfClosingTags.Contains(tagName))
                return TagType.SelfClosed;

            else if (token.StartsWith("</"))
                return TagType.Closing;

            else if (token.StartsWith("<"))
                return TagType.Opening;

            else
                return TagType.Text;
        }

        public static ElementType GetElementType(this string token)
        {
            if (token.StartsWith("<!--") || token.EndsWith("-->"))
                return ElementType.Comment;
            else if (token.StartsWith("<!"))
                return ElementType.Document;
            else if (token.StartsWith('<') && token.EndsWith('>'))
                return ElementType.Element;
            else
                return ElementType.Text;
        }

        public static List<HtmlElement> GetAttributes(this string token, string tagName)
        {
            List<HtmlElement> attributes = new();
            HtmlElement attribute = new HtmlElement()
            {
                ElementType = ElementType.Attribute
            };

            var attrs = token.Split(" ").Where(x => x.Contains('=')).Select(x => x);
            foreach (var attr in attrs)
            {
                var splitAttr = attr.Split('=');
                attribute.Name = splitAttr[0];
                attribute.Value = splitAttr[1];

                attributes.Add(attribute);
            }

            return attributes;
        }
    }
}
