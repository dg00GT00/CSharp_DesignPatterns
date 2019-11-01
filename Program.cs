using System;
using System.Collections.Generic;
using System.Text;

namespace Console_BookExamples
{
    //************************** Interpreter principle **************************//
    // A component that processes structured text data. Does so by turning in into separate
    // lexical tokens (lexing) and then interpreting sequences of said tokens (parsing)
    public class Token
    {
        public enum Type
        {
            Integer,
            Plus,
            Minus,
            LParen,
            RParen
        }

        public Type MyType;
        public string Text;

        public Token(Type myType, string text)
        {
            MyType = myType;
            Text = text;
        }

        public override string ToString()
        {
            return $"`{Text}`";
        }
    }

    internal static class Demo
    {
        static IEnumerable<Token> Lex(string input)
        {
            var result = new List<Token>();
            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case '+':
                        result.Add(new Token(Token.Type.Plus, "+"));
                        break;
                    case '-':
                        result.Add(new Token(Token.Type.Minus, "-"));
                        break;
                    case '(':
                        result.Add(new Token(Token.Type.LParen, "("));
                        break;
                    case ')':
                        result.Add(new Token(Token.Type.RParen, ")"));
                        break;
                    default:
                        var sb = new StringBuilder(input[i].ToString());
                        for (int j = i + 1; j < input.Length; j++)
                        {
                            if (char.IsDigit(input[j]))
                            {
                                sb.Append(input[j]);
                                ++i;
                            }
                            else
                            {
                                result.Add(new Token(Token.Type.Integer, sb.ToString()));
                                break;
                            }
                        }
                        break;
                }
            }

            return result;
        }

        private static void Main(string[] args)
        {
            string input = "(13 + 4) - (12 + 1)";
            var tokens = Lex(input);
            Console.WriteLine(string.Join("\t", tokens));
        }
    }
}