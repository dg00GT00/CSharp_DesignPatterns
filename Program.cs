using System;
using System.Collections.Generic;

namespace Console_BookExamples
{
    //************* Command principle *************//
    // An object which represents an instruction to perform a particular action. Contains
    // all the information necessary for the action to be taken
    public class BankAccount
    {
        private int _balance;
        private int _overdraftLimit = -500;

        public void Deposit(int amount)
        {
            _balance += amount;
            Console.WriteLine($"Deposited ${amount}, balance is now {_balance}");
        }

        public void Withdraw(int amount)
        {
            if (_balance - amount >= _overdraftLimit)
            {
                _balance -= amount;
                Console.WriteLine($"Withdrew ${amount}, balance is now {_balance}");

            }
        }

        public override string ToString()
        {
            return $"{nameof(_balance)}: {_balance}";
        }
    }

    public interface ICommand
    {
        void Call();
        
    }
    
    public class BankAccountCommand: ICommand
    {
        private BankAccount _account;

        public enum Action
        {
            Deposit, Withdraw
        }

        private Action _action;
        private int _amount;

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
                    break;
                case Action.Withdraw:
                    _account.Withdraw(_amount);
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
                new BankAccountCommand(ba, BankAccountCommand.Action.Withdraw, 50),
            };
            Console.WriteLine(ba);
            foreach (var command in commands)
            {
                command.Call();
            }
            Console.WriteLine(ba);
        }
    }
}