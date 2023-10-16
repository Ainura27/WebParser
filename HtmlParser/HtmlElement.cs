using HtmlParser.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HtmlParser
{
    public class HtmlElement
    {
        public ElementType ElementType;
        public string Name;
        public string Value;
        public List<HtmlElement> ChildNodes = new();
        public List<HtmlElement> Attributes = new();
        public HtmlElement ParentNode;
    }
}
