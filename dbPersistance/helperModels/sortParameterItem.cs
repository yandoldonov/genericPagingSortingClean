using dbPersistance.enums;
using dbPersistance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbPersistance.helperModels
{
    public class sortParameterItem : ISortParameterItem
    {
        public int number { get; set; }
        public sortOrder sortOrder { get; set; }
        public string colDisplay { get; set; }
        public string colValue { get; set; }
    }
}
