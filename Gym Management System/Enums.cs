using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace Gym_Management_System
{
    // Represents the current status of a user's account
    public enum UserStatus
    {
        Pending = 0,
        Active = 1,
        Hold = 2,
        Suspended = 3
    }

    // Represents the status of store item orders
    public enum OrderStatus
    {
        Hold = 0,
        Completed = 1,
        Reversed = 2
    }

    // Defines the different roles a user can have in the system
    public enum UserRole
    {
        Admin = 1,  
        Staff = 2,
        Coach = 3,  
        Member = 4
    }

    // Defines accepted payment methods
    public enum PaymentMethod
    {
        Cash = 0,
        CreditCard = 1,
        DebitCard = 2,
        MobilePayment = 3
    }

    // Represents the status of requests (like schedule or leave requests)
    public enum RequestStatus
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2
    }

    // Categories for items in the gym
    public enum Categories 
    {
        GymItem,
        Memberships
    }
}