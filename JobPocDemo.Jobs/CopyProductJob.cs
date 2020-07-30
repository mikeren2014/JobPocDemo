using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static System.Guid;
using static JobPocDemo.Jobs.Helpers.TestHelper;

namespace JobPocDemo.Jobs
{
    public class CopyProductJob : IJob
    {
        #region properties

        public int CatalogProductId { get; set; }

        #endregion

        #region methods

        public async Task<IEnumerable<IJob>> RunAsync(CancellationToken cancellationToken = default)
        {
            await DoSomeWorkAsync(1, $"Copying product info CatalogProductId={CatalogProductId}...", cancellationToken)
                .ConfigureAwait(false);

            // Run Sequential
            return new IJob[]
                   {
                       new CopyPriceJob
                       {
                           Sku = NewGuid()
                               .ToString()
                       }
                   };
        }

        #endregion
    }
}
