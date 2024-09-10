using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingLib
{
    public class Room
    {
        private int id; // ID of the room
        //private int capacity; // capacity of the room
        public string Name { get; } // room name; public for data binding
        public int Capacity { get; } // capacity of the room; public for data binding

        // overloaded constructors
        public Room(int id, int capacity, string name)
        {
            this.id = id;
            this.Capacity = capacity;
            this.Name = name;
        }

        public Room(int capacity, string name)
        {
            this.Capacity = capacity;
            this.Name = name;
        }

        public Room(int id)
        {
            this.id = id;
        }

        public Room(string name)
        {
            this.Name = name;
        }

        public int GetID() { return id; }
        public string GetName() { return Name; }
        public int GetCapacity() { return Capacity; }
    }
}
