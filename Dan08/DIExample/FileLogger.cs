using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIExample
{
    class FileLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(
                "Writing to file... Content is : " + message
            );
        }
    }
}
