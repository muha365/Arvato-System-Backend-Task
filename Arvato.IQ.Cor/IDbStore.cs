using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.IQ.Cor
{
    /// <summary>
    /// interface that exposes basic database unit of work api
    /// </summary>
    public interface IDbStore : IDisposable
    {

        /// <summary>
        /// Save changes Asynchronously 
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Save changes Synchronously
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
    }
}
