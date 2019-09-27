using System;

namespace Console_BookExamples
{
    //************* Single responsibility principle *************//
    // A typical class is responsible for only one specific operation 


    public class Point
    {
        private double x, y;

        /// <summary>
        /// Initialize a point form either cartesian or polar
        /// </summary>
        /// <param name="x">x if cartesian, rho if polar</param>
        /// <param name="y"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }

        private static class Factory
        {
            public static Point NewCartesianPoint(double x, double y)
            {
                return new Point(x, y);
            }

            public static Point NewPolarPoint(double rho, double theta)
            {
                return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
            }
        }

        internal static class Demo
        {
            private static void Main(string[] args)
            {
                var point = Factory.NewPolarPoint(1.0, Math.PI / 2);
                Console.WriteLine(point);
            }
        }
    }
}