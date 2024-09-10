using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingLib
{
    public class MultipleParams
    {
        private Meeting meeting;
        private Person person;
        private Room room;
        private AdminUser admin;

        // overloaded constructor
        public MultipleParams(Meeting m, Person p)
        {
            meeting = m;
            person = p;
        }

        public MultipleParams(AdminUser a, Room r)
        {
            admin = a;
            room = r;
        }

        public Person GetPerson() { return person; }

        public Meeting GetMeeting() { return meeting; }

        public Room GetRoom() { return room; }

        public AdminUser GetAdmin() { return admin; }
    }
}
