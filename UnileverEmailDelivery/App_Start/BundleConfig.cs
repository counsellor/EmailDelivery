using System.Web;
using System.Web.Optimization;

namespace UnileverEmailDelivery
{
    public class BundleConfig
    {
        //Дополнительные сведения об объединении см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                        "~/Scripts/DataTables/jquery.dataTables.min.js",
                         "~/Scripts/DataTables/dataTables.select.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jscolor").Include(
            "~/Scripts/jscolor.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/site.css",
                        "~/Content/DataTables/css/jquery.dataTables.min.css",
                        "~/Content/DataTables/css/select.dataTables.min.css"));
        }
    }
}