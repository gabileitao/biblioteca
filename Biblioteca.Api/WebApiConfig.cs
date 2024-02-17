﻿using System;
using System.Web.Http;

namespace Biblioteca.Api
{
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {
            // Serviços e configuração da API da Web
            config.EnableCors();

            // Rotas da API da Web
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(new System.Net.Http.Formatting.RequestHeaderMapping("Accept", "text/html", StringComparison.InvariantCultureIgnoreCase, true, "application/json"));
        }
    }
}