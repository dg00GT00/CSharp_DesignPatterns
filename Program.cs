using System;
using System.Collections.Generic;

namespace Console_BookExamples
{
    //******************* Strategy responsibility principle *******************//
    // Enables the exact behaviour of a system to be selected either at run-time
    // (dynamic) or compile-time (static)
    // Also known as 'policy' (esp. in the C++ world)
    // Define an algorithm at high-level
    // Define the interface you expect each strategy to follow
    // Provide for either dynamic or static composition of strategy in the overall algorithm

    public class Person : IComparable<Person>, IComparable
    {
        public int Id;
        public string Name;
        public int Age;

        public Person(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }

        public int CompareTo(Person other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Id.CompareTo(other.Id);
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            return obj is Person other
                ? CompareTo(other)
                : throw new ArgumentException($"Object must be of type {nameof(Person)}");
        }

        public static bool operator <(Person left, Person right)
        {
            return Comparer<Person>.Default.Compare(left, right) < 0;
        }

        public static bool operator >(Person left, Person right)
        {
            return Comparer<Person>.Default.Compare(left, right) > 0;
        }

        public static bool operator <=(Person left, Person right)
        {
            return Comparer<Person>.Default.Compare(left, right) <= 0;
        }

        public static bool operator >=(Person left, Person right)
        {
            return Comparer<Person>.Default.Compare(left, right) >= 0;
        }

        private sealed class NameRelationalComparer : IComparer<Person>
        {
            public int Compare(Person x, Person y)
            {
                if (ReferenceEquals(x, y)) return 0;
                if (ReferenceEquals(null, y)) return 1;
                if (ReferenceEquals(null, x)) return -1;
                return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
            }
        }

        public static IComparer<Person> NameComparer { get; } = new NameRelationalComparer();
    }

    internal static class Demo
    {
        private static void Main(string[] args)
        {
            var people = new List<Person>();
            people.Sort();
            people.Sort(Person.NameComparer);
        }
    }
}