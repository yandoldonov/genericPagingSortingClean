using businessLogic.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace businessLogic.viewModels
{
    public class collectionItemProperty : ICollectionItemProperty
    {
        public int number { get; set; }
        public string display { get; set; }
        public string value { get; set; }
    }
}
