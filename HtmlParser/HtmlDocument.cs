using HtmlParser.Enums;
using HtmlParser.Extensions;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace HtmlParser
{
    public class HtmlDocument
    {
        HtmlElement _documentNode = new();
        public HtmlElement DocumentNode  => _documentNode;

        public HtmlDocument() { }
        public HtmlDocument(string html)
        {
            LoadHtml(html);
        }

        public void LoadHtml(string html)
        {
            if (string.IsNullOrEmpty(html))
                throw new Exception("html is empty");

            var tokens = Tokenize(html.AsSpan());
            _documentNode.ChildNodes = Parse(tokens);
        }

        private List<string> Tokenize(ReadOnlySpan<char> htmlSpan)
        {
            List<string> tokens = new();
            while(htmlSpan.Length - 1 > 0)
            {
                htmlSpan = htmlSpan.Trim();
                var c = htmlSpan[0];
                if (c == '<')
                {
                    int tagEndIndex = htmlSpan.IndexOf('>');
                    var token = htmlSpan[1..(tagEndIndex)];
                    tokens.Add($"<{token.ToString()}>");
                    htmlSpan = htmlSpan[(tagEndIndex + 1)..];
                }

                else
                {
                    int tagOpenIndex = htmlSpan.IndexOf('<');
                    tokens.Add(htmlSpan[0..tagOpenIndex].ToString());
                    htmlSpan = htmlSpan[tagOpenIndex..];
                }
            }

            return tokens;
        }

        private List<HtmlElement> Parse(List<string> tokens)
        {
            List<HtmlElement> elements = new();
            List<string> openedTags = new();
            HtmlElement parent = null;

            foreach (var token in tokens)
            {
                HtmlElement element = new()
                {
                    ElementType = token.GetElementType(),
                    ParentNode = parent,
                };

                if (element.ElementType == ElementType.Text)
                {
                    element.Name = "text";
                    element.Value = token;
                }
                
                else if (element.ElementType == ElementType.Element)
                {
                    string tagName = token.GetTagName();
                    TagType tagType = token.GetTagType(tagName);

                    if (tagType == TagType.Opening)
                    {
                        element.Name = tagName;
                        element.ChildNodes = new List<HtmlElement>();
                        element.Attributes = token.GetAttributes(tagName);
                        bool isSelfClosing = ElementExtension.selfClosingTags.Exists(e => e == element.Name);
                        
                        if(!isSelfClosing)
                        {
                            parent = element;
                            openedTags.Add(tagName);
                        }
                    }
                    else
                    {
                        int index = openedTags.LastIndexOf(tagName);

                        if (index != -1)
                        {
                            if (parent.Name == tagName)
                            {
                                parent = parent.ParentNode;
                            }
                            else
                            {
                                HtmlElement parentEl = GetParentTag(tagName, parent.ParentNode);

                                if (parent != null && parent.ParentNode != null)
                                {
                                    parent = parent.ParentNode;
                                }
                            }

                            openedTags.RemoveAt(index);
                        }

                        continue;
                    }
                }

                if (element.ParentNode == null)
                {
                    elements.Add(element);
                }
                else
                {
                    element.ParentNode.ChildNodes.Add(element);
                }
            }

            return elements;
        }

        public HtmlElement GetParentTag(string tagName, HtmlElement htmlElement)
        {
            if (htmlElement != null)
            {
                if (htmlElement.Name == tagName)
                {
                    return htmlElement;
                }
                else
                {
                    return GetParentTag(tagName, htmlElement.ParentNode);
                }
            }

            return null;
        }
    }
}