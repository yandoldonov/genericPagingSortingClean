using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace genericPagingSortingClean.Controllers
{
    public class baseController : Controller
    {
        protected string title
        {
            get { return this.GetType().Name.Remove(this.GetType().Name.Length - 10); }
        }
    }
}