using dbPersistance.atributes;
using dbPersistance.enums;
using dbPersistance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace dbPersistance
{
    public class dbItemTypeThree : IPocoEntity
    {
        [pagedListProperty("Id", 1, true, sortOrder.ASC, pagedPropertyType.orderAndFilter)]
        public int Id { get; set; }
        [pagedListProperty("Guid", 2, false, sortOrder.ASC, pagedPropertyType.orderAndFilter)]
        public string guid { get; set; }
        [pagedListProperty("Name", 3, false, sortOrder.ASC, pagedPropertyType.orderAndFilter)]
        public string name { get; set; }
        [pagedListProperty("Description", 4, false, sortOrder.ASC, pagedPropertyType.orderAndFilter)]
        public string description { get; set; }
        [pagedListProperty("Enum Type", 5, false, sortOrder.ASC, pagedPropertyType.orderAndFilter)]
        public dataTypeEnum dataTypeEnum { get; set; }
        [pagedListProperty("Date Time Type", 6, false, sortOrder.ASC, pagedPropertyType.orderAndFilter)]
        public DateTime? indicatedDate { get; set; }
        [pagedListProperty("INavigational", 7, false, sortOrder.ASC, pagedPropertyType.orderAndFilter)]
        public virtual dbItemTypeOne dbItemTypeOne { get; set; }

        public string pagedListDescription => name;
        public string selectListTextDescription => name;
    }

}
