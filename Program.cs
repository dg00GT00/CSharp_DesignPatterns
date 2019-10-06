using System;

namespace Console_BookExamples
{
    //************* Prototype principle *************//
    // A partially or fully initialized object that you copy (clone) and make use of. 
    internal class Person : ICloneable
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

        public object Clone()
        {
            return new Person(Names, (Address) Address.Clone());
        }
    }

    internal class Address : ICloneable
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

        public object Clone()
        {
            return new Address(StreetName, HouseNumber);
        }
    }

    internal static class Demo
    {
        private static void Main(string[] args)
        {
            var john = new Person(new[] {"John", "Smith"}, new Address("London Road", 123));
            var jane = (Person) john.Clone();
            jane.Address.HouseNumber = 321;

            Console.WriteLine(john);
            Console.WriteLine(jane);
        }
    }
}