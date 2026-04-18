using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Management_System
{
    internal class Enums
    {
        public enum UserStatus
        {
            Pending = 0,
            Active = 1,
            Hold = 2,
            Suspended = 3
        }

        public enum OrderStatus
        {
            Hold = 0,
            Completed = 1,
            Reversed = 2
        }
        public enum UserRole
        {
            Admin = 0,
            Staff = 1,
            Member = 2
        }
        public enum PaymentMethod
        {
            Cash = 0,
            CreditCard = 1,
            DebitCard = 2,
            MobilePayment = 3
        }
        public enum RequestStatus
        {
            Pending = 0,
            Approved = 1,
            Rejected = 2
        }
    }
}
