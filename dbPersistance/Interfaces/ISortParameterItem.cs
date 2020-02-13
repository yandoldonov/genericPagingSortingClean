using dbPersistance.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbPersistance.Interfaces
{
    public interface ISortParameterItem
    {
        int number { get; set; }
        sortOrder sortOrder { get; set; }
        string colDisplay { get; set; }
        string colValue { get; set; }
    }
}
