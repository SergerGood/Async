using System;
using System.Threading;
using System.Threading.Tasks;

namespace Async
{
    public static class Progress
    {
        public static async void Run()
        {
            CancellationToken ct = new CancellationToken();
            var addresses = new string[] { "a", "b", "c" };

            var progress = new Progress<int>(step =>
            {
                Console.WriteLine($"{DateTime.Now.ToString("s.fff")}: step {step} is notify");
            });

            await DownloadDataTaskAsync(addresses, ct, progress);

            Console.WriteLine("Progress done");
        }

        public static async Task DownloadDataTaskAsync(string[] addresses,
            CancellationToken cancellationToken,
            IProgress<int> progress)
        {

            for (int i = 0; i <= addresses.Length - 1; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                await Do(addresses[i]);

                progress.Report(i+1);
            }

        }

        private static async Task Do(string x)
        {
            Console.WriteLine($"{DateTime.Now.ToString("s.fff")}: adress {x} start");

            Task delayTask = Task.Delay(1000)
                .ContinueWith(_ =>
                {
                    Console.WriteLine($"{DateTime.Now.ToString("s.fff")}: adress: {x} stop");
                });

            await delayTask;
        }
    }
}
