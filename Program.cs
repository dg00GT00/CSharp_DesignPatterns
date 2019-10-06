using System;

namespace Console_BookExamples
{
    //************* Prototype principle *************//
    // A partially or fully initialized object that you copy (clone) and make use of. 
    internal class Person
    {
        public string[] Names;
        public Address Address;

        public Person(string[] names, Address address)
        {
            Names = names;
            Address = address;
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }
        // C++ copy constructor
        public Person(Person other)
        {
            Names = other.Names;
            Address = new Address(other.Address);
        }
    }

    internal class Address
    {
        public string StreetName;
        public int HouseNumber;

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }

        // C++ copy constructor
        public Address(Address other)
        {
            StreetName = other.StreetName;
            HouseNumber = other.HouseNumber;
        }
    }

    internal static class Demo
    {
        private static void Main(string[] args)
        {
            var john = new Person(new[] {"John", "Smith"}, new Address("London Road", 123));
            var jane = new Person(john);
            jane.Address.HouseNumber = 321;

            Console.WriteLine(john);
            Console.WriteLine(jane);
        }
    }
}