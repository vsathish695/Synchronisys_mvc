using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Synchronisys_mvc
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
        protected void Application_BeginRequest()
        {
            var req = HttpContext.Current.Request;
            var response = HttpContext.Current.Response;
            if (req.HttpMethod == "OPTIONS")
            {
                response.StatusCode = 200;
                if (req["HTTP_Origin"] != null)
                {
                    response.Headers.Add("Access-Control-Allow-Origin", req["HTTP_Origin"]);
                }
                response.End();
            }
            else
            {
                if (req["HTTP_Origin"] != null)
                {
                    response.Headers.Add("Access-Control-Allow-Origin", req["HTTP_Origin"]);
                }
            }
        }
    }
}
