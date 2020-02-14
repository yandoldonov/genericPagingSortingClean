using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbPersistance.Interfaces
{
    public interface IPocoEntity
    {
        string guid
        {
            get;
        }

        string description
        {
            get;
        }

        int Id
        {
            get;
        }

        string name
        {
            get;
        }
    }
}
