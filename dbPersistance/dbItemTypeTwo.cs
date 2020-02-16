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
    public class dbItemTypeTwo : IPocoEntity
    {
        [pagedListProperty("Id", 1, true, sortOrder.ASC, pagedPropertyType.orderAndFilter)]
        public int Id { get; set; }
        [pagedListProperty("Guid", 2, false, sortOrder.ASC, pagedPropertyType.orderAndFilter)]
        public string guid { get; set; }
        [pagedListProperty("Name", 3, false, sortOrder.ASC, pagedPropertyType.orderAndFilter)]
        public string name { get; set; }
        [pagedListProperty("Description", 4, false, sortOrder.ASC, pagedPropertyType.orderAndFilter)]
        public string description { get; set; }
        [pagedListProperty("Strring Value #1", 5, false, sortOrder.ASC, pagedPropertyType.orderAndFilter)]
        public string stringValueOne { get; set; }
        [pagedListProperty("Strring Value #2", 6, false, sortOrder.ASC, pagedPropertyType.orderAndFilter)]
        public string stringValueTwo { get; set; }
        [pagedListProperty("Decimal Value", 7, false, sortOrder.ASC, pagedPropertyType.orderAndFilter)]
        public decimal decValue { get; set; }
        [pagedListProperty("Int Value #1", 8, false, sortOrder.ASC, pagedPropertyType.orderAndFilter)]
        public int intVlue { get; set; }
        [pagedListProperty("Int Value #2", 9, false, sortOrder.ASC, pagedPropertyType.orderAndFilter)]
        public int invFieldTwo { get; set; }
        [pagedListProperty("INavigational", 10, false, sortOrder.ASC, pagedPropertyType.orderByOnly)]
        public virtual dbItemTypeOne dbItemTypeOne { get; set; }
    }
}
