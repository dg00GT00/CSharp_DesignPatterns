using System;

namespace Console_BookExamples
{
    //************* Proxy principle *************//
    // A class that function as an interface to a particular resource.
    // That resource may be remote, expensive to construct, or may require logging
    // or some other added functionality

    public interface ICar
    {
        void Drive();
    }

    public class Car : ICar
    {
        public void Drive()
        {
            Console.WriteLine("Car is being driven");
        }
    }

    public class Driver
    {
        public Driver(int age)
        {
            Age = age;
        }

        public int Age { get; set; }
    }

    public class CarProxy : ICar
    {
        private Driver _driver;
        private Car _car = new Car();

        public CarProxy(Driver driver)
        {
            _driver = driver;
        }

        public void Drive()
        {
            if (_driver.Age >= 16)
            {
                _car.Drive();
            }
            else
            {
                Console.WriteLine("You are too young to driver the car");
            }
        }
    }

    internal static class Demo
    {
        private static void Main(string[] args)
        {
            ICar car = new CarProxy(new Driver(12));
            car.Drive();
        }
    }
}