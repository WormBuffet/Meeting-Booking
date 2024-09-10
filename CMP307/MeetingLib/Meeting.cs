using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingLib
{
    public class Meeting
    {
        private int MeetingID;
        private int RoomID;

        public string MeetingName { get; }
        public DateTime MeetingStart { get; }
        public DateTime MeetingEnd { get; }

        public Meeting(int id, string name, DateTime start, DateTime end, int rid)
        {
            MeetingID = id;
            MeetingName = name;
            MeetingStart = start;
            MeetingEnd = end;
            RoomID = rid;
        }

        public Meeting(int rid, string name, DateTime start, DateTime end)
        {
            RoomID = rid;
            MeetingName = name;
            MeetingStart = start;
            MeetingEnd = end;
        }

        public Meeting(int mid)
        {
            MeetingID = mid;
        }

        public int GetID() { return MeetingID; }
        public string GetName() { return MeetingName; }
        public DateTime GetStart() { return MeetingStart; }
        public DateTime GetEnd() { return MeetingEnd; }
        public int GetRoomID() { return RoomID; }
    }
}
