using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Cors;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace Todo.Mvc
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Enable cors
            EnableCorsAttribute corsAttr = new EnableCorsAttribute("http://localhost:49360", "*", "*");
            config.EnableCors(corsAttr);

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);


            config.Routes.MapHttpRoute(
              name: "DefaultApiWithActions",
              routeTemplate: "api/{controller}/{action}/{param1}/{param2}",
              defaults: new
              {
                  param1 = RouteParameter.Optional,
                  param2 = RouteParameter.Optional
              }
          );


            config.Routes.MapHttpRoute(
               name: "DefaultApi",
               routeTemplate: "api/{controller}/{param1}/{param2}",
               defaults: new
               {
                   param1 = RouteParameter.Optional,
                   param2 = RouteParameter.Optional
               }
           );
        }
    }
}
