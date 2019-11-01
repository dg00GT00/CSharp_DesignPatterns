using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Console_BookExamples
{
    //********************** Iterator principle **********************//
    // An object (or, in .NET, a method) that facilitates the traversal
    // of a data structure.
    // An iterator specified how you can traverse an object
    // An iterator object, unlike a method cannot be recursive
    // Generally, an IEnumerable<T>  returning method is enough
    // Iteration works through duck typing - you need a GetEnumerator() that yields
    // a type that has Current and MoveNext()

    public class Creature: IEnumerable<int>
    {
        private int[] _stats = new int[3];

        private const int Strengh = 0;

        public int Strength
        {
            get => _stats[Strengh];
            set => _stats[Strengh] = value;
        }

        public int Agility { get; set; }
        public int Intelligence { get; set; }

        public double AverageStat => _stats.Average();
        public IEnumerator<int> GetEnumerator()
        {
            return _stats.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    internal static class Demo
    {
        private static void Main(string[] args)
        {
        }
    }
}