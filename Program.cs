using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Console_BookExamples
{
    //************* Prototype principle *************//
    // A partially or fully initialized object that you copy (clone) and make use of. 
    // To implement a prototype, partially construct an object and store it somewhere
    // Clone the prototype:
    ////// Implement your own deep copy functionality; or serialize and deserialize
    // Customize the resulting instance
    public static class ExtensionMethods
    {
        public static T DeepCopy<T>(this T self)
        {
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, self);
            stream.Seek(0, SeekOrigin.Begin);
            object copy = formatter.Deserialize(stream);
            stream.Close();
            return (T) copy;
        }

        public static T DeepCopyXml<T>(this T self)
        {
            using (var ms = new MemoryStream())
            {
                var s = new XmlSerializer(typeof(T));
                s.Serialize(ms, self);
                ms.Position = 0;
                return (T) s.Deserialize(ms);
            }
        }
    }

//    [Serializable] // Used when requesting the DeepCopy() extension
    public class Person

    {
        public string[] Names;
        public Address Address;

        public Person(string[] names, Address address)
        {
            Names = names;
            Address = address;
        }

        public Person()
        {
            // Required to the DeepCopyXml extension method
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }
    }

//    [Serializable] // Used when requesting the DeepCopy() extension
    public class Address

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

        public Address()
        {
            // Required to the DeepCopyXml extension method
        }
    }

    internal static class Demo
    {
        private static void Main(string[] args)
        {
            var john = new Person(new[] {"John", "Smith"}, new Address("London Road", 123));
      
            var jane = john.DeepCopyXml();
            jane.Names[0] = "Jane";
            jane.Address.HouseNumber = 234;

            Console.WriteLine(john);
            Console.WriteLine(jane);
        }
    }
}