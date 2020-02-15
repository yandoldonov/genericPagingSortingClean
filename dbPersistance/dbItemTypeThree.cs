using dbPersistance.atributes;
using dbPersistance.enums;
using dbPersistance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbPersistance
{
    public class dbItemTypeThree : IPocoEntity
    {
        [pagedListProperty("Id", 1, true, sortOrder.ASC)]
        public int Id { get; set; }
        [pagedListProperty("Guid", 2, false, sortOrder.ASC)]
        public string guid { get; set; }
        [pagedListProperty("Name", 3, false, sortOrder.ASC)]
        public string name { get; set; }
        [pagedListProperty("Description", 4, false, sortOrder.ASC)]
        public string description { get; set; }
        [pagedListProperty("Enum Type", 5, false, sortOrder.ASC)]
        public dataTypeEnum dataTypeEnum { get; set; }
        [pagedListProperty("Date Time Type", 6, false, sortOrder.ASC)]
        public DateTime? indicatedDate { get; set; }
    }

}
