using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JobPocDemo.Data;
using JobPocDemo.Jobs;
using JobPocDemo.Jobs.Extensions;
using Taylor.UFP.XCore.Json.Extensions;
using static System.Console;

namespace JobPocDemo
{
    public class JobService
    {
        #region methods

        public async Task DemoExecuteFromQueueAsync(CancellationToken cancellationToken = default)
        {
            var dequeuedJob = await DeQueueAsync(cancellationToken)
                                  .ConfigureAwait(false);

            if (dequeuedJob is null)
                return;

            foreach (var job in await dequeuedJob.RunAsync(cancellationToken)
                                                 .ConfigureAwait(false))
            {
                await SaveJobAsync(job, cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        public Task DemoRunAtomicAsync(CancellationToken cancellationToken = default) =>
            new CopyAccountJob
            {
                AccountId = 123
            }.FullRunAsync(cancellationToken);

        public async Task DemoSequentialAsync(CancellationToken cancellationToken = default)
        {
            foreach (var job in await new CopyAccountJob
                                      {
                                          AccountId = 123
                                      }.RunAsync(cancellationToken)
                                       .ConfigureAwait(false))
            {
                await SaveJobAsync(job, cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        static async Task<IJob?> DeQueueAsync(CancellationToken cancellationToken = default)
        {
            await using var dbContext = new JobDbContext();

            var firstJob = dbContext.JobItems.OrderBy(_ => _.Id)
                                    .FirstOrDefault();

            if (firstJob is null)
                return null;

            dbContext.JobItems.Remove(firstJob);

            await dbContext.SaveChangesAsync(cancellationToken)
                           .ConfigureAwait(false);

            return firstJob.Content.To<IJob>(new JobConverter(firstJob.Type));
        }

        static async Task SaveJobAsync(IJob job, CancellationToken cancellationToken = default)
        {
            var jobItem = new JobItem
                          {
                              Content = job.ToJson(),
                              Type = job.GetType()
                                        .Name
                          };

            WriteLine();
            WriteLine($"Queue Job: type = {jobItem.Type}");
            WriteLine(jobItem.Content);

            await using var dbContext = new JobDbContext();

            await dbContext.JobItems.AddAsync(jobItem, cancellationToken)
                           .ConfigureAwait(false);

            await dbContext.SaveChangesAsync(cancellationToken)
                           .ConfigureAwait(false);
        }

        #endregion
    }
}
