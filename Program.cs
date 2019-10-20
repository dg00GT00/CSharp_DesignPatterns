using System;

namespace Console_BookExamples
{
    //************************************** Decorator principle **************************************//
    // Facilitates the addition of behaviors to individual objects without inheriting from them
    // A decorator keeps the reference to the decorated objects(s)
    // May or may not proxy over calls
    // Exists in a static variation but is very limited due to inability to inherit from type parameters
    public abstract class Shape
    {
        public abstract string AsString();
    }

    public class Circle : Shape
    {
        private float _radius;

        public Circle() : this(0.0f)
        {
        }

        public Circle(float radius)
        {
            this._radius = radius;
        }

        public void Resize(float factor)
        {
            _radius *= factor;
        }

        public override string AsString() => $"A circle with radius {_radius}";
    }

    public class Square : Shape
    {
        private float _side;

        public Square() : this(0.0f)
        {
        }

        public Square(float side)
        {
            _side = side;
        }

        public override string AsString() => $"A square with side {_side}";
    }

    public class ColoredShape : Shape
    {
        private Shape _shape;
        private string _color;

        public ColoredShape(Shape shape, string color)
        {
            _shape = shape;
            _color = color;
        }

        public override string AsString() => $"{_shape.AsString()} has the color {_color}";
    }

    public class TransparentShape : Shape
    {
        private Shape _shape;
        private float _transparency;

        public TransparentShape(Shape shape, float transparency)
        {
            _shape = shape;
            this._transparency = transparency;
        }

        public override string AsString() => $"{_shape.AsString()} has {_transparency * 100.0}% transparency";
    }

    public class ColoredShape<T> : Shape where T : Shape, new()
    {
        private string _color;
        private T _shape = new T();

        public ColoredShape() : this("black")
        {
        }

        public ColoredShape(string color)
        {
            _color = color;
        }
        public override string AsString() => $"{_shape.AsString()} has the color {_color}";
    }

    public class TransparentShape<T> : Shape where T : Shape, new()
    {
        private float _transparency;
        private T _shape = new T();

        public TransparentShape() : this(0)
        {
        }

        public TransparentShape(float transparency)
        {
            _transparency = transparency;
        }


        public override string AsString() => $"{_shape.AsString()} has {_transparency * 100.0f}% transparency";
    }

    internal static class Demo
    {
        private static void Main(string[] args)
        {
            var redSqure = new ColoredShape<Square>();
            Console.WriteLine(redSqure.AsString());

            var circle = new TransparentShape<ColoredShape<Circle>>(0.4f);
            Console.WriteLine(circle.AsString());
        }
    }
}