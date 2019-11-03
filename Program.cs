using System;
using System.Security.Cryptography;

namespace Console_BookExamples
{
    //*********************** Memento responsibility principle ***********************//
    // A token/handle representing the system state. Lets us roll back to the state
    // when the token was generated. May or may not directly expose state information
    public class Memento
    {
        public int Balance { get; }

        public Memento(int balance)
        {
            Balance = balance;
        }
    }

    public class BankAccount
    {
        private int _balance;

        public BankAccount(int balance)
        {
            _balance = balance;
        }

        public Memento Deposit(int amount)
        {
            _balance += amount;
            return new Memento(_balance);
        }

        public void Restore(Memento m)
        {
            _balance = m.Balance;
        }

        public override string ToString()
        {
            return $"{nameof(_balance)}: {_balance}";
        }
    }

    internal static class Demo
    {
        private static void Main(string[] args)
        {
            var ba = new BankAccount(100);
            var m1 = ba.Deposit(50);
            var m2 = ba.Deposit(25);
            Console.WriteLine(ba);
            
            ba.Restore(m1);
            Console.WriteLine(ba);
            
            ba.Restore(m2);
            Console.WriteLine(ba);
        }
    }
}