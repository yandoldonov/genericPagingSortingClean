using businessLogic.interfaces;
using businessLogic.viewModelFactories;
using businessLogic.viewModels;
using dbPersistance.enums;
using dbPersistance.extentionHelpers;
using dbPersistance.helperModels;
using dbPersistance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace genericPagingSortingClean.Controllers
{
    public class genericCrudListController<TDataItem, TListModel, TViewModel, TListModelFactory>
        : baseController where TDataItem : class, IPocoEntity where TListModel : class, IListModelCollection where TViewModel : class, ICollectionItem where TListModelFactory : class, IListModelFactory<TDataItem, TListModel>
    {

        public ActionResult pagedList()
        {
            using (IListModelFactory<TDataItem, TListModel> factory = new genericListModelFactory<TDataItem, TListModel, TViewModel>())
            {
                return View("~/Views/genericCrudList/pagedList.cshtml", factory.getDataList(this.title, "pagedListPartial", 10, 1));
            }
        }

        public ActionResult pagedListPartial 
           (string orderBy,
           sortOrder? sortOrder,
           int? pageNumber, 
           int? takeCount,
           string selectedProperty,
           string queryString,
           string queryOptions)
        {
            using (IListModelFactory<TDataItem, TListModel> factory = new genericListModelFactory<TDataItem, TListModel, TViewModel>())
            {
                if (pageNumber != null && pageNumber is int && takeCount != null && takeCount is int)
                {
                    if (string.IsNullOrEmpty(selectedProperty))
                    {
                        if (string.IsNullOrEmpty(queryString)) queryString = string.Empty;

                        if (string.IsNullOrEmpty(orderBy))
                            return PartialView("~/Views/genericCrudList/_pagedListPartial.cshtml", factory.getDataList(base.title, "pagedListPartial", (int)takeCount, (int)pageNumber));
                        else return PartialView("~/Views/genericCrudList/_pagedListPartial.cshtml", factory.getDataList(base.title, "pagedListPartial", orderBy, (sortOrder)sortOrder, (int)takeCount, (int)pageNumber));
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(orderBy))
                            return PartialView("~/Views/genericCrudList/_pagedListPartial.cshtml", factory.getDataList(base.title, "pagedListPartial", (int)takeCount, (int)pageNumber));
                        else return PartialView("~/Views/genericCrudList/_pagedListPartial.cshtml",
                            factory.getDataList(base.title, "pagedListPartial", orderBy, (sortOrder)sortOrder, (int)takeCount, (int)pageNumber, selectedProperty, queryString, queryOptions));
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(selectedProperty))
                    {
                        if (string.IsNullOrEmpty(queryString)) queryString = string.Empty;

                        if (string.IsNullOrEmpty(orderBy))
                            return PartialView("~/Views/genericCrudList/_pagedListPartial.cshtml", factory.getDataList(base.title, "pagedListPartial", 10, 1));
                        else return PartialView("~/Views/genericCrudList/_pagedListPartial.cshtml", factory.getDataList(base.title, "pagedListPartial", orderBy, (sortOrder)sortOrder, 10, 1));
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(orderBy))
                            return PartialView("~/Views/genericCrudList/_pagedListPartial.cshtml", factory.getDataList(base.title, "pagedListPartial", 10, 1));
                        else return PartialView("~/Views/genericCrudList/_pagedListPartial.cshtml", factory.getDataList(base.title, "pagedListPartial", orderBy, (sortOrder)sortOrder, 10, 1, selectedProperty, queryString, queryOptions));
                    }
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult pullPagedListFormListData(filterFormModel model)
        {
            using (IListModelFactory<TDataItem, TListModel> factory = new genericListModelFactory<TDataItem, TListModel, TViewModel>())
            {
                return PartialView("~/Views/genericCrudList/_pagedListPartial.cshtml", factory.getDataList(base.title, "pagedListPartial", model.orderBy, sortOrder.ASC, 10, 1, model.selectedProperty, model.queryString, model.selectedQuetyOption));
            }
        }

        [HttpPost]
        public ActionResult updateQueryVariantsFromDatabase(string selectedProperty)
        {
            if (!string.IsNullOrEmpty(selectedProperty))
            {               
                SelectList xList = pagedListExtentionHelpers.queryVariantsSelectList(typeof(TDataItem), selectedProperty);
                return Json(xList);
            }
            else
            {
                List<selectlistItemHelper> emptyList = new List<selectlistItemHelper>
                {
                new selectlistItemHelper { name = "NO ITEMS AVAILABLE", guid = "XXXXXXXX" }
                };

                return Json(new SelectList(emptyList, "guid", "name"));
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult updatePagedListTakeCount(filterFormModel model)
        {
            using (IListModelFactory<TDataItem, TListModel> factory = new genericListModelFactory<TDataItem, TListModel, TViewModel>())
            {
                if (model.currentPage > 0 && model.takeCount > 0)
                {
                    if (string.IsNullOrEmpty(model.selectedProperty) || string.IsNullOrEmpty(model.queryString))
                    {
                        if (string.IsNullOrEmpty(model.orderBy))
                            return PartialView("~/Views/genericCrudList/_pagedListPartial.cshtml", factory.getDataList(base.title, "pagedListPartial", model.takeCount, model.currentPage));
                        else return PartialView("~/Views/genericCrudList/_pagedListPartial.cshtml", factory.getDataList(base.title, "pagedListPartial", model.orderBy, model.currentSortOrder, model.takeCount, model.currentPage));
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(model.orderBy))
                            return PartialView("~/Views/genericCrudList/_pagedListPartial.cshtml", factory.getDataList(base.title, "pagedListPartial", model.takeCount, model.currentPage));
                        else return PartialView("~/Views/genericCrudList/_pagedListPartial.cshtml",
                            factory.getDataList(base.title, "pagedListPartial", model.orderBy, model.currentSortOrder, model.takeCount, model.currentPage, model.selectedProperty, model.queryString, model.selectedQuetyOption));
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(model.selectedProperty) || string.IsNullOrEmpty(model.queryString))
                    {
                        if (string.IsNullOrEmpty(model.orderBy))
                            return View(factory.getDataList(base.title, "pagedListPartial", 10, 1));
                        else return PartialView("~/Views/genericCrudList/_pagedListPartial.cshtml", factory.getDataList(base.title, "pagedListPartial", model.orderBy, model.currentSortOrder, 10, 1));
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(model.orderBy))
                            return View(factory.getDataList(base.title, "pagedListPartial", 10, 1));
                        else return PartialView("~/Views/genericCrudList/_pagedListPartial.cshtml", factory.getDataList(base.title, "pagedListPartial", model.orderBy, model.currentSortOrder, 10, 1, model.selectedProperty, model.queryString, model.selectedQuetyOption));
                    }
                }
            }
        }
    }
}