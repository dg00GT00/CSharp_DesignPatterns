using System;

//using MoreLinq;

namespace Console_BookExamples
{
    //************* Singleton principle *************//
    // Making a "safe" singleton is easy:
    // construct a static Lazy<T> and return its Value
    // Singletons are difficult to test
    // Instead of directly using a singleton, consider depending on an abstraction (e.g. an interface)
    // Consider defining singleton lifetime in DI container
    internal class CEO
    {
        private static string _name;
        private static int _age;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public int Age
        {
            get => _age;
            set => _age = value;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}";
        }
    }
    

    internal static class Demo
    {
        private static void Main(string[] args)
        {
            var ceo = new CEO {Name = "Adam Smith", Age = 55};

            var ceo2 = new CEO();
            Console.WriteLine(ceo2);
        }
    }
}