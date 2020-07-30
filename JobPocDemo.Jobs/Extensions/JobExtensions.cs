using System.Threading;
using System.Threading.Tasks;

namespace JobPocDemo.Jobs.Extensions
{
    public static class JobExtensions
    {
        #region methods

        public static async Task FullRunAsync(this IJob job, CancellationToken cancellationToken = default)
        {
            foreach (var childJob in await job.RunAsync(cancellationToken)
                                              .ConfigureAwait(false))
            {
                await childJob.FullRunAsync(cancellationToken)
                              .ConfigureAwait(false);
            }
        }

        #endregion
    }
}
