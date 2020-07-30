using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static System.Linq.Enumerable;
using static JobPocDemo.Jobs.Helpers.TestHelper;

namespace JobPocDemo.Jobs
{
    public class CopyAccountSettingsJob : IJob
    {
        #region properties

        public int AccountId { get; set; }

        #endregion

        #region methods

        public async Task<IEnumerable<IJob>> RunAsync(CancellationToken cancellationToken = default)
        {
            await DoSomeWorkAsync(1, $"Coping AccountSettings: AccountId: {AccountId}", cancellationToken)
                .ConfigureAwait(false);

            return Empty<IJob>();
        }

        #endregion
    }
}
