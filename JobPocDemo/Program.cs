using System;
using System.Threading;
using System.Threading.Tasks;
using JobPocDemo.Data;
using static System.Console;

namespace JobPocDemo
{
    class Program
    {
        #region methods

        static async Task Main()
        {
            var cancellationTokenSource = new CancellationTokenSource();

            try
            {
                WriteLine("Select what you want[Default 1]:");
                WriteLine("[1] Demo DemoRunAtomic");
                WriteLine("[2] Demo DemoSequential And DemoExecuteFromQueue");

                var select = ReadLine();
                var jobService = new JobService();

                if (select is "2")
                {
                    await using var ensureDeletedContext = new JobDbContext();

                    await ensureDeletedContext.Database.EnsureDeletedAsync(cancellationTokenSource.Token)
                                              .ConfigureAwait(false);

                    await using var ensureCreatedContext = new JobDbContext();

                    await ensureCreatedContext.Database.EnsureCreatedAsync(cancellationTokenSource.Token)
                                              .ConfigureAwait(false);

                    await jobService.DemoSequentialAsync(cancellationTokenSource.Token)
                                    .ConfigureAwait(false);

                    await jobService.DemoExecuteFromQueueAsync(cancellationTokenSource.Token)
                                    .ConfigureAwait(false);
                }
                else
                {
                    await jobService.DemoRunAtomicAsync(cancellationTokenSource.Token)
                                    .ConfigureAwait(false);
                }

                WriteLine("Done");
            }
            catch (Exception exception)
            {
                WriteLine(exception.Message);
            }

            ReadKey();
        }

        #endregion
    }
}
