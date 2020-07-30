using System;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;
using static System.Threading.Tasks.Task;

namespace JobPocDemo.Jobs.Helpers
{
    public static class TestHelper
    {
        #region methods

        public static async Task DoSomeWorkAsync(int seconds, string message, CancellationToken cancellationToken = default)
        {
            await Delay(seconds * 1000, cancellationToken)
                .ConfigureAwait(false);

            WriteLine();
            WriteLine(message);
        }

        public static int GetRandom()
        {
            var random = new Random();

            return random.Next(100, 999);
        }

        #endregion
    }
}
