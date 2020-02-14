using businessLogic.interfaces;
using dbPersistance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace businessLogic.viewModels
{
    public class genericCollectionItem<TDbItem> : ICollectionItem where TDbItem : class, IPocoEntity
    {
        public int id { get; set; }
        public string guid { get; set; }
        public decimal intData { get; set; }
        public decimal decimalData { get; set; }
        public bool boolData { get; set; }
        public string genericTypeName { get; set; }
        public int listablePropertiesCount { get; set; }

        public genericCollectionItem() 
        {
            properties = new List<ICollectionItemProperty>();
        }
        public IList<ICollectionItemProperty> properties { get; set; }

        public static ICollectionItem buildFromDbInstance(TDbItem _TDbItem)
        {
            ICollectionItem _basicListCollectionItem = new basicListCollectionItem()
            {
                id = _TDbItem.Id,
                guid = _TDbItem.guid,
                genericTypeName = typeof(TDbItem).Name,
                listablePropertiesCount = dbPersistance.extentionHelpers.typeExtentions.countPageListProperties(typeof(TDbItem))
            };

            foreach(var item in dbPersistance.extentionHelpers.typeExtentions.getPageListProperties(typeof(TDbItem)))
            {
                _basicListCollectionItem.properties.Add(new collectionItemProperty()
                {
                    display = dbPersistance.extentionHelpers.typeExtentions.getPagedListPropertyAttribute(item).getDisplay(),
                    value = item.GetValue(_TDbItem).ToString(),
                    number = dbPersistance.extentionHelpers.typeExtentions.getPagedListPropertyAttribute(item).getPosition(),
                });
            }

            return _basicListCollectionItem;
        }
    }
}
