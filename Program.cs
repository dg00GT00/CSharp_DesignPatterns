using System;
using System.Collections.Generic;

namespace Console_BookExamples
{
    //************* Proxy principle *************//
    // A class that function as an interface to a particular resource.
    // That resource may be remote, expensive to construct, or may require logging
    // or some other added functionality
    
    // A proxy has the same interface as the underlying object
    // To create a proxy, simply replicate the existing interface of an object
    // Add relevant functionality to the redefined member functions
    // Different proxies (communication, logging, caching, etc) have completely different behaviours

    public class Creature
    {
        public byte Age;
        public int X, Y;
    }

    public class Creatures
    {
        private readonly int _size;
        private byte[] _age;
        private int[] _x, _y;

        public Creatures(int size)
        {
            _size = size;
            _age = new byte[size];
            _x = new int[size];
            _y = new int[size];
        }

        public struct CreatureProxy
        {
            private readonly Creatures _creatures;
            private readonly int _index;

            public CreatureProxy(Creatures creatures, int index)
            {
                _creatures = creatures;
                _index = index;
            }

            public ref byte Age => ref _creatures._age[_index];
            public ref int X => ref _creatures._x[_index];
            public ref int Y => ref _creatures._y[_index];
        }

        public IEnumerator<CreatureProxy> GetEnumerator()
        {
            for (int pos = 0; pos < _size; pos++)
            {
                yield return new CreatureProxy(this, pos);
            }
        }
    }

    internal static class Demo
    {
        private static void Main(string[] args)
        {
            var creatures = new Creatures(100);
            foreach (Creatures.CreatureProxy creature in creatures)
            {
                creature.X++;
            }
        }
    }
}