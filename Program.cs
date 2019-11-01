using System;
using System.Collections.Generic;
using System.Linq;

namespace Console_BookExamples
{
    //************* Command principle *************//
    // An object which represents an instruction to perform a particular action. Contains
    // all the information necessary for the action to be taken
    // Encapsulate all details of an operation in a separete object
    // Define instruction for applying the command (either in the command itself, or elsewhere)
    // Optionally define instructions for undoing the command
    // Can create composite commands (a.k.a. macros)
    public class BankAccount
    {
        private int _balance;
        private int _overdraftLimit = -500;

        public void Deposit(int amount)
        {
            _balance += amount;
            Console.WriteLine($"Deposited ${amount}, balance is now {_balance}");
        }

        public bool Withdraw(int amount)
        {
            if (_balance - amount < _overdraftLimit) return false;
            _balance -= amount;
            Console.WriteLine($"Withdrew ${amount}, balance is now {_balance}");
            return true;

        }

        public override string ToString()
        {
            return $"{nameof(_balance)}: {_balance}";
        }
    }

    public interface ICommand
    {
        void Call();
        void Undo();
    }

    public class BankAccountCommand : ICommand
    {
        private BankAccount _account;

        public enum Action
        {
            Deposit,
            Withdraw
        }

        private Action _action;
        private int _amount;
        private bool _succeeded;

        public BankAccountCommand(BankAccount account, Action action, int amount)
        {
            _account = account;
            _action = action;
            _amount = amount;
        }

        public void Call()
        {
            switch (_action)
            {
                case Action.Deposit:
                    _account.Deposit(_amount);
                    _succeeded = true;
                    break;
                case Action.Withdraw:
                    _succeeded = _account.Withdraw(_amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Undo()
        {
            if (!_succeeded)
            {
                return;
            }

            switch (_action)
            {
                case Action.Deposit:
                    _account.Withdraw(_amount);
                    break;
                case Action.Withdraw:
                    _account.Deposit(_amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    internal static class Demo
    {
        private static void Main(string[] args)
        {
            var ba = new BankAccount();
            var commands = new List<BankAccountCommand>
            {
                new BankAccountCommand(ba, BankAccountCommand.Action.Deposit, 100),
                new BankAccountCommand(ba, BankAccountCommand.Action.Withdraw, 1000),
            };
            Console.WriteLine(ba);
            foreach (var command in commands)
            {
                command.Call();
            }

            Console.WriteLine(ba);
            foreach (var c in Enumerable.Reverse(commands))
            {
                c.Undo();
            }

            Console.WriteLine(ba);
        }
    }
}