using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JobPocDemo.Jobs
{
    public interface IJob
    {
        #region methods

        Task<IEnumerable<IJob>> GetAsync(CancellationToken cancellationToken = default);
        Task RunAsync(CancellationToken cancellationToken = default);

        #endregion
    }
}
