using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static JobPocDemo.Jobs.Helpers.TestHelper;

namespace JobPocDemo.Jobs
{
    public class CopyComponentJob : IJob
    {
        #region properties

        public int CatalogComponentId { get; set; }

        #endregion

        #region methods

        public async Task<IEnumerable<IJob>> RunAsync(CancellationToken cancellationToken = default)
        {
            await DoSomeWorkAsync(1, $"Coping Components: CatalogComponentId: {CatalogComponentId}", cancellationToken)
                .ConfigureAwait(false);

            return Enumerable.Empty<IJob>();
        }

        #endregion
    }
}
