using System.Web;
using System.Web.Optimization;

namespace VTrade_Website_V3
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/jsheaderbundles/javasripts").Include(
                       "~/assets/js/jquery.min.js"
                       ));

            bundles.Add(new Bundle("~/jsfooterbundles/javasripts").Include(
                        "~/assets/vendor/purecounter/purecounter_vanilla.js",
                        "~/assets/vendor/aos/aos.js",
                        "~/assets/vendor/bootstrap/js/bootstrap.bundle.min.js",
                        "~/assets/vendor/glightbox/js/glightbox.min.js",
                        "~/assets/vendor/isotope-layout/isotope.pkgd.min.js",
                        "~/assets/vendor/swiper/swiper-bundle.min.js",
                        "~/assets/vendor/waypoints/noframework.waypoints.js",
                        "~/assets/js/main.js",
                        "~/assets/js/jquery.flurry.js"
                        ));

            bundles.Add(new Bundle("~/cssbundles/csstyle").Include(
                      "~/assets/vendor/aos/aos.css",
                      "~/assets/vendor/bootstrap/css/bootstrap.min.css",
                      "~/assets/vendor/bootstrap-icons/bootstrap-icons.css",
                      "~/assets/vendor/boxicons/css/boxicons.min.css",
                      "~/assets/vendor/glightbox/css/glightbox.min.css",
                      "~/assets/vendor/swiper/swiper-bundle.min.css",
                      "~/assets/css/style.css"));
        }
    }
}
