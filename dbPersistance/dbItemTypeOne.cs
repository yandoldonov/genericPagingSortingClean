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
        [pagedListProperty("Id", 1, true, sortOrder.ASC)]
        public int Id { get; set; }
        [pagedListProperty("Guid", 2, false, sortOrder.ASC)]
        public string guid { get; set; }
        [pagedListProperty("Name", 3, false, sortOrder.ASC)]
        public string name { get; set; }
        [pagedListProperty("Description", 4, false, sortOrder.ASC)]
        public string description { get; set; }
        [pagedListProperty("Decimal Data", 5, false, sortOrder.ASC)]
        public decimal decimalData { get; set; }
        [pagedListProperty("Bolean Value", 6, false, sortOrder.ASC)]
        public bool boolvalue { get; set; }
    }
}
