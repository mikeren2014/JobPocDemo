using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static JobPocDemo.Jobs.Helpers.TestHelper;

namespace JobPocDemo.Jobs
{
    public class CopyCatalogJob : IJob
    {
        #region properties

        public int CatalogId { get; set; }

        #endregion

        #region methods

        public async Task<IEnumerable<IJob>> GetAsync(CancellationToken cancellationToken = default)
        {
            await DoSomeWorkAsync(1, $"Copying catalog info catalogId:{CatalogId}", cancellationToken)
                .ConfigureAwait(false);

            // Run Sequential
            return GetCatalogComponentIds(CatalogId)
                   .Select(_ => new CopyComponentJob
                                {
                                    CatalogComponentId = _
                                })
                   .Cast<IJob>()
                   .Concat(GetCatalogProductIds(CatalogId)
                               .Select(_ => new CopyProductJob
                                            {
                                                CatalogProductId = _
                                            }));
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            await DoSomeWorkAsync(1, $"Copying catalog info catalogId:{CatalogId}", cancellationToken)
                .ConfigureAwait(false);

            await Task.WhenAll(GetCatalogComponentIds(CatalogId)
                                   .Select(_ => new CopyComponentJob
                                                {
                                                    CatalogComponentId = _
                                                }.RunAsync(cancellationToken)));

            await Task.WhenAll(GetCatalogProductIds(CatalogId)
                                   .Select(_ => new CopyProductJob
                                                {
                                                    CatalogProductId = _
                                                }.RunAsync(cancellationToken)))
                      .ConfigureAwait(false);
        }

        static IEnumerable<int> GetCatalogComponentIds(int catalogId) => GetCatalogProductIds(catalogId);

        // ReSharper disable once UnusedParameter.Local
        static IEnumerable<int> GetCatalogProductIds(int catalogId) =>
            new[]
            {
                GetRandom(),
                GetRandom()
            };

        #endregion
    }
}
