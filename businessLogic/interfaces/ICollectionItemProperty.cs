using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace businessLogic.interfaces
{
    public interface ICollectionItemProperty
    {
        int number { get; set; }
        string display { get; set; }
        string value { get; set; }
    }
}
