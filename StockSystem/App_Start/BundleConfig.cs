using System.Web;
using System.Web.Optimization;

namespace StockSystem
{
    public class BundleConfig
    {        
        public static void RegisterBundles(BundleCollection bundles)
        {           
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));           

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/tether.min.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootbox.js",                                            
                      "~/Scripts/jquery.easing.min.js",
                      "~/Scripts/DataTables/jquery.dataTables.js",
                      "~/Scripts/DataTables/dataTables.bootstrap4.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/sb-admin.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(                                                                                        
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css",
                      "~/Content/Site.css",
                      "~/Content/datatables/css/dataTables.bootstrap4.css",
                      "~/Content/sb-admin.css"));                                              
                                            
            BundleTable.EnableOptimizations = true;
        }
    }
}
