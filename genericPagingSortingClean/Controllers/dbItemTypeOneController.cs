using businessLogic.viewModelFactories;
using businessLogic.viewModels;
using dbPersistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace genericPagingSortingClean.Controllers
{
    public class dbItemTypeOneController : genericCrudListController<dbItemTypeOne, genericModelCollection, genericCollectionItem<dbItemTypeOne>, genericListModelFactory<dbItemTypeOne, genericModelCollection, genericCollectionItem<dbItemTypeOne>>>
    {

    }
}