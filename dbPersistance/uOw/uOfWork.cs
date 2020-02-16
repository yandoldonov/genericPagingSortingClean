using dbPersistance.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbPersistance.uOw
{
    public class uOfWork : IDisposable
    {
        private dbContext context;

        public uOfWork()
        {
            this.context = new dbContext();

        }
        public uOfWork(dbContext Context)
        {
            this.context = Context;
        }

        private entityRepository<dbItemTypeOne> _dbItemTypeOne; 
        private entityRepository<dbItemTypeTwo> _dbItemTypeTwo;
        private entityRepository<dbItemTypeThree> _dbItemTypeThree;

        public entityRepository<dbItemTypeOne> dbItemTypeOne
        {
            get
            {
                if (this._dbItemTypeOne == null)
                {
                    this._dbItemTypeOne = new entityRepository<dbItemTypeOne>(context);
                }
                return _dbItemTypeOne;
            }
        }
        public entityRepository<dbItemTypeTwo> dbItemTypeTwo
        {
            get
            {
                if (this._dbItemTypeTwo == null)
                {
                    this._dbItemTypeTwo = new entityRepository<dbItemTypeTwo>(context);
                }
                return _dbItemTypeTwo;
            }
        }
        public entityRepository<dbItemTypeThree> dbItemTypeThree
        {
            get
            {
                if (this._dbItemTypeThree == null)
                {
                    this._dbItemTypeThree = new entityRepository<dbItemTypeThree>(context);
                }
                return _dbItemTypeThree;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
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
