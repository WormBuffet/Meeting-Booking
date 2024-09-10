using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingLib
{
    public class InputValidation
    {
        DatabaseRequest request;

        public InputValidation()
        {
            request = new DatabaseRequest();
        }

        public String Validate(string Name, DateTime Start, DateTime End, Room Selected, ObservableCollection<Person> persons, Person person)
        {
            Meeting meeting = new Meeting(Selected.GetID(), Name, Start, End);
            DateTime Now = new DateTime(DateTime.Now.Year, DateTime.Now.Month,
                DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0); // current time

            Debug.WriteLine(End.Hour - Start.Hour);
            Debug.WriteLine(End.Minute - Start.Minute);

            if (Start > End)
            {
                // the start time must be earlier than the end time
                return "Start Time Cannot be After End Time!";
            }
            else if (Start == End)
            {
                // the start and end time must be different
                return "Meeting Cannot Start and End at the Same Time!";
            }
            else if (Start < Now)
            {
                // because these meetings are ad hoc, they cannot start at a time which is earlier than the current time
                return "Meeting Must Start Later in the Day!";
            }
            else if (Selected.GetCapacity() < persons.Count + 1) // list is incremented by 1 to take into account the person making the meeting
            {
                // meeting room capacity must be adhered to
                return "Too Many Participants!";
            }
            else if (!request.IsRoomVacant(Selected.GetID(), Start, End))
            {
                // the meeting cannot happen unless the requested room is vacant
                return "Room Is Occupied During This Time!";
            }
            else if (Name.Length < 8 || Name.Length > 128)
            {
                // the room name must be between 8 and 128 characters
                return "Please Enter A Valid Meeting Name!";
            }
            else if ((End.Hour - Start.Hour) >= 1 && (End.Minute - Start.Minute) >= 1 || (End.Hour - Start.Hour) >= 2)
            {
                // meetings can only be a maximum of 1 hour long
                return "1 Hour Maximum for Meeting!";
            }
            else if (persons.Count < 1)
            {
                // the meeting must have more than 1 member attending
                return "Invite List Empty!";
            }
            else
            {
                int meetingID = request.CreateMeeting(meeting, person);
                if (meetingID <= -1)
                {
                    // unexpected error; it is expected that this code should never be reached
                    return "Something Went Wrong, Please Contact an Admin!";
                }
                else
                {
                    // add each person in the collection to the meeting
                    foreach (Person p in persons)
                    {
                        request.AddToMeeting(p.GetID(), meetingID);
                    }
                    return "";
                }
            }
        }

        public string EditValidate(string Name, DateTime Start, DateTime End, Room Selected, ObservableCollection<Person> persons, Person person, Meeting m)
        {
            Meeting meeting = new Meeting(Selected.GetID(), Name, Start, End);
            DateTime Now = new DateTime(DateTime.Now.Year, DateTime.Now.Month,
                DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0); // current time
            DateTime[] dateTime = request.GetMeetingTimes(m.GetID());

            if (Start > End)
            {
                // the start time must be earlier than the end time
                return "Start Time Cannot be After End Time!";
            }
            else if (Start == End)
            {
                // the start and end time must be different
                return "Meeting Cannot Start and End at the Same Time!";
            }
            else if (Start < Now)
            {
                // because these meetings are ad hoc, they cannot start at a time which is earlier than the current time
                return "Meeting Must Start Later in the Day!";
            }
            else if (Selected.GetCapacity() < persons.Count + 1) // list is incremented by 1 to take into account the person making the meeting
            {
                // meeting room capacity must be adhered to
                return "Too Many Participants!";
            }
            else if (!request.IsRoomVacantEdit(Selected.GetID(), Start, End, m.GetID()))
            {
                // the meeting cannot happen unless the requested room is vacant
                return "Room is Occupied During This Time!";
            }
            else if (Name.Length < 8 || Name.Length > 128)
            {
                // the room name must be between 8 and 128 characters
                return "Please Enter a Valid Meeting Name!";
            }
            else if((End.Hour - Start.Hour) >= 1 && (End.Minute - Start.Minute) >= 1 || (End.Hour - Start.Hour) >= 2)
            {
                // meetings can only be a maximum of 1 hour long
                return "1 Hour Maximum for Meeting!";
            }
            else if (persons.Count < 1)
            {
                // the meeting must have more than 1 member attending
                return "Invite List Empty!";
            }
            else
            {
                // since this is an edit request, delete the current instance of the meeting
                request.DeleteMeeting(m.GetID());

                int meetingID = request.CreateMeeting(meeting, person);
                if (meetingID <= -1)
                {
                    // unexpected error; it is expected that this code should never be reached
                    return "Something Went Wrong, Please Contact an Admin!";
                }
                else
                {
                    // add each person in the collection to the meeting
                    foreach (Person p in persons)
                    {
                        request.AddToMeeting(p.GetID(), meetingID);
                    }
                    return "";
                }
            }
        }
    }
}
