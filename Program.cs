using System;
using System.Collections.Generic;
using System.Text;

namespace Console_BookExamples
{
    //****************** Fluent builder principle ******************//
    
    internal class HtmlElement
    {
        public string TagName, Text;
        public List<HtmlElement> Elements = new List<HtmlElement>();
        private const int IndentSize = 2;

        public HtmlElement()
        {
        }

        public HtmlElement(string tagName, string text)
        {
            TagName = tagName;
            Text = text;
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }

        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', IndentSize * indent);
            sb.AppendLine($"{i}<{TagName}>");

            if (!string.IsNullOrWhiteSpace(Text))
            {
                sb.Append(new string(' ', IndentSize * (indent + 1)));
                sb.AppendLine(Text);
            }

            foreach (var e in Elements)
            {
                sb.Append(e.ToStringImpl(indent + 1));
            }

            sb.AppendLine($"{i}</{TagName}>");
            return sb.ToString();
        }
    }

    public class HtmlBuilder
    {
        private HtmlElement _root = new HtmlElement();
        private readonly string _rootTagName;

        public HtmlBuilder(string rootTagName)
        {
            _rootTagName = rootTagName;
            _root.TagName = rootTagName;
        }

        public override string ToString()
        {
            return _root.ToString();
        }

        public HtmlBuilder AddChild(string childTagName, string childText)
        {
            var e = new HtmlElement(childTagName, childText);
            _root.Elements.Add(e);
            return this;
        }

        public void Clear()
        {
            _root = new HtmlElement {TagName = _rootTagName};
        }
    }


    internal static class Demo
    {
        private static void Main(string[] args)
        {
//            var hello = "hello";
//            var sb = new StringBuilder();
//            sb.Append("<p>");
//            sb.Append(hello);
//            sb.Append("</p>");
//            Console.WriteLine(sb);
//
//            var words = new[] {"hello", "world"};
//            sb.Clear();
//            sb.Append("<ul>");
//            foreach (var word in words)
//            {
//                sb.AppendFormat("<li>{0}</li>", word);
//            }
//
//            sb.Append("</ul>");
//            Console.WriteLine(sb);
            var builder = new HtmlBuilder("ul");
            builder.AddChild("li", "hello").AddChild("li", "world");
            Console.WriteLine(builder);
        }
    }
}