using Gym_Management_System.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gym_Management_System
{
    internal static class Program
    {
        // The main entry point for the application.
        [STAThread]
        static void Main()
        {
            // Enable visual styles for modern Windows UI components
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Launch the splash screen form as the initial window
            Application.Run(new splashForm());
        }
    }
}
