using System;

namespace Console_BookExamples
{
    //************* Faceted Builder principle *************//
    
    internal class Person
    {
        public string StreetAddress, PostCode, City;
        public string CompanyName, Position;
        public int AnnualIncome;

        public override string ToString()
        {
            return
                $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(PostCode)}: {PostCode}, {nameof(City)}: {City}, " +
                $"{nameof(CompanyName)}: {CompanyName}, {nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
        }
    }

    // façade
    internal class PersonBuilder
    {
        protected Person Person = new Person();
        public PersonJobBuilder Works => new PersonJobBuilder(Person);
        public PersonAddressBuilder Lives => new PersonAddressBuilder(Person);

        public static implicit operator Person(PersonBuilder pb)
        {
            return pb.Person;
        }
    }

    internal class PersonJobBuilder : PersonBuilder
    {
        public PersonJobBuilder(Person person)
        {
            Person = person;
        }

        public PersonJobBuilder At(string companyName)
        {
            Person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder AsA(string position)
        {
            Person.Position = position;
            return this;
        }

        public PersonJobBuilder Earning(int amount)
        {
            Person.AnnualIncome = amount;
            return this;
        }
    }

    internal class PersonAddressBuilder : PersonBuilder
    {
        public PersonAddressBuilder(Person person)
        {
            Person = person;
        }

        public PersonAddressBuilder At(string streetAddress)
        {
            Person.StreetAddress = streetAddress;
            return this;
        }

        public PersonAddressBuilder WithPostcode(string postcode)
        {
            Person.PostCode = postcode;
            return this;
        }

        public PersonAddressBuilder In(string city)
        {
            Person.City = city;
            return this;
        }
    }

    internal static class Demo
    {
        private static void Main(string[] args)
        {
            var pb = new PersonBuilder();
            Person person = pb
                .Works.At("Fabrikam").AsA("Engineer").Earning(123000)
                .Lives.At("123 LondonStreet").WithPostcode("345678").In("London");
            Console.WriteLine(person);
        }
    }
}