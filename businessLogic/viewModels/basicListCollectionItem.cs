using businessLogic.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace businessLogic.viewModels
{
    public class basicListCollectionItem : ICollectionItem
    {
        public basicListCollectionItem()
        {
            properties = new List<ICollectionItemProperty>();
        }
        public IList<ICollectionItemProperty> properties { get; set; }


        public int id { get; set; }
        public string guid { get; set; }
        public decimal intData { get; set; }
        public decimal decimalData { get; set; }
        public bool boolData { get; set; }
        public string genericTypeName { get; set; }
        public int listablePropertiesCount { get; set; }
    }
}
