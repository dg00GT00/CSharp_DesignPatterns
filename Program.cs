using System;
using System.Collections.Generic;

namespace Console_BookExamples
{
    //************* Proxy principle *************//
    // A class that function as an interface to a particular resource.
    // That resource may be remote, expensive to construct, or may require logging
    // or some other added functionality
    public struct Percentage : IEquatable<Percentage>
    {
        private readonly float _value;

        internal Percentage(float value)
        {
            _value = value;
        }

        public static float operator *(float f, Percentage p)
        {
            return f * p._value;
        }

        public static Percentage operator +(Percentage a, Percentage b)
        {
            return new Percentage(a._value + b._value);
        }

        public override string ToString()
        {
            return $"{_value * 100}%";
        }

        public bool Equals(Percentage other)
        {
            return _value.Equals(other._value);
        }

        public override bool Equals(object obj)
        {
            return obj is Percentage other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static bool operator ==(Percentage left, Percentage right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Percentage left, Percentage right)
        {
            return !left.Equals(right);
        }
    }

    public static class PercentageExtensions
    {
        public static Percentage Percent(this int value)
        {
            return new Percentage(value / 100.00f);
        }

        public static Percentage Percent(this float value)
        {
            return new Percentage(value / 100.00f);
        }
    }

    internal static class Demo
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(10f * 5.Percent());
            Console.WriteLine(2.Percent() + 3.Percent());
        }
    }
}