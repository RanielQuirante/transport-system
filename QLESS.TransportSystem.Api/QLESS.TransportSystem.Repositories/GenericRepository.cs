using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLESS.TransportSystem.Repositories
{
    public abstract class GenericRepository<TEntity>
    {
        public abstract Task<bool> Delete(TEntity tModel);
        public abstract Task<IEnumerable<TEntity>> Get(TEntity tModel);
        public abstract Task<int> Insert(TEntity tModel);
        public abstract Task<bool> Update(TEntity tModel);
    }
}
