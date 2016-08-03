using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async
{
    public static class Combinator
    {
        public static async Task<T> WithTimeOut<T>(Task<T> task, int time)
        {
            Task delayTask = Task.Delay(time);
            Task firstToFinish = await Task.WhenAny(task, delayTask);

            if (firstToFinish == delayTask)
            {
                await task.ContinueWith(HandleException);
                throw new TimeoutException();
            }

            return await task;
        }

        private static void HandleException<T>(Task<T> task)
        {
            if (task.Exception != null)
            {
                Console.WriteLine(task.Exception);
            }
        }
    }
}
