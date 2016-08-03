using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Async
{
    class Program
    {
        [STAThreadAttribute]
        static void Main(string[] args)
        {
            Progress.Run();

            //TaskbasedAsyncPattern.Run();
            //TaskbasedAsyncPattern.RunAsync();

            TaskbasedAsyncPattern.GetHostEntryAsync("localhost");
            TaskbasedAsyncPattern.GetHostEntryAsync2("localhost");

            Console.WriteLine("done");
            Console.ReadKey();
        }

       
    }
}
