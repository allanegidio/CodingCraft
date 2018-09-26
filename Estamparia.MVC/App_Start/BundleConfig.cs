using System.Web.Optimization;

namespace Estamparia.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery-validate/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr/modernizr-*"));


            // Bundle of Bootstrap

            bundles.Add(new ScriptBundle("~/bootstrap/js").Include(
                      "~/Scripts/bootstrap/bootstrap.min.js",
                      "~/Scripts/bootstrap/respond.min.js"));

            bundles.Add(new StyleBundle("~/bootstrap/css").Include(
                      "~/Content/bootstrap/css/bootstrap.min.css",
                      "~/Content/bootstrap/site-bootstrap.css"));

            // Bundle of Zurb-Foundation

            bundles.Add(new StyleBundle("~/foundation/css").Include(
                      "~/Content/zurb-foundation/css/foundation.min.css",
                      "~/Content/zurb-foundation/site-foundation.css"));

            bundles.Add(new ScriptBundle("~/foundation/js").Include(
                     "~/Scripts/zurb-foundation/js/foundation.min.js",
                     "~/Scripts/zurb-foundation/site-foundation.js"));

            // Bundle of Ink
            bundles.Add(new StyleBundle("~/ink/css").Include(
                "~/Content/ink/css/ink.min.css",
                "~/Content/ink/css/font-awesome.min.css",
                "~/Content/ink/css/site-ink.css"
                ));

            bundles.Add(new ScriptBundle("~/ink/js").Include(
                "~/Content/ink/js/ink-all.min.js",
                "~/Content/ink/js/autoload.min.js"
                ));

            // Bundle of App
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                    "~/Scripts/app/controllers/layoutController.js",
                    "~/Scripts/app/app.js"
                ));

        }
    }
}
