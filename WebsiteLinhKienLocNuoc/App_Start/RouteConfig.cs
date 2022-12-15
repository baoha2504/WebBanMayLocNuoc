using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebsiteLinhKienLocNuoc
{
     public class RouteConfig
     {
          public static void RegisterRoutes(RouteCollection routes)
          {
               routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
               routes.MapRoute(
               name: "confirmRegister", 
                url: "confirmRegister",
                defaults: new { controller = "Account", action = "confirmRegister", id = UrlParameter.Optional }, new[] { "WebsiteLinhKienLocNuoc.Controllers" }
                    );
               routes.MapRoute(
               name: "confirmLogin",
                url: "confirmLogin",
                defaults: new { controller = "Account", action = "confirmLogin", id = UrlParameter.Optional }, new[] { "WebsiteLinhKienLocNuoc.Controllers" }
                    );
               routes.MapRoute(
               name: "updateInfor",
                url: "updateInfor",
                defaults: new { controller = "Account", action = "updateInfor", id = UrlParameter.Optional }, new[] { "WebsiteLinhKienLocNuoc.Controllers" }
                    );
               routes.MapRoute(
               name: "changedPassword",
                url: "changedPassword",
                defaults: new { controller = "Account", action = "changedPassword", id = UrlParameter.Optional }, new[] { "WebsiteLinhKienLocNuoc.Controllers" }
                    );
               routes.MapRoute(
               name: "CheckAccount",
                url: "CheckAccount",
                defaults: new { controller = "Account", action = "CheckAccount", id = UrlParameter.Optional }, new[] { "WebsiteLinhKienLocNuoc.Controllers" }
                    );
               //routes.MapRoute(
               //name: "Categories",
               // url: "Categories/{id}",
               // defaults: new { controller = "Categories", action = "Categories", id = UrlParameter.Optional }, new[] { "WebsiteLinhKienLocNuoc.Controllers" }
               //     );
               routes.MapRoute(
                   name: "Default",
                   url: "{controller}/{action}/{id}",
                   defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
               );
          }
     }
}
