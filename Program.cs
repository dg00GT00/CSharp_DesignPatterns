using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Console_BookExamples
{
    //************* Single responsibility principle *************//
    // A typical class is responsible for only one specific operation 
    internal class Journal
    {
        private readonly List<string> entries = new List<string>();
        private static int count = 0;

        public int AddEntry(string text)
        {
            entries.Add($"{++count}: {text}");
            return count;
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }
    }

    internal class Persistence
    {
        public void SaveToFile(Journal j, string filename, bool overwrite = false)
        {
            if (overwrite || !File.Exists(filename))
            {
                File.WriteAllText(filename, j.ToString());
            }
        }
    }

    internal static class Demo
    {
        private static void  Main(string[] args)
        {
            var j = new Journal();
            j.AddEntry("I cried today");
            j.AddEntry("I ate a bug");
            Console.WriteLine(j);
            
            var p = new Persistence();
            const string filename = @"C:\Users\dg_gt\Downloads\journal.txt";
            p.SaveToFile(j, filename, true);
            Process.Start(filename);
            
        }
    }
}