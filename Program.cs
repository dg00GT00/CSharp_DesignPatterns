using System;

namespace Console_BookExamples
{
    //************* Chain responsibility principle *************//
    // A chain of components who all get a chance to process a command or a query,
    // optionally having default processing implementation and an ability to terminate
    // the processing chain
    // Enlist objects in the chain, possibly controlling their order
    // Object removal from chain (e.g. in Dispose())
    public class Game
    {
        public event EventHandler<Query> Queries;

        public void PerformQuery(object sender, Query q)
        {
            Queries?.Invoke(sender, q);
        }
    }

    public class Query
    {
        public string CreateName;

        public enum Argument
        {
            Attack,
            Defense
        }

        public Argument WhatToQuery;
        public int Value;

        public Query(string name, Argument whatToQuery, int value)
        {
            CreateName = name;
            WhatToQuery = whatToQuery;
            Value = value;
        }
    }

    public class Creature
    {
        private Game _game;
        public string Name;
        private int _attack, _defense;

        public Creature(Game game, string name, int attack, int defense)
        {
            _game = game;
            Name = name;
            _attack = attack;
            _defense = defense;
        }

        public int Attack
        {
            get
            {
                var q = new Query(Name, Query.Argument.Attack, _attack);
                _game.PerformQuery(this, q);
                return q.Value;
            }
        }

        public int Defense
        {
            get
            {
                var q = new Query(Name, Query.Argument.Attack, _defense);
                _game.PerformQuery(this, q);
                return q.Value;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Attack)}: {Attack}, {nameof(Defense)}: {Defense}";
        }
    }

    public abstract class CreatureModifier : IDisposable
    {
        protected Game _game;
        protected Creature _creature;

        protected CreatureModifier(Game game, Creature creature)
        {
            _game = game;
            _creature = creature;
            _game.Queries += Handle;
        }

        protected abstract void Handle(object sender, Query q);


        public void Dispose()
        {
            _game.Queries -= Handle;
        }
    }

    public class DoubleAttackModifier : CreatureModifier
    {
        public DoubleAttackModifier(Game game, Creature creature) : base(game, creature)
        {
        }

        protected override void Handle(object sender, Query q)
        {
            if (q.CreateName == _creature.Name && q.WhatToQuery == Query.Argument.Attack)
            {
                q.Value *= 2;
            }
        }
    }


    internal static class Demo
    {
        private static void Main(string[] args)
        {
            var game = new Game();
            var goblin = new Creature(game, "Strong Goblin", 3, 3);
            Console.WriteLine(goblin);

            using (new DoubleAttackModifier(game, goblin))
            {
                Console.WriteLine(goblin);
            }

            Console.WriteLine(goblin);
        }
    }
}