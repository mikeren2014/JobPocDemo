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

        public async Task<IEnumerable<IJob>> GetAsync(CancellationToken cancellationToken = default)
        {
            await DoSomeWorkAsync(1, $"Copying product info CatalogProductId={CatalogProductId}...", cancellationToken)
                .ConfigureAwait(false);

            return new IJob[]
                   {
                       new CopyPriceJob
                       {
                           Sku = NewGuid()
                               .ToString()
                       }
                   };
        }

        public async Task RunAsync(CancellationToken cancellationToken = default)
        {
            await DoSomeWorkAsync(1, $"Copying product info CatalogProductId={CatalogProductId}...", cancellationToken)
                .ConfigureAwait(false);

            await new CopyPriceJob
                  {
                      Sku = NewGuid()
                          .ToString()
                  }.RunAsync(cancellationToken)
                   .ConfigureAwait(false);
        }

        #endregion
    }
}
