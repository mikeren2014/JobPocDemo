using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JobPocDemo.Jobs.Extensions;
using static JobPocDemo.Jobs.Helpers.TestHelper;

namespace JobPocDemo.Jobs
{
    public class CopyAccountJob : IJob
    {
        #region properties

        public int AccountId { get; set; }

        #endregion

        #region methods

        public async Task<IEnumerable<IJob>> RunAsync(CancellationToken cancellationToken = default)
        {
            await DoSomeWorkAsync(1, $"Copying Account info: AccountId {AccountId}", cancellationToken)
                .ConfigureAwait(false);

            // Run Atomic
            await new CopyAccountSettingsJob
                  {
                      AccountId = AccountId
                  }.FullRunAsync(cancellationToken)
                   .ConfigureAwait(false);

            var catalogId = GetCatalogIds();

            // Run Sequential
            return new IJob[]
                   {
                       new CopyCatalogJob
                       {
                           CatalogId = catalogId
                       }
                   };
        }

        static int GetCatalogIds() => 1000;

        #endregion
    }
}
