using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MeetingLib
{
    public class AdminDB
    {
        MySqlConnection Connection;

        private int saltByteCount; // number of bytes for the salt
        private int hashByteCount; // number of bytes for the hash
        private int totalBytes; // total bytes
        private int iterations; // number of hashing iterations

        public AdminDB()
        {
            //string conn = "server=localhost;user=root;database=cmp307;port=3306;password=";
            string conn = "server=lochnagar.abertay.ac.uk;user=sql1905943;database=sql1905943;password=UdsMsGBtBdu6";
            Connection = new MySqlConnection(conn);

            saltByteCount = 16;
            hashByteCount = 20; 
            totalBytes = hashByteCount + saltByteCount;
            iterations = 10000;
        }   

        #region Login
        public bool AdminLogin(string username, string password)
        {
            //string hashPassword = HashPassword(password);

            Connection.Open(); // open the connection to allow sql statements to be executed
            MySqlCommand sql = new MySqlCommand("select * from admin where adminUsername = @admin", Connection);

            sql.Parameters.Add("@admin", MySqlDbType.VarChar).Value = username; // bind the username as a parameter
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

                /*
                // if the password matches the record in the database, allow user to log in
                if (dataReader.GetString(2) == hashPassword)
                {
                    Connection.Close();
                    return true;
                }
                */
            }
            // credentials not found; user cannot log in
            Connection.Close(); // close the connection
            return false;
        }
        #endregion

        #region Creation

        public bool CreateRoom(Room room)
        {
            int Result;
            Connection.Open();

            MySqlCommand select = new MySqlCommand
                ("select * from room where roomName = @name",
                Connection); // checks if there is already a room with the inputted name
            select.Parameters.Add("@name", MySqlDbType.String).Value = room.GetName();
            MySqlDataReader dataReader = select.ExecuteReader(CommandBehavior.CloseConnection);

            if (!dataReader.HasRows)
            {
                dataReader.Close();
                Connection.Open();

                MySqlCommand sql = new MySqlCommand
                ("insert into room set roomName = @name, roomCapacity = @cap",
                Connection);

                sql.Parameters.Add("@name", MySqlDbType.String).Value = room.GetName();
                sql.Parameters.Add("@cap", MySqlDbType.Int32).Value = room.GetCapacity();
                Result = sql.ExecuteNonQuery();

                Debug.WriteLine(Result);

                if (Result > 0)
                {
                    Connection.Close();
                    return true;
                }
                Connection.Close();
                return false;
            }
            Connection.Close();
            return false;
        }

        public bool CreatePerson(Person person)
        {
            int Result;
            Connection.Open();

            MySqlCommand select = new MySqlCommand
                ("select * from person where personUsername = @name",
                Connection); // checks if there is already an admin with the inputted name
            select.Parameters.Add("@name", MySqlDbType.String).Value = person.GetUsername();
            MySqlDataReader dataReader = select.ExecuteReader(CommandBehavior.CloseConnection);

            if(!dataReader.HasRows)
            {
                dataReader.Close();
                Connection.Open();

                MySqlCommand sql = new MySqlCommand
                ("insert into person set personUsername = @name, personPassword = @salt",
                Connection);

                sql.Parameters.Add("@name", MySqlDbType.String).Value = person.GetUsername();
                sql.Parameters.Add("@salt", MySqlDbType.String).Value = HashPassword(person.GetPassword());
                Result = sql.ExecuteNonQuery();

                Debug.WriteLine(Result);

                if (Result > 0)
                {
                    Connection.Close();
                    return true;
                }
                Connection.Close();
                return false;
            }
            Connection.Close();
            return false;
        }

        public string HashPassword(string password)
        {
            byte[] salt; // create a new array of bytes
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[saltByteCount]);
            var hashSalt = new Rfc2898DeriveBytes(password, salt, iterations);

            byte[] hash = hashSalt.GetBytes(hashByteCount);
            byte[] hashBytes = new byte[totalBytes];

            Array.Copy(salt, 0, hashBytes, 0, saltByteCount);
            Array.Copy(hash, 0, hashBytes, saltByteCount, hashByteCount);
            string hashedPassword = Convert.ToBase64String(hashBytes);
            return hashedPassword;
        }

        public bool CreateAdmin(AdminUser admin)
        {
            int Result;
            Connection.Open();

            MySqlCommand select = new MySqlCommand
                ("select * from admin where adminUsername = @name",
                Connection); // checks if there is already an admin with the inputted name
            select.Parameters.Add("@name", MySqlDbType.String).Value = admin.GetAdminName();
            MySqlDataReader dataReader = select.ExecuteReader(CommandBehavior.CloseConnection);

            if (!dataReader.HasRows)
            {
                dataReader.Close();
                Connection.Open();

                MySqlCommand sql = new MySqlCommand
                ("insert into admin set adminUsername = @name, adminSalt = @salt",
                Connection);

                sql.Parameters.Add("@name", MySqlDbType.String).Value = admin.GetAdminName();
                sql.Parameters.Add("@salt", MySqlDbType.String).Value = HashPassword(admin.GetAdminPassword());
                Result = sql.ExecuteNonQuery();

                Debug.WriteLine(Result);

                if (Result > 0)
                {
                    Connection.Close();
                    return true;
                }
                Connection.Close();
                return false;
            }
            Connection.Close();
            return false;
        }

        #endregion

        #region GetUser
        public int GetIdByUser(string username)
        {
            int id = 0;
            Connection.Open();

            MySqlCommand sql = new MySqlCommand("select * from admin where adminUsername = @admin", Connection);
            sql.Parameters.Add("@admin", MySqlDbType.VarChar).Value = username;
            MySqlDataReader dataReader = sql.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                id = dataReader.GetInt32(0);
            }
            Connection.Close();
            return id;
        }

        #endregion

        #region Update

        public int FindRoomByName(string name)
        {
            int ID = -1;
            Connection.Open();

            MySqlCommand sql = new MySqlCommand("select * from room where roomName = @name", Connection);
            sql.Parameters.Add("@name", MySqlDbType.Int32).Value = name;
            MySqlDataReader dataReader = sql.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                ID = dataReader.GetInt32(0);
            }
            Connection.Close();
            return ID;
        }

        public bool UpdateRoomBoth(string name, int capacity, string oldName)
        {
            int Result;
            Connection.Open();

            MySqlCommand sql = new MySqlCommand
            ("update room set roomName = @name, roomCapacity = @cap where roomName = @oldName",
            Connection);

            sql.Parameters.Add("@name", MySqlDbType.String).Value = name;
            sql.Parameters.Add("@cap", MySqlDbType.Int32).Value = capacity;
            sql.Parameters.Add("@oldName", MySqlDbType.String).Value = oldName;
            Result = sql.ExecuteNonQuery();

            if (Result > 0)
            {
                Connection.Close();
                return true;
            }
            Connection.Close();
            return false;
        }

        public bool UpdateRoomName(string name, string oldName)
        {
            int Result;
            Connection.Open();

            MySqlCommand sql = new MySqlCommand
            ("update room set roomName = @name where roomName = @oldName",
            Connection);

            sql.Parameters.Add("@name", MySqlDbType.String).Value = name;
            sql.Parameters.Add("@oldName", MySqlDbType.String).Value = oldName;
            Result = sql.ExecuteNonQuery();

            if (Result > 0)
            {
                Connection.Close();
                return true;
            }
            Connection.Close();
            return false;
        }

        public bool UpdateRoomCap(int capacity, string oldName)
        {
            int Result;
            Connection.Open();

            MySqlCommand sql = new MySqlCommand
            ("update room set roomCapacity = @cap where roomName = @oldName",
            Connection);

            sql.Parameters.Add("@cap", MySqlDbType.String).Value = capacity;
            sql.Parameters.Add("@oldName", MySqlDbType.String).Value = oldName;
            Result = sql.ExecuteNonQuery();

            if (Result > 0)
            {
                Connection.Close();
                return true;
            }
            Connection.Close();
            return false;
        }

        #endregion
    }
}
