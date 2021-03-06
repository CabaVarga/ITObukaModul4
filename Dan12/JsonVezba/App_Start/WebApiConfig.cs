﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace JsonVezba
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            // 1. RESENJE za rekurziju
            //config.Formatters.JsonFormatter
            //    .SerializerSettings
            //    .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            // 2. RESENJE za rekurziju
            //config.Formatters.JsonFormatter
            //    .SerializerSettings
            //    .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;

            // 3. RESENJE za rekurziju
            //config.Formatters.JsonFormatter
            //    .SerializerSettings
            //    .PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;



        }
    }
}
