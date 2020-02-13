using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace businessLogic.interfaces
{
    public interface ICollectionItem
    {
        int id { get; set; }
        string guid { get; set; }
        decimal intData { get; set; }
        decimal decimalData { get; set; }
        bool boolData { get; set; }
        string genericTypeName { get; set; }
        int listablePropertiesCount { get; }

        IList<ICollectionItemProperty> properties { get; set; }
    }
}
