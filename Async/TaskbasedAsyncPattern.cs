using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Async
{
    public static class TaskbasedAsyncPattern
    {
        public static Task<IPHostEntry> GetHostEntryAsync(string hostNameOrAddress)
        {
            TaskCompletionSource<IPHostEntry> tcs = new TaskCompletionSource<IPHostEntry>();

            Dns.BeginGetHostEntry(hostNameOrAddress, asyncResult =>
            {
                try
                {
                    IPHostEntry result = Dns.EndGetHostEntry(asyncResult);
                    tcs.SetResult(result);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            },
            null);

            return tcs.Task;
        }

        public static Task<IPHostEntry> GetHostEntryAsync2(string hostNameOrAddress)
        {
            return Task<IPHostEntry>.Factory.FromAsync(
                Dns.BeginGetHostEntry,
                Dns.EndGetHostEntry,
                hostNameOrAddress,
                null);
        }

        public static void Run()
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

        public async static void RunAsync()
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
