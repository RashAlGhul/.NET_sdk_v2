using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSMA.MobileConnect.Test
{
    /// <summary>
    /// Simple class for test, which use Log class.
    /// Should be initialized in setup method or test class 
    /// and clear Log after use it (Log.RegisterLogger(null);) 
    /// </summary>
    internal class TestLogger : ILogger
    {
        public void Info(string message)
        {
            Console.WriteLine($"Info: {message}");
        }

        public void Debug(string message)
        {
            Console.WriteLine($"Debug: {message}");
        }

        public void Warning(string message)
        {
            Console.WriteLine($"Warning: {message}");
        }

        public void Error(string message, Exception ex)
        {
            Console.WriteLine($"Error: {message}");
        }

        public void Fatal(string message, Exception ex)
        {
            Console.WriteLine($"Fatal: {message}");
        }
    }
}
