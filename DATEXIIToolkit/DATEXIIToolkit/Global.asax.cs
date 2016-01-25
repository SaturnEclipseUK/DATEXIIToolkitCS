using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using DATEXIIToolkit.Services;
using DATEXIIToolkit.Common;
using System.Configuration;
using System.IO;

namespace DATEXIIToolkit
{
    public class Global : HttpApplication
    {
        private LogWrapper logWrapper;

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
            logWrapper = new LogWrapper("Global");
            logWrapper.Warning("*** Starting DATEXII Toolkit Web Services ***");

            string rootDirectory = ConfigurationManager.AppSettings["rootDirectory"];
            if (!Directory.Exists(rootDirectory))
            {
                Directory.CreateDirectory(rootDirectory);
            }
            DATEXIIUpdateService.GetInstance();
        }
    }
}