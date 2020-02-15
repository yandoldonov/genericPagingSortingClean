using System.Data.Entity;

namespace dbPersistance
{
    public class dbContext : DbContext
    {
        public dbContext()
            : base("pagingSortingDb")
        {
        }

        public virtual DbSet<dbItemTypeOne> dbItemTypeOne { get; set; } 
        public virtual DbSet<dbItemTypeTwo> dbItemTypeTwo { get; set; }
        public virtual DbSet<dbItemTypeThree> dbItemTypeThree { get; set; }
    }
}
