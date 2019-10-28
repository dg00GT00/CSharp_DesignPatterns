using System;

namespace Console_BookExamples
{
    //************* Chain responsibility principle *************//
    // A chain of components who all get a chance to process a command or a query,
    // optionally having default processing implementation and an ability to terminate
    // the processing chain
    public class Creature
    {
        public string Name;
        public int Attack, Defense;

        public Creature(string name, int attack, int defense)
        {
            Name = name;
            Attack = attack;
            Defense = defense;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Attack)}: {Attack}, {nameof(Defense)}: {Defense}";
        }
    }

    public class CreatureModifier
    {
        protected Creature _creature;
        protected CreatureModifier _next; // linked list

        public CreatureModifier(Creature creature)
        {
            _creature = creature;
        }

        public void Add(CreatureModifier cm)
        {
            if (_next != null)
            {
                _next.Add(cm);
            }
            else
            {
                _next = cm;
            }
        }

        public virtual void Handle() => _next?.Handle();
    }

    public class DoubleAttackModifier : CreatureModifier
    {
        public DoubleAttackModifier(Creature creature) : base(creature)
        {
        }

        public override void Handle()
        {
            Console.WriteLine($"Doubling {_creature.Name}'s attack");
            _creature.Attack *= 2;
            base.Handle();
        }
    }

    public class IncreasedDefenseModifier : CreatureModifier
    {
        public IncreasedDefenseModifier(Creature creature) : base(creature)
        {
        }

        public override void Handle()
        {
            Console.WriteLine($"Increasing {_creature.Defense}'s defense");
            _creature.Defense += 3;
            base.Handle();
        }
    }

    public class NoBonusesModifier : CreatureModifier
    {
        public NoBonusesModifier(Creature creature) : base(creature)
        {
        }

        public override void Handle()
        {
            // Not implementing this method breaks the chain
        }
    }

    internal static class Demo
    {
        private static void Main(string[] args)
        {
            var goblin = new Creature("Goblin", 2, 2);
            Console.WriteLine(goblin);

            var root = new CreatureModifier(goblin);
            root.Add(new DoubleAttackModifier(goblin));

            Console.WriteLine("Let's increase goblin's defense");
            root.Add(new IncreasedDefenseModifier(goblin));

            root.Handle();
            Console.WriteLine(goblin);
        }
    }
}