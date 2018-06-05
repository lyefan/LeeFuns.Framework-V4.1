using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LeeFuns.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",                        // 路由名
                url: "{controller}/{action}/{id}",      // URL参数
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }     // 默认参数
            );
        }
    }
}
