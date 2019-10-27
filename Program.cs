using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using JetBrains.dotMemoryUnit;
using NUnit.Framework;

namespace Console_BookExamples
{
    //************* Flyweight principle *************//
    // A space optimization technique that lets use use less memory by storeing externally the data
    // associate with similar objects
    internal class User
    {
        private string _fullName;

        public User(string fullName)
        {
            _fullName = fullName;
        }
    }

    internal class User2
    {
        static List<string> strings = new List<string>();
        private int[] _names;

        public User2(string fullName)
        {
            int GetOrAdd(string s)
            {
                int idx = strings.IndexOf(s);
                if (idx != -1) return idx;
                strings.Add(s);
                return strings.Count - 1;
            }

            _names = fullName.Split(' ').Select(GetOrAdd).ToArray();
        }

        public string FullName => string.Join(" ", _names.Select(i => strings[i]));
    }

    [TestFixture]
    internal static class Demo
    {
        private static void Main(string[] args)
        {
        }

        [Test]
        public static void TestUser() //3340613
        {
            var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var users = (
                from firstName in firstNames
                from lastNme in lastNames
                select new User($"{firstName} {lastNames}")
            ).ToList();

            ForceGc();

            dotMemory.Check(memory => { Console.WriteLine(memory.SizeInBytes); });
        }
        [Test]
        public static void TestUser2() //1543885
        {
            var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var users = (
                from firstName in firstNames
                from lastNme in lastNames
                select new User2($"{firstName} {lastNames}")
            ).ToList();

            ForceGc();

            dotMemory.Check(memory => { Console.WriteLine(memory.SizeInBytes); });
        }

        private static void ForceGc()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private static string RandomString()
        {
            Random rand = new Random();
            return new string(Enumerable.Range(0, 10).Select(i => (char) ('a' + rand.Next(26))).ToArray());
        }
    }
}