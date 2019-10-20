using System;

namespace Console_BookExamples
{
    //************* Decorator principle *************//
    // Facilitates the addition of behaviors to individual objects without
    // inheriting from them
    public interface IShape
    {
        string AsString();
    }

    public class Circle : IShape
    {
        private float _radius;

        public Circle(float radius)
        {
            this._radius = radius;
        }

        public void Resize(float factor)
        {
            _radius *= factor;
        }

        public string AsString() => $"A circle with radius {_radius}";
    }

    public class Square : IShape
    {
        private float _side;

        public Square(float side)
        {
            _side = side;
        }

        public string AsString() => $"A square with side {_side}";
    }

    public class ColoredShape : IShape
    {
        private IShape _shape;
        private string _color;

        public ColoredShape(IShape shape, string color)
        {
            _shape = shape;
            _color = color;
        }

        public string AsString() => $"{_shape.AsString()} has the color {_color}";
    }

    public class TransparentShape: IShape
    {
        private IShape _shape;
        private float _transparency;

        public TransparentShape(IShape shape, float transparency)
        {
            _shape = shape;
            this._transparency = transparency;
        }
        public string AsString() => $"{_shape.AsString()} has {_transparency * 100.0}% transparency";
    }

    internal static class Demo
    {
        private static void Main(string[] args)
        {
            var square = new Square(1.23f);
            Console.WriteLine(square.AsString());

            var redSquare = new ColoredShape(square, "red");
            Console.WriteLine(redSquare.AsString());


            var redHalfTransparentSquare = new TransparentShape(redSquare, 0.5f);
            Console.WriteLine(redHalfTransparentSquare.AsString());
        }
    }
}