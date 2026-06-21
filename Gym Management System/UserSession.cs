using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Management_System
{
    // Static class to manage the currently logged-in user's details globally
    internal class UserSession
    {
        // Session properties
        public static string UserID { get; set; }
        public static string UserName { get; set; }
        public static int UserRole { get; set; } // 1:Admin, 2:Staff, 3:Coach, 4:Member

        // Method to reset the session upon logout
        public static void ClearSession()
        {
            UserID = null;
            UserName = null;
            UserRole = 0;
        }
    }
}
