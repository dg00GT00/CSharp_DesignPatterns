using System;
using System.Collections.Generic;
using System.Linq;

namespace Console_BookExamples
{
    //************* Dependency inversion principle *************//
    // High-level modules should not depend upon low-level ones, use abstractions
    public enum RelationShip
    {
        Parent,
        Child,
        Sibling
    }

    public class Person
    {
        public string Name;
    }

    public interface IRelationShipBrowser
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }

    // low-level
    public class RelationShips : IRelationShipBrowser
    {
        private readonly List<(Person, RelationShip, Person)> _relations = new List<(Person, RelationShip, Person)>();

        public void AddParentAndChild(Person parent, Person child)
        {
            _relations.Add((parent, RelationShip.Parent, child));
            _relations.Add((child, RelationShip.Child, parent));
        }

//        public List<(Person, RelationShip, Person)> Relations => _relations;

        public override string ToString()
        {
            return $"{nameof(_relations)}: {_relations}";
        }

        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            return _relations.Where(x => x.Item1.Name == "John" && x.Item2 == RelationShip.Parent).Select(r => r.Item3);
        }
    }

    internal static class Demo
    {
//        private static void Research(RelationShips relationShips)
//        {
//            var relations = relationShips.Relations;
//            foreach (var r in relations.Where(x => x.Item1.Name == "John" && x.Item2 == RelationShip.Parent))
//            {
//                Console.WriteLine($"John has a child called {r.Item3.Name}");
//            }
//            
//        }
        private static void Research(IRelationShipBrowser browser)
        {
            foreach (var p in browser.FindAllChildrenOf("John"))
            {
                Console.WriteLine($"John has a child called {p.Name}");
            }
        }
            

        private static void Main(string[] args)
        {
            var parent = new Person {Name = "John"};
            var child1 = new Person {Name = "Chris"};
            var child2 = new Person {Name = "Mary"};

            var relationships = new RelationShips();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);

            Research(relationships);
        }
    }
}