using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autofac;
using MoreLinq.Extensions;
using NUnit.Framework;
//using MoreLinq;

namespace Console_BookExamples
{
    //************* Singleton principle *************//
    // A component which is instantiated only once 
    public interface IDatabase
    {
        int GetPopulation(string name);
    }

    public class SingleDatabase : IDatabase
    {
        private Dictionary<string, int> _capitals;
        private static int _instanceCount;

        public static int Count => _instanceCount;

        private SingleDatabase()
        {
            _instanceCount++;
            Console.WriteLine("Initalizing database");
            _capitals = File.ReadAllLines(@"C:\Users\dg_gt\RiderProjects\Console_BookExamples\capitals.txt")
                .Batch(2) // Comes from a external package "Nuget"
                .ToDictionary(capital => capital.ElementAt(0).Trim(),
                    population => int.Parse(population.ElementAt(1)));
        }

        public int GetPopulation(string name)
        {
            return _capitals[name];
        }

        private static Lazy<SingleDatabase> _instance = new Lazy<SingleDatabase>(() => new SingleDatabase());

        public static SingleDatabase Instance => _instance.Value;
    }

    public class SingleTonRecordFinder
    {
        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = names.Sum(name => SingleDatabase.Instance.GetPopulation(name));
            return result;
        }
    }

    public class ConfigurableRecordFinder
    {
        private IDatabase _database;

        public ConfigurableRecordFinder(IDatabase database)
        {
            _database = database;
        }

        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = names.Sum(name => _database.GetPopulation(name));
            return result;
        }
    }

    public class DummyDatabase : IDatabase
    {
        public int GetPopulation(string name)
        {
            return new Dictionary<string, int>()
            {
                ["alpha"] = 1,
                ["beta"] = 2,
                ["gamma"] = 3
            }[name];
        }
    }

    public class OrdinaryDatabase: IDatabase
    {
        private Dictionary<string, int> _capitals;
        public OrdinaryDatabase()
        {
            Console.WriteLine("Initalizing database");
            _capitals = File.ReadAllLines(@"C:\Users\dg_gt\RiderProjects\Console_BookExamples\capitals.txt")
                .Batch(2) // Comes from a external package "Nuget"
                .ToDictionary(capital => capital.ElementAt(0).Trim(),
                    population => int.Parse(population.ElementAt(1)));
        }

        public int GetPopulation(string name)
        {
            return _capitals[name];
        }
    }

    [TestFixture]
    public class SingletonTests
    {
        [Test]
        public void IsSingleTonTest()
        {
            var db = SingleDatabase.Instance;
            var db2 = SingleDatabase.Instance;
            Assert.That(db, Is.SameAs(db2));
            Assert.That(SingleDatabase.Count, Is.EqualTo(1));
        }

        [Test]
        public void SingletonTotalPopulationTest()
        {
            var rf = new SingleTonRecordFinder();
            var names = new[] {"Seoul", "Sao Paulo"};
            int tp = rf.GetTotalPopulation(names);
            Assert.That(tp, Is.EqualTo(17500000 + 17700000));
        }

        [Test]
        public void ConfigurablePopulationTest()
        {
            var rf = new ConfigurableRecordFinder(new DummyDatabase());
            var names = new[] {"alpha", "gamma"};
            int tp = rf.GetTotalPopulation(names);
            Assert.That(tp, Is.EqualTo(4));
        }

        [Test]
        public void DIPopulationTest()
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<OrdinaryDatabase>().As<IDatabase>().SingleInstance();
            cb.RegisterType<ConfigurableRecordFinder>();
            using (var c = cb.Build())
            {
                var rf = c.Resolve<ConfigurableRecordFinder>();
            }
            
        }
        
    }

    internal static class Demo
    {
        private static void Main(string[] args)
        {
            var db = SingleDatabase.Instance;
            var city = "Tokyo";
            Console.WriteLine($"{city} has population {db.GetPopulation(city)}");
        }
    }
}