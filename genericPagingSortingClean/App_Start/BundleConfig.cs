using System.Web;
using System.Web.Optimization;

namespace genericPagingSortingClean
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/bootstrapStyles").Include(
                    "~/Content/bootstrap.min.css",
                    "~/Content/custom.css"));

            bundles.Add(new StyleBundle("~/Content/jqueryUi").Include(
                   "~/Content/jquery-ui.css",
                    "~/Content/jquery-ui.structure.css",
                   "~/Content/jquery-ui.theme.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                 "~/Scripts/jquery-3.4.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryUi").Include(
            "~/Scripts/jquery-ui.js",
            "~/Scripts/jquery.blockUI.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapScripts").Include(
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/unobtrusiveAjax").Include(
                "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/validation").Include(
              "~/Scripts/jquery.validate.min.js",
               "~/Scripts/jquery.validate.unobtrusive.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                "~/Scripts/custom.js"));
        }
    }
}