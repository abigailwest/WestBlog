using System.Web;
using System.Web.Optimization;

namespace WestBlog
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                         "~/Scripts/jquery-{version}.js",
                         "~/Scripts/jquery-2.1.3.js",
                         "~/Scripts/bootstrap.min.js",
                         "~/Scripts/jquery.parallax-1.1.3.js",
                         "~/Scripts/jquery.superslides.min.js",
                         "~/Scripts/jquery.mb.YTPlayer.min.js",
                         "~/Scripts/jquery.simple-text-rotator.min.js",
                         "~/Scripts/imagesloaded.pkgd.js",
                         "~/Scripts/isotope.pkgd.min.js",
                         "~/Scripts/jquery.magnific-popup.min.js",  
                         "~/Scripts/owl.carousel.min.js",
                         "~/Scripts/jquery.fitvids.js",
                         "~/Scripts/jqBootstrapValidation.js",
                         "~/Scripts/gmap3.min.js",
                         "~/Scripts/appear.js",
                         "~/Scripts/wow.min.js", 
                         "~/Scripts/smoothscroll.js",
                         "~/Scripts/submenu-fix.js",
                         "~/Scripts/custom.js",
                         "~/Scripts/Site.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                    "~/Content/bootstrap.css",
                    "~/Site Template/greensea.css",
                    "~/Site Template/style.css",
                    "~/Site Template/bootstrap-theme.min.css",
                    "~/Site Template/animate.css",
                    "~/Site Template/font-awesome.min.css",
                    "~/Site Template/magnific-popup.css",
                    "~/Site Template/owl.carousel.css",
                    "~/Site Template/simpletextrotator.css",
                    "~/Site Template/stroke-gap-icons.css",
                    "~/Site Template/superslides.css",
                    "~/Site Template/vertical.min.css",
                    "~/Content/site.css"));
        }
    }
}
