using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Gym_Management_System
{
    internal class dbConnect
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();  

        public string connection()
        {
            //string con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\ECS\Sem 5 (Year of Study 3 )\BECS 31242 - Visual Programming\Projects\Project02 - Gym MS\Gym Management System\Gym Management System\dbGymMS.mdf"";Integrated Security=True";
            //string con = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=dbGymMS;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;";
            string con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\ECS\Sem 5 (Year of Study 3 )\BECS 31242 - Visual Programming\Projects\Project02 - Gym MS\Gym Management System\Gym Management System\dbGymMS.mdf"";Integrated Security=True";
            return con;
        }
    }
}
