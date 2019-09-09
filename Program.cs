using System;

namespace Console_BookExamples
{
    //************* Single responsibility principle *************//
    // A typical class is responsible for only one specific operation 
    internal class Person
    {
        public string Name;
        public string Position;

        public class Builder : PersonJobBuilder<Builder>
        {
            
        }
        
        public static Builder New => new Builder();

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }
    }

    internal abstract class PersonBuilder
    {
        protected Person person = new Person();

        public Person Build()
        {
            return person;
        }
    }

    internal class PersonInfoBuilder<TSelf> : PersonBuilder where TSelf : PersonInfoBuilder<TSelf>
    {
        public TSelf Called(string name)
        {
            person.Name = name;
            return (TSelf) this;
        }
    }

    internal class PersonJobBuilder<TSelf> : PersonInfoBuilder<PersonJobBuilder<TSelf>>
        where TSelf : PersonJobBuilder<TSelf>
    {
        public TSelf WorksAs(string position)
        {
            person.Position = position;
            return (TSelf) this;
        }
    }

    internal static class Demo
    {
        private static void Main(string[] args)
        {
            var me = Person.New.Called("Gledson").WorksAs("Developer").Build();
            Console.WriteLine(me);
        }
    }
}