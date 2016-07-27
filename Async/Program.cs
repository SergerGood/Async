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
            Run();
            RunAsync();

            Console.WriteLine("done");
            Console.ReadKey();
        }

        private static void Run()
        {
            if (GetUserDialogResult())
            {
                Console.WriteLine("true");
            }
        }

        private static bool GetUserDialogResult()
        {
            OpenFileDialog dialog = new OpenFileDialog();

            return dialog.ShowDialog() == DialogResult.OK;
        }

        private async static void RunAsync()
        {
            if (await GetUserDialogResultAsync())
            {
                Console.WriteLine("true");
            }
        }

        private static Task<bool> GetUserDialogResultAsync()
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.FileOk += delegate 
            {
                tcs.SetResult(true);
            };

            dialog.ShowDialog();

            return tcs.Task;
        }
    }
}
