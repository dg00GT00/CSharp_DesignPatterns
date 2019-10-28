using System;
using System.Collections.Generic;

namespace Console_BookExamples
{
    //************* Proxy principle *************//
    // A class that function as an interface to a particular resource.
    // That resource may be remote, expensive to construct, or may require logging
    // or some other added functionality

    public class Property<T> : IEquatable<Property<T>> where T : new()
    {
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                if (Equals(_value, value))
                {
                    return;
                }

                Console.WriteLine($"Assigning value to {value}");
                _value = value;
            }
        }

        public static implicit operator T(Property<T> property)
        {
            return property._value; // int n = Property<int> variable;
        }

        public static implicit operator Property<T>(T value)
        {
            return new Property<T>(value); // Property<int> p = 123;
        }

        public Property() : this(Activator.CreateInstance<T>())
        {
        }

        public Property(T value)
        {
            _value = value;
        }

        public bool Equals(Property<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<T>.Default.Equals(_value, other._value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Property<T>) obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(_value);
        }

        public static bool operator ==(Property<T> left, Property<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Property<T> left, Property<T> right)
        {
            return !Equals(left, right);
        }
    }

    public class Create
    {
        private Property<int> _agility = new Property<int>();

        public Property<int> Agility
        {
            get => _agility.Value;
            set => _agility.Value = value;
        }
    }

    internal static class Demo
    {
        private static void Main(string[] args)
        {
            var c = new Create();
            c.Agility = 10;
            c.Agility = 10;
        }
    }
}