using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Management_System
{
    internal class UserSession
    {
        public static string UserID { get; set; }
        public static string UserName { get; set; }
        public static int UserRole { get; set; } // 1:Admin, 2:Staff, 3:Coach, 4:Member

        public static void ClearSession()
        {
            UserID = null;
            UserName = null;
            UserRole = 0;
        }
    }
}
