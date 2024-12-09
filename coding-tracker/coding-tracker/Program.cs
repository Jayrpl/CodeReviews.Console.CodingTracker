using System;
using System.Configuration;
using System.Collections.Specialized;

namespace coding_tracker
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller controller = new Controller();
            controller.InitialiseDatabase();
            View.GetUserSelection();
        }
    }
}