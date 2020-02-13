using dbPersistance.Interfaces;
using dbPersistance.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbPersistance.uOw
{
    public class unitOfWork<TPocoEntity> : IUnitOfWork<TPocoEntity>, IDisposable where TPocoEntity : class, IPocoEntity
    {

        private dbContext context;
        private IEntityRepository<TPocoEntity> _repo;

        public unitOfWork()
        {
            this.context = new dbContext();

        }
        public unitOfWork(dbContext Context)
        {
            this.context = Context;
        }

        public IEntityRepository<TPocoEntity> repository
        {
            get
            {
                if (this._repo == null)
                {
                    this._repo = new repository<TPocoEntity>(context);
                }
                return _repo;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }



        #region Dispose
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
