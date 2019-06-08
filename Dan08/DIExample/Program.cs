using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIExample
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger consoleLogger = new ConsoleLogger();
            ILogger fileLogger = new FileLogger();
            User user1 = new User("Marko", "mm@mail", consoleLogger);
            User user2 = new User("", "", fileLogger);

            user1.IntroduceYourself();
            user2.IntroduceYourself();
        }
    }

}
