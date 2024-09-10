using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace MeetingLib
{
    public class DatabaseRequest
    {
        MySqlConnection Connection;

        private int saltByteCount; // number of bytes for the salt
        private int hashByteCount; // number of bytes for the hash
        private int totalBytes; // total bytes
        private int iterations; // number of hashing iterations

        public DatabaseRequest()
        {
            //string conn = "server=localhost;user=root;database=cmp307;port=3306;password=";
            string conn = "server=lochnagar.abertay.ac.uk;user=sql1905943;database=sql1905943;password=UdsMsGBtBdu6";
            Connection = new MySqlConnection(conn);

            saltByteCount = 16;
            hashByteCount = 20;
            totalBytes = hashByteCount + saltByteCount;
            iterations = 10000;
        }

        public bool UserLogin(string username, string password)
        {
            Connection.Open(); // open the connection to allow sql statements to be executed
            MySqlCommand sql = new MySqlCommand("select * from person where personUsername = @usern", Connection);

            sql.Parameters.Add("@usern", MySqlDbType.VarChar).Value = username; // bind the username as a parameter
            MySqlDataReader dataReader = sql.ExecuteReader(CommandBehavior.CloseConnection);

            // while an instance of the username can be found in the database
            while (dataReader.Read())
            {
                string passwordHash = dataReader.GetString(2); // hash from database
                byte[] hashBytes = Convert.FromBase64String(passwordHash); // convert hash to bytes

                byte[] salt = new byte[saltByteCount];
                Array.Copy(hashBytes, 0, salt, 0, saltByteCount);
                var hashType = new Rfc2898DeriveBytes(password, salt, iterations);
                byte[] hash = hashType.GetBytes(hashByteCount);

                int access = 0;
                for (int i = 0; i < hashByteCount; i++)
                    if (hashBytes[i + saltByteCount] != hash[i])
                        access = -1;

                // if the password matches the record in the database, allow user to log in
                if (access >= 0)
                {
                    Connection.Close();
                    return true;
                }
            }
            // credentials not found; user cannot log in
            Connection.Close(); // close the connection
            return false;
        }

        public int GetIdByUser(string username)
        {
            int id = 0;
            Connection.Open();

            MySqlCommand sql = new MySqlCommand("select * from person where personUsername = @usern", Connection);
            sql.Parameters.Add("@usern", MySqlDbType.VarChar).Value = username;
            MySqlDataReader dataReader = sql.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                id = dataReader.GetInt32(0);
            }
            Connection.Close();
            return id;
        }

        public Person GetUserById(int id)
        {
            Person p = new Person(id);
            Connection.Open();

            MySqlCommand sql = new MySqlCommand("select * from person where personID = @pid", Connection);
            sql.Parameters.Add("@pid", MySqlDbType.Int32).Value = id;
            MySqlDataReader dataReader = sql.ExecuteReader(CommandBehavior.CloseConnection);

            while(dataReader.Read())
            {
                p = new Person(id, dataReader.GetString(1));
            }
            Connection.Close();

            return p;
        }

        public Room GetRoomById(int id)
        {
            Room r = new Room(id);
            Connection.Open();

            MySqlCommand sql = new MySqlCommand("select * from room where roomID = @rid", Connection);
            sql.Parameters.Add("@rid", MySqlDbType.Int32).Value = id;
            MySqlDataReader dataReader = sql.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                r = new Room(id, dataReader.GetInt32(1), dataReader.GetString(2));
            }
            Connection.Close();
            return r;
        }

        public Room[] GetRoomList()
        {
            List<Room> roomList = new List<Room>();
            Connection.Open();

            MySqlCommand sql = new MySqlCommand("select * from room", Connection);
            MySqlDataReader dataReader = sql.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                roomList.Add(new Room(dataReader.GetInt32(0), dataReader.GetInt32(1), dataReader.GetString(2)));
            }
            Connection.Close();
            return roomList.ToArray();
        }


        #region GetMeetings
        public List<Meeting> GetEditMeetingList(int pID)
        {
            List<Meeting> meetingList = new List<Meeting>();
            Connection.Open();

            MySqlCommand sql = new MySqlCommand("select * from meeting where fk_personID = @pid", 
                Connection);
            sql.Parameters.Add("@pid", MySqlDbType.Int32).Value = pID;
            MySqlDataReader dataReader = sql.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                meetingList.Add(new Meeting(dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetDateTime(2), 
                    dataReader.GetDateTime(3), dataReader.GetInt32(4)));
            }
            Connection.Close();
            return meetingList;
        }

        public List<Meeting> GetActiveMeetingList(int pID)
        {
            List<int> meetingIDs = new List<int>();
            List<Meeting> meetingList = new List<Meeting>();
            Connection.Open();

            MySqlCommand sql = new MySqlCommand("select * from meeting_person where fk_personID = @pid",
                Connection);
            sql.Parameters.Add("@pid", MySqlDbType.Int32).Value = pID;
            MySqlDataReader dataReader = sql.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                meetingIDs.Add(dataReader.GetInt32(1));
            }
            Connection.Close();

            foreach(int i in meetingIDs)
            {
                Meeting m = GetMeetingsById(i);
                meetingList.Add(m);
            }
            return meetingList;
        }

        public Meeting GetMeetingsById(int mID)
        {
            Meeting m = new Meeting(mID);
            Connection.Open();
            MySqlCommand sql2 = new MySqlCommand("select * from meeting where meetingID = @mid",
            Connection);
            sql2.Parameters.Add("@mid", MySqlDbType.Int32).Value = mID;
            MySqlDataReader dataReader2 = sql2.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader2.Read())
            {
                m = new Meeting(dataReader2.GetInt32(0), dataReader2.GetString(1), dataReader2.GetDateTime(2),
                    dataReader2.GetDateTime(3), dataReader2.GetInt32(4));
            }
            Connection.Close();
            return m;
        }
        #endregion

        public bool IsRoomVacant(int rID, DateTime start, DateTime end)
        {
            bool IsVacant = true; // boolean for whether the room is vacant or not
            Connection.Open();

            MySqlCommand sql = new MySqlCommand("select * from meeting where fk_roomID = @rid", Connection);
            sql.Parameters.Add("@rid", MySqlDbType.Int32).Value = rID;
            MySqlDataReader dataReader = sql.ExecuteReader(CommandBehavior.CloseConnection);

            while(dataReader.Read())
            {
                //bool ja = dataReader.GetDateTime(2) >= start;
                //Debug.WriteLine(ja);
                //bool da = dataReader.GetDateTime(3) >= end;
                //Debug.WriteLine(da);

                //Debug.WriteLine(end + " " + dataReader.GetDateTime(2));
                //bool baba = end >= dataReader.GetDateTime(2);
                //Debug.WriteLine(baba);

                if (dataReader.GetDateTime(2) >= start && dataReader.GetDateTime(3) <= end)
                {
                    // meeting already exists within that timeframe
                    IsVacant = false;
                }else if(dataReader.GetDateTime(2) >= start && dataReader.GetDateTime(3) >= end)
                {
                    if (end >= dataReader.GetDateTime(2))
                    {
                        // meeting which starts before user meeting already exists
                        IsVacant = false;
                    }
                }else if(dataReader.GetDateTime(2) <= start && dataReader.GetDateTime(3) <= end)
                {
                    if(start <= dataReader.GetDateTime(3))
                    {
                        // meeting which does not end before user meeting already exists
                        IsVacant = false;
                    }
                }else if(dataReader.GetDateTime(2) <= start && dataReader.GetDateTime(3) >= end)
                {
                    // meeting which does not end before user meeting already exists
                    IsVacant = false;
                }
            }
            Connection.Close();
            return IsVacant;
        }

        public bool IsRoomVacantEdit(int rID, DateTime start, DateTime end, int mID)
        {
            bool IsVacant = true; // boolean for whether the room is vacant or not
            Connection.Open();

            MySqlCommand sql = new MySqlCommand("select * from meeting where fk_roomID = @rid and not meetingID = @mid", Connection);
            sql.Parameters.Add("@rid", MySqlDbType.Int32).Value = rID;
            sql.Parameters.Add("@mid", MySqlDbType.Int32).Value = mID;
            MySqlDataReader dataReader = sql.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                if (dataReader.GetDateTime(2) >= start && dataReader.GetDateTime(3) <= end)
                {
                    // meeting already exists within that timeframe
                    IsVacant = false;
                }
                else if (dataReader.GetDateTime(2) >= start && dataReader.GetDateTime(3) >= end)
                {
                    if (end >= dataReader.GetDateTime(2))
                    {
                        // meeting which starts before user meeting already exists
                        IsVacant = false;
                    }
                }
                else if (dataReader.GetDateTime(2) <= start && dataReader.GetDateTime(3) <= end)
                {
                    if (start <= dataReader.GetDateTime(3))
                    {
                        // meeting which does not end before user meeting already exists
                        IsVacant = false;
                    }
                }
                else if (dataReader.GetDateTime(2) <= start && dataReader.GetDateTime(3) >= end)
                {
                    // meeting which does not end before user meeting already exists
                    IsVacant = false;
                }
            }
            Connection.Close();
            return IsVacant;
        }

        public int CreateMeeting(Meeting meeting, Person person)
        {
            int Error = -1;
            int Result;
            Connection.Open();
            MySqlCommand sql = new MySqlCommand
                ("insert into meeting set meetingName = @name, meetingStart = @start, meetingEnd = @end, fk_roomID = @rid, fk_personID = @pid", 
                Connection);

            sql.Parameters.Add("@name", MySqlDbType.String).Value = meeting.GetName();
            sql.Parameters.Add("@start", MySqlDbType.DateTime).Value = meeting.GetStart();
            sql.Parameters.Add("@end", MySqlDbType.DateTime).Value = meeting.GetEnd();
            sql.Parameters.Add("@rid", MySqlDbType.Int32).Value = meeting.GetRoomID();
            sql.Parameters.Add("@pid", MySqlDbType.Int32).Value = person.GetID();
            Result = sql.ExecuteNonQuery();

            if(Result > 0)
            {
                int ID = Error;
                MySqlCommand command = new MySqlCommand
                    ("select * from meeting where fk_personID = @pid order by meetingID asc",
                    Connection);
                command.Parameters.Add("@pid", MySqlDbType.Int32).Value = person.GetID();

                MySqlDataReader mysqlread = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (mysqlread.Read())
                {
                    ID = mysqlread.GetInt32(0);
                }
                Connection.Close();
                return ID;
            }
            Connection.Close();
            return Error;
        }

        public bool AddToMeeting(int pID, int mID)
        {
            int Result;
            Connection.Open();
            MySqlCommand sql = new MySqlCommand
                ("insert into `meeting_person` (`fk_personID`, `fk_meetingID`) values (@pid, @mid)",
                Connection);
            sql.Parameters.Add("@pid", MySqlDbType.Int32).Value = pID;
            sql.Parameters.Add("@mid", MySqlDbType.Int32).Value = mID;
            Result = sql.ExecuteNonQuery();
            Connection.Close();

            if (Result > 0)
                return true;

            return false;
        }

        public List<Person> GetMeetingUsers(int mID)
        {
            List<int> idList = new List<int>();
            List<Person> persons = new List<Person>();
            Connection.Open();
            MySqlCommand sql = new MySqlCommand
                ("select * from meeting_person where fk_meetingID = @mid",
                Connection);
            sql.Parameters.Add("@mid", MySqlDbType.Int32).Value = mID;
            MySqlDataReader dataReader = sql.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                idList.Add(dataReader.GetInt32(0));
            }
            Connection.Close();

            foreach(int i in idList)
            {
                Person p = GetUserById(i);
                persons.Add(p);
            }

            return persons;
        }

        public bool DeleteMeeting(int mID)
        {
            Connection.Open();

            MySqlCommand sql2 = new MySqlCommand
                ("delete from meeting_person where fk_meetingID = @mid",
                Connection);
            sql2.Parameters.Add("@mid", MySqlDbType.Int32).Value = mID;
            sql2.ExecuteNonQuery();
            int Table2 = sql2.ExecuteNonQuery();

            MySqlCommand sql = new MySqlCommand
                ("delete from meeting where meetingID = @mid",
                Connection);
            sql.Parameters.Add("@mid", MySqlDbType.Int32).Value = mID;
            int Table1 = sql.ExecuteNonQuery();

            Connection.Close();
            if (Table1 > 0 && Table2 > 0)
                return true;

            return false;
        }

        public DateTime[] GetMeetingTimes(int mID)
        {
            DateTime[] dt = new DateTime[2];
            Connection.Open();
            MySqlCommand sql = new MySqlCommand
                ("select meetingStart, meetingEnd from meeting where meetingID = @mid",
                Connection);
            sql.Parameters.Add("@mid", MySqlDbType.Int32).Value = mID;
            MySqlDataReader dataReader = sql.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                dt[0] = dataReader.GetDateTime(0);
                dt[1] = dataReader.GetDateTime(1);
            }
            Connection.Close();
            return dt;
        }

        #region OldCode

        /*
         * public bool UserLogin(string username, string password)
        {
            Connection.Open(); // open the connection to allow sql statements to be executed
            MySqlCommand sql = new MySqlCommand("select * from person where personUsername = @usern", Connection);

            sql.Parameters.Add("@usern", MySqlDbType.VarChar).Value = username; // bind the username as a parameter
            MySqlDataReader dataReader = sql.ExecuteReader(CommandBehavior.CloseConnection);

            // while an instance of the username can be found in the database
            while (dataReader.Read())
            {
                // if the password matches the record in the database, allow user to log in
                if(dataReader.GetString(2) == password)
                {
                    Connection.Close();
                    return true;
                }
            }
            // credentials not found; user cannot log in
            Connection.Close(); // close the connection
            return false;
        */

        #endregion
    }
}
