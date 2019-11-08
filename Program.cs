using System;
using System.Collections.Generic;
using System.Text;

namespace Console_BookExamples
{
    //******************* Strategy responsibility principle *******************//
    // Enables the exact behaviour of a system to be selected either at run-time
    // (dynamic) or compile-time (static)
    // Also known as 'policy' (esp. in the C++ world)
    public enum OutputFormat
    {
        MarkDown,
        Html
    }

    public interface IListStrategy
    {
        void Start(StringBuilder sb);
        void End(StringBuilder sb);
        void AddListItem(StringBuilder sb, string item);
    }

    public class HtmlListStrategy : IListStrategy
    {
        public void Start(StringBuilder sb)
        {
            sb.AppendLine("<ul>");
        }

        public void End(StringBuilder sb)
        {
            sb.AppendLine("</ul>");
        }

        public void AddListItem(StringBuilder sb, string item)
        {
            sb.AppendLine($"    <li>{item}</li>");
        }
    }

    public class MarkdownListStrategy : IListStrategy
    {
        public void Start(StringBuilder sb)
        {
        }

        public void End(StringBuilder sb)
        {
        }

        public void AddListItem(StringBuilder sb, string item)
        {
            sb.AppendLine($" * {item}");
        }
    }

    public class TextProcessor<TLs> where TLs : IListStrategy, new()
    {
        private StringBuilder _sb = new StringBuilder();
        private TLs _listStrategy = new TLs();


        public void AppendList(IEnumerable<string> items)
        {
            _listStrategy.Start(_sb);
            foreach (var item in items)
            {
                _listStrategy.AddListItem(_sb, item);
            }

            _listStrategy.End(_sb);
        }

        public StringBuilder Clear()
        {
            return _sb.Clear();
        }

        public override string ToString()
        {
            return _sb.ToString();
        }
    }

    internal static class Demo
    {
        private static void Main(string[] args)
        {
            var tp = new TextProcessor<MarkdownListStrategy>();
            tp.AppendList(new []{"foo", "bar", "baz"});
            Console.WriteLine(tp);
            
            var tp2 = new TextProcessor<HtmlListStrategy>();
            tp2.AppendList(new []{"foo", "bar", "baz"});
            Console.WriteLine(tp2);
        }
    }
}