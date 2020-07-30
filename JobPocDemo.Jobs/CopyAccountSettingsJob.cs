using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static JobPocDemo.Jobs.Helpers.TestHelper;
using static System.Linq.Enumerable;

namespace JobPocDemo.Jobs
{
    public class CopyAccountSettingsJob : IJob
    {
        #region properties

        public int AccountId { get; set; }

        #endregion

        #region methods

        public async Task<IEnumerable<IJob>> GetAsync(CancellationToken cancellationToken = default)
        {
            await DoSomeWorkAsync(1, $"Coping AccountSettings: AccountId: {AccountId}", cancellationToken)
                .ConfigureAwait(false);

            return Empty<IJob>();
        }

        public Task RunAsync(CancellationToken cancellationToken = default) => DoSomeWorkAsync(1, $"Coping AccountSettings: AccountId: {AccountId}", cancellationToken);

        #endregion
    }
}
