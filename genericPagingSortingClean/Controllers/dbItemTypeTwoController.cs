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
    public class dbItemTypeTwoController 
        : genericCrudListController<dbItemTypeTwo, genericModelCollection, genericCollectionItem<dbItemTypeTwo>, genericListModelFactory<dbItemTypeTwo, genericModelCollection, genericCollectionItem<dbItemTypeTwo>>>
    {

    }
}