using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingLib
{
    public class AdminUser
    {
        private int ID;
        public string AdminName { get; } // public for data binding

        private string AdminPassword;

        //private string AdminSalt;

        public AdminUser(int ID)
        {
            this.ID = ID;
        }

        public AdminUser(int ID, string AdminName)
        {
            this.ID = ID;
            this.AdminName = AdminName;
        }

        public AdminUser(string AdminName, string AdminPassword)
        {
            this.AdminName = AdminName;
            this.AdminPassword = AdminPassword;
        }

        public int GetID() { return ID; }
        public string GetAdminName() { return AdminName; }
        public string GetAdminPassword() { return AdminPassword; }

        //public string GetAdminSalt() { return AdminSalt; }
    }
}
