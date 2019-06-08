using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DependencyInjectionExample
{
    public class Logger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}