using System;

namespace Console_BookExamples
{
    //************* Null Object responsibility principle *************//
    // A no-op object that conforms to the required interface, satisfying a 
    // dependency requirement of some other object
    public interface ILog
    {
        void Info(string msg);
        void Warn(string msg);
    }

    public class ConsoleLog : ILog
    {
        public void Info(string msg)
        {
            Console.WriteLine(msg);
        }

        public void Warn(string msg)
        {
            Console.WriteLine("WARNING!! " + msg);
        }
    }
    
    public class BankAccount
    {
        private ILog _log;
        private int _balance;

        public BankAccount(ILog log)
        {
            _log = log;
        }

        public void Deposit(int amount)
        {
            _balance += amount;
            _log.Info($"Deposited {amount}, balance is now {_balance}");
        }
    }
    
    public class NullLog : ILog
    {
        public void Info(string msg)
        {
            
        }

        public void Warn(string msg)
        {
            
        }
    }

    internal static class Demo
    {
        private static void Main(string[] args)
        {
            var log = new ConsoleLog();
            var ba = new BankAccount(log);
            ba.Deposit(10);
        }
    }
}