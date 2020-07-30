using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JobPocDemo.Jobs
{
    public interface IJob
    {
        #region methods

        Task<IEnumerable<IJob>> RunAsync(CancellationToken cancellationToken = default);

        #endregion
    }
}
