using System;

namespace Console_BookExamples
{
    //************* Liskov principle *************//
    
    internal class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public Rectangle()
        {
            
        }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    internal class Square : Rectangle
    {
        public override int Width
        {
            
            set => base.Width = base.Height = value;
        }


        public override int Height
        {
            set => base.Width = base.Height = value;
        }
    }
    
    internal static class Demo
    {
        private static int Area(Rectangle r) => r.Width * r.Height;
        private static void  Main(string[] args)
        {
            Rectangle rc = new Rectangle(2, 3);
            Console.WriteLine($"{rc} has are {Area(rc)}");
            
            // The follow syntax works without using the "override" and "virtual" keywords
            //  Rectangle sq = new Square {Width = 4};
            Rectangle sq = new Square();
            sq.Width = 4;
            Console.WriteLine($"{sq} has area {Area(sq)}");
        }
    }
}