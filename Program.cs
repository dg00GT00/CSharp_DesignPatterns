using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Console_BookExamples
{
    //************* State principle *************//
    // A pattern in which the object's behaviour is determined by its state
    // An object transitions from one state to another (something needs to trigger a transition
    // A formalized construct which manages state and transitions is called a state machine
    public enum State
    {
        OffHook,
        Connecting,
        Connected,
        OnHold
    }

    public enum Trigger
    {
        CallDialed,
        HungUp,
        CallConnected,
        PlaceOnHold,
        TakeOffHold,
        LeftMessage
    }

    internal static class Demo
    {
        private static readonly Dictionary<State, List<(Trigger, State)>> Rules =
            new Dictionary<State, List<(Trigger, State)>>
            {
                [State.OffHook] = new List<(Trigger, State)>
                {
                    (Trigger.CallDialed, State.Connecting)
                },
                [State.Connecting] = new List<(Trigger, State)>
                {
                    (Trigger.HungUp, State.OffHook),
                    (Trigger.CallConnected, State.Connected)
                },
                [State.Connected] = new List<(Trigger, State)>
                {
                    (Trigger.LeftMessage, State.OffHook),
                    (Trigger.HungUp, State.OffHook),
                    (Trigger.PlaceOnHold, State.OnHold)
                },
                [State.OnHold] = new List<(Trigger, State)>
                {
                    (Trigger.TakeOffHold, State.Connected),
                    (Trigger.HungUp, State.OffHook)
                }
            };

        private static void Main(string[] args)
        {
            var state = State.OffHook;
            while (true)
            {
                Console.WriteLine($"The phone is currently {state}");
                Console.WriteLine("Select a trigger:");

                for (var index = 0; index < Rules[state].Count; index++)
                {
                    var (t, _) = Rules[state][index];
                    Console.WriteLine($"{index}. {t}");
                }

                int input = int.Parse(Console.ReadLine());
                var (_, s) = Rules[state][input];
                state = s;
            }
        }
    }
}