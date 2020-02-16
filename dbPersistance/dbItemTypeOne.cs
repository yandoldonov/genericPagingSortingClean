using dbPersistance.atributes;
using dbPersistance.enums;
using dbPersistance.helperModels;
using dbPersistance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace dbPersistance
{
    public class dbItemTypeOne : IPocoEntity
    {
        public dbItemTypeOne()
        {
            dbItemTypeTwos = new HashSet<dbItemTypeTwo>();
            dbItemTypeThrees = new HashSet<dbItemTypeThree>();
        }

        [pagedListProperty("Id", 1, true, sortOrder.ASC, pagedPropertyType.orderAndFilter)]
        public int Id { get; set; }
        [pagedListProperty("Guid", 2, false, sortOrder.ASC, pagedPropertyType.orderAndFilter)]
        public string guid { get; set; }
        [pagedListProperty("Name", 3, false, sortOrder.ASC, pagedPropertyType.orderAndFilter)]
        public string name { get; set; }
        [pagedListProperty("Description", 4, false, sortOrder.ASC, pagedPropertyType.orderAndFilter)]
        public string description { get; set; }
        [pagedListProperty("Decimal Data", 5, false, sortOrder.ASC, pagedPropertyType.orderAndFilter)]
        public decimal decimalData { get; set; }
        [pagedListProperty("Bolean Value", 6, false, sortOrder.ASC, pagedPropertyType.orderAndFilter)]
        public bool boolvalue { get; set; }
        public virtual ICollection<dbItemTypeTwo> dbItemTypeTwos { get; set; }
        public virtual ICollection<dbItemTypeThree> dbItemTypeThrees { get; set; }

    }
}
