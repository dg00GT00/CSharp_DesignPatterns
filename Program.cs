using System;

namespace Console_BookExamples
{
    //************* Decorator principle *************//
    // Facilitates the addition of behaviors to individual objects without
    // inheriting from them
    public interface IBird
    {
        void Fly();
        int Weight { get; set; }
    }

    public class Bird : IBird
    {
        public int Weight { get; set; }

        public void Fly()
        {
            Console.WriteLine($"Soring in the sky with weigth: {Weight}");
        }
    }

    public interface ILizard
    {
        void Crawl();
        int Weight { get; set; }
    }

    public class Lizard : ILizard
    {
        public int Weight { get; set; }

        public void Crawl()
        {
            Console.WriteLine($"Crawling in the dirt with weight: {Weight}");
        }
    }

    public class Dragon : IBird, ILizard
    {
        private Bird _bird = new Bird();
        private Lizard _lizard = new Lizard();
        private int _weight;

        public void Crawl()
        {
            _lizard.Crawl();
        }

        public void Fly()
        {
            _bird.Fly();
        }

        public int Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                _bird.Weight = value;
                _lizard.Weight = value;
            }
        }
    }
    internal static class Demo
    {
        private static void Main(string[] args)
        {
            var d = new Dragon();
            d.Weight = 234;
            d.Fly();
            d.Crawl();
        }
    }
}