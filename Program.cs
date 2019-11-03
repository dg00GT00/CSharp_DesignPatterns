using System;
using System.Collections.Generic;

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
        private List<Memento> _changes = new List<Memento>();
        private int _current;

        public BankAccount(int balance)
        {
            _balance = balance;
            _changes.Add(new Memento(_balance));
        }

        public Memento Deposit(int amount)
        {
            _balance += amount;
            var m = new Memento(_balance);
            _changes.Add(m);
            ++_current;
            return m;
        }

        public Memento Restore(Memento m)
        {
            if (m == null) return null;
            _balance = m.Balance;
            _changes.Add(m);
            return m;
        }

        public Memento Undo()
        {
            if (_current <= 0) return null;
            var m = _changes[--_current];
            _balance = m.Balance;
            return m;
        }

        public Memento Redo()
        {
            if (_current + 1 >= _changes.Count) return null;
            var m = _changes[++_current];
            _balance = m.Balance;
            return m;
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
            ba.Deposit(50);
            ba.Deposit(25);
            Console.WriteLine(ba);

            ba.Undo();
            Console.WriteLine($"Undo 1: {ba}");
            ba.Undo();
            Console.WriteLine($"Undo 2: {ba}");
            ba.Redo();
            Console.WriteLine($"Redo 1: {ba}");

        }
    }
}