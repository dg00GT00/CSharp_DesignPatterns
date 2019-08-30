using System;

namespace Console_BookExamples
{
    //************* Interface Segregate principle *************//
    // It's more worth segregate the interface into multiple
    // dedicated interfaces than code a multipurpose one 
    public class Document
    {
        
    }

    public interface IMachine
    {
        void Print(Document d);
        void Scan(Document d);
        void Fax(Document d);
        
    }

    public class MultiFunctionPrinter : IMachine
    {
        public void Print(Document d)
        {
            throw new NotImplementedException();
        }

        public void Scan(Document d)
        {
            throw new NotImplementedException();
        }

        public void Fax(Document d)
        {
            throw new NotImplementedException();
        }
    }

    public interface IPrinter
    {
        void Print(Document d);
        
    }

    public interface IScanner
    {
        void Scan(Document d);
        
    }

    public class Photocopier : IPrinter, IScanner
    {
        public void Print(Document d)
        {
            throw new NotImplementedException();
        }

        public void Scan(Document d)
        {
            throw new NotImplementedException();
        }
    }

    public interface IMultiFunctionDevice : IScanner, IPrinter
    {
        
    }

    public class MultiFunctionMachine : IMultiFunctionDevice
    {
        private IPrinter _printer;
        private IScanner _scanner;

        public MultiFunctionMachine(IPrinter printer, IScanner scanner)
        {
            _printer = printer;
            _scanner = scanner;
        }

        public void Scan(Document d)
        {
            _scanner.Scan(d);
        }

        public void Print(Document d)
        {
            _printer.Print(d);
            
        }
    }

    internal static class Demo
    {
        private static void Main()
        {
            
        }
    }
}