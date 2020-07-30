using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static JobPocDemo.Jobs.Helpers.TestHelper;

namespace JobPocDemo.Jobs
{
    public class CopyAccountJob : IJob
    {
        #region properties

        public int AccountId { get; set; }

        #endregion

        #region methods

        public async Task<IEnumerable<IJob>> GetAsync(CancellationToken cancellationToken = default)
        {
            await DoSomeWorkAsync(1, $"Copying Account info: AccountId {AccountId}", cancellationToken)
                .ConfigureAwait(false);

            return new[]
                   {
                       new CopyAccountSettingsJob
                       {
                           AccountId = AccountId
                       } as IJob,
                       new CopyCatalogJob
                       {
                           CatalogId = GetCatalogId()
                       }
                   };
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            await DoSomeWorkAsync(1, $"Copying Account info: AccountId {AccountId}", cancellationToken)
                .ConfigureAwait(false);

            await new CopyAccountSettingsJob
                  {
                      AccountId = AccountId
                  }.RunAsync(cancellationToken)
                   .ConfigureAwait(false);

            await new CopyCatalogJob
                  {
                      CatalogId = GetCatalogId()
                  }.RunAsync(cancellationToken)
                   .ConfigureAwait(false);
        }

        static int GetCatalogId() => 1000;

        #endregion
    }
}
