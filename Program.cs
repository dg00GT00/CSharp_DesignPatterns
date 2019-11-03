using System;
using System.Collections.Generic;
using System.Linq;

namespace Console_BookExamples
{
    //****************************** Mediator principle ******************************//
    // A component that facilitates communication between other components without
    // them necessarily being aware of each other or having direct (reference) access to
    // each other
    // Create the mediator and have each object in the system refer to it
    // Mediator engages in bidirectional communication with its connected components
    // Components have functions the mediator can call
    // Event processing (e.g. RX) libraries make communication easier to implement
    public class Person
    {
        public string Name;
        public ChatRoom Room;
        private readonly List<string> _chatlog = new List<string>();

        public Person(string name)
        {
            Name = name;
        }

        public void Say(string message)
        {
            Room.Broadcast(Name, message);
        }

        public void PrivateMessage(string who, string message)
        {
            Room.Message(Name, who, message);
        }

        public void Receive(string sender, string message)
        {
            var s = $"{sender}: '{message}'";
            _chatlog.Add(s);
            Console.WriteLine($"[{Name}'s chat session] {s}");
        }
    }

    public class ChatRoom
    {
        private readonly List<Person> _people = new List<Person>();

        public void Join(Person p)
        {
            string joinMsg = $"{p.Name} joins the chat";
            Broadcast("room", joinMsg);
            p.Room = this;
            _people.Add(p);
        }

        public void Broadcast(string source, string message)
        {
            foreach (var person in _people.Where(person => person.Name != source))
            {
                person.Receive(source, message);
            }
        }

        public void Message(string source, string destination, string message)
        {
            _people.FirstOrDefault(p => p.Name == destination)?.Receive(source, message);
        }
    }

    internal static class Demo
    {
        private static void Main(string[] args)
        {
            var room = new ChatRoom();
            var john = new Person("John");
            var jane = new Person("Jane");
            
            room.Join(john);
            room.Join(jane);
            john.Say("Hi!");
            jane.Say("Oh, hey john");
            
            var simon = new Person("Simon");
            room.Join(simon);
            simon.Say("Hi everyone");
            
            jane.PrivateMessage("Simon", "glad you could join us!");
        }
    }
}