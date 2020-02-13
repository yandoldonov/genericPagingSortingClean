using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbPersistance.Interfaces
{
    public interface IUnitOfWork<TEntity> : IDisposable where TEntity : class, IPocoEntity
    {
        IEntityRepository<TEntity> repository { get; }
    }
}
