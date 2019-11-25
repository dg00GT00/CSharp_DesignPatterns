using System;

namespace Console_BookExamples
{
    //************* Template Method principle *************//
    // Allows us to define the 'skeleton' of the algorithm, with concrete
    // implementations defined in subclasses.
    // Define an algorithm at a high level
    // Define constituent parts as abstract methods/properties
    // Inherit the algorithm class, providing necessary overrides
    public abstract class Game
    {
        public void Run()
        {
            Start();
            while (!HaveWinner)
            {
                TakeTurn();
                Console.WriteLine($"Player {WinningPlayer} wins!");
            }
        }

        protected int currentPlayer;
        protected readonly int numberOfPlayers;

        protected Game(int numberOfPlayers)
        {
            this.numberOfPlayers = numberOfPlayers;
        }

        protected abstract void Start();
        protected abstract void TakeTurn();
        protected abstract bool HaveWinner { get; }
        protected abstract int WinningPlayer { get; }
    }

    public class Chess : Game
    {
        private int _turn = 1;
        private int _maxTurns = 10;

        public Chess() : base(2)
        {
        }

        protected override void Start()
        {
            Console.WriteLine($"Starting a game of chess with {numberOfPlayers} players.");
        }

        protected override void TakeTurn()
        {
            Console.WriteLine($"Turn {_turn++} taken by player {currentPlayer}.");
            currentPlayer = (currentPlayer + 1) % numberOfPlayers;
        }
        protected override bool HaveWinner => _turn == _maxTurns;
        protected override int WinningPlayer => currentPlayer;
    }

    internal static class Demo
    {
        private static void Main(string[] args)
        {
            var chess = new Chess();
            chess.Run();
        }
    }
}