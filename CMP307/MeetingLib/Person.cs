using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingLib
{
    public class Person
    {
        private int ID;
        private string Password;
        public string Username { get; } // public for data binding

        public Person(int ID)
        {
            this.ID = ID;
        }

        public Person(int ID, string Username)
        {
            this.ID = ID;
            this.Username = Username;
        }

        public Person(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }

        public int GetID() { return ID; }

        public string GetUsername() { return Username; }

        public string GetPassword() { return Password; }
    }
}
