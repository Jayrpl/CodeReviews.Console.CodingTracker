using System;
using System.Configuration;
using System.Collections.Specialized;

namespace ConConfig
{
    class Program
    {
        static void Main(string[] args)
        {
            string sAttr;

            // Read a particular key from the config file 
            sAttr = ConfigurationManager.AppSettings.Get("Key0");
            Console.WriteLine("The value of Key0: " + sAttr);

            // Read all the keys from the config file
            NameValueCollection sAll;
            sAll = ConfigurationManager.AppSettings;

            foreach (string s in sAll.AllKeys)
                Console.WriteLine("Key: " + s + " Value: " + sAll.Get(s));

            Console.ReadLine();
        }
    }
}