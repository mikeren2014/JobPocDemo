using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static System.Linq.Enumerable;
using static JobPocDemo.Jobs.Helpers.TestHelper;

namespace JobPocDemo.Jobs
{
    public class CopyPriceJob : IJob
    {
        #region properties

        public int AccountId { get; set; }
        public string Sku { get; set; } = default!;

        #endregion

        #region methods

        public async Task<IEnumerable<IJob>> RunAsync(CancellationToken cancellationToken = default)
        {
            await DoSomeWorkAsync(1, $"Copying Price sku = {Sku}...", cancellationToken)
                .ConfigureAwait(false);

            return Empty<IJob>();
        }

        #endregion
    }
}
