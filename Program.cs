using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Console_BookExamples
{
    //************* Flyweight principle *************//
    // A space optimization technique that lets use use less memory by storing externally the data
    // associate with similar objects
    public class FormattedTex
    {
        private readonly string _plainText;
        private bool[] _capitalize;

        public FormattedTex(string plainText)
        {
            _plainText = plainText;
            _capitalize = new bool[plainText.Length];
        }

        public void Capitalize(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                _capitalize[i] = true;}
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < _plainText.Length; i++)
            {
                var c = _plainText[i];
                sb.Append(_capitalize[i] ? char.ToUpper(c) : c);
            }

            return sb.ToString();
        }
    }

    public class BetterFormattedText
    {
        private string _plainText;
        private List<TextRange> _formatting = new List<TextRange>();

        public BetterFormattedText(string plainText)
        {
            _plainText = plainText;
        }

        public class TextRange
        {
            public int Start, End;
            public bool Capitalize, Bold, Italic;

            public bool Covers(int position)
            {
                return position >= Start && position <= End;
            }
        }

        public TextRange GetRange(int start, int end)
        {
            var range = new TextRange {Start = start, End = end};
            _formatting.Add(range);
            return range;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < _plainText.Length; i++)
            {
                var c = _plainText[i];
                c = _formatting
                    .Where(range => range.Covers(i) && range.Capitalize)
                    .Aggregate(c, (current, range) => char.ToUpper(current));

                sb.Append(c);
            }

            return sb.ToString();
        }
    }

    internal static class Demo
    {
        private static void Main(string[] args)
        {
            var ft = new FormattedTex("This is a brave new world");
            ft.Capitalize(10, 15);
            Console.WriteLine(ft);

            var bft = new BetterFormattedText("This is a brave new world");
            bft.GetRange(10, 15).Capitalize = true;
            Console.WriteLine(bft.ToString());
        }
    }
}