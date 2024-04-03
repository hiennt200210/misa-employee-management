using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Domain
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        /// <summary>
        /// Gọi vào Connection để lấy ra connection
        /// </summary>
        DbConnection Connection { get; }

        /// <summary>
        /// Gọi vào Transaction để lấy ra transaction
        /// </summary>
        DbTransaction Transaction { get; }

        void BeginTransaction();

        Task BeginTransactionAsync();

        void CommitTransaction();

        Task CommitTransactionAsync();

        void RollbackTransaction();

        Task RollbackTransactionAsync();
    }
}
