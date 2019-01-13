using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHostExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Startup plugin = new Startup();
            Console.WriteLine("Press Enter to quit.");
            Console.ReadLine();
        }
    }
}
