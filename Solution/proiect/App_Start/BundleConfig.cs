using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI.WebControls;

namespace proiect.App_Start
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            
            bundles.Add(new Bundle("~/bundles/mainPage.css").Include(
              "~/assets3/css/css/bootstrap-reboot.css",
              "~/assets3/css/aos.css",
              "~/assets3/css/bootstrap.css",
              "~/assets3/css/bootstrap-grid.css",
              "~/assets3/css/bootstrap-reboot.css",
              "~/assets3/css/flatpickr.min.css",
              "~/assets3/css/glightbox.min.css",
              "~/assets3/css/style.css",
              "~/assets3/css/fonts/icomoon/style.css",
              "~/assets3/fonts/flaticon/font/flaticon.css"
           ));

            bundles.Add(new ScriptBundle("~/bundles/mainPageJava/js").Include(
                "~/assets3/js/aos.js",
                "~/assets3/js/bootstrap.bundle.min.js",
                "~/assets3/js/counter.js",
                "~/assets3/js/custom.js",
                "~/assets3/js/flatpickr.min.js",
                "~/assets3/js/glightbox.min.js",
                "~/assets3/js/navbar.js",
                "~/assets3/js/tiny-slider.js"
                ));

            bundles.Add(new Bundle("~/bundles/bootstrap.css").Include(
               "~/assets/css/animate.min.css",
               "~/assets/css/responsive.css",
               "~/assets/css/style.css",
               "~/assets/plugins/pace/pace-theme-flash.css",
               "~/assets/plugins/bootstrap/css/bootstrap.min.css",
               "~/assets/fonts/font-awesome/css/font-awesome.css",
               "~/assets/css/animate.min.css",
               "~/assets/plugins/perfect-scrollbar/perfect-scrollbar.css"
            ));
            bundles.Add(new ScriptBundle("~/bundles/navbar/js").Include(
                "~/asset/js/blo-dashboard.js",
                "~/asset/js/chart-chartjs.js",
                "~/asset/js/chart-easypie.js",
                "~/asset/js/chart-echarts.js",
                "~/asset/js/chart-flot.js",
                "~/asset/js/chart-knobs.js",
                "~/asset/js/chart-morris.js",
                "~/asset/js/chart-rickshaw.js",
                "~/asset/js/chart-sparkline.js",
                "~/asset/js/crm-dashboard.js",
                "~/asset/js/dashboard.js",
                "~/asset/js/eco-dashboard.js",
                "~/asset/js/form-validation.js",
                "~/asset/js/frl-dashboard.js",
                "~/asset/js/hos-dashboard.js",
                "~/asset/js/jquery.easing.min.js",
                "~/asset/js/jquery-1.11.2.min.js",
                "~/asset/js/jquery-3.2.1.min.js",
                "~/asset/js/jquery-3.2.1.slim.min.js",
                "~/asset/js/messenger.js",
                "~/asset/js/popper.min.js",
                "~/asset/js/scripts.js",
                "~/asset/js/soc-dashboard.js",
                "~/asset/js/uni-dashboard.js"
                ));

        }
    }
}
