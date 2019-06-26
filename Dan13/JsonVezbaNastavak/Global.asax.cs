using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace JsonVezbaNastavak
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            GlobalConfiguration.Configuration.Formatters
                .JsonFormatter.SerializerSettings.DateFormatString = "dd-MM-yyyy hh:mm:ss";
        }
    }
}
