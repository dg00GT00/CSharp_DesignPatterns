using System;
using System.Text;

namespace Console_BookExamples
{
    //************* State principle *************//
    // A pattern in which the object's behaviour is determined by its state
    // An object transitions from one state to another (something needs to trigger a transition
    // A formalized construct which manages state and transitions is called a state machine
    // Given sufficient complexity, it pays to formally define possible states and events/triggers
    // Can define:
     //    State entry/exit behaviors
     //    Action when a particular event causes a transition
     //    Guard conditions enabling/disabling a transition
     //    Default action when no transitions are found for an event
    public enum State
    {
        Locked,
        Failed,
        Unlocked
    }
    internal static class Demo
    {
        private static void Main(string[] args)
        {
            var code = "1234";
            var state = State.Locked;
            var entry = new StringBuilder();

            while (true)
            {
                switch (state)
                {
                    case State.Locked:
                        entry.Append(Console.ReadKey().KeyChar);
                        if (entry.ToString() == code)
                        {
                            state = State.Unlocked;
                        }

                        if (!code.StartsWith(entry.ToString()))
                        {
//                            state = State.Failed;
                            goto case State.Failed;
                        }

                        break;
                    case State.Failed:
                        Console.CursorLeft = 0;
                        Console.WriteLine("FAILED");
                        entry.Clear();
                        state = State.Locked;
                        break;
                    case State.Unlocked:
                        Console.CursorLeft = 0;
                        Console.WriteLine("UNLOCKED");
                        return;
                }
            }
        }
    }
}