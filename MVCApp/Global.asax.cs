﻿using MVCApp.CustomAuthentication;
using MVCApp.DataAccess;
using MVCApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using ExpressiveAnnotations.Attributes;
using ExpressiveAnnotations.MvcUnobtrusive.Providers;
using ExpressiveAnnotations.MvcUnobtrusive.Validators;

namespace MVCApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
        
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {//Setting Up Authentication for user, currently already determine user is valid user and assigning cookie for session.
            HttpCookie authCookie = Request.Cookies["Cookie1"];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                var serializeModel = JsonConvert.DeserializeObject<CustomSerializeModel>(authTicket.UserData);
                CustomPrincipal principal = new CustomPrincipal(authTicket.Name);

                principal.ID = serializeModel.ID;
                principal.FirstName = serializeModel.FirstName;
                principal.LastName = serializeModel.LastName;
                principal.Roles = serializeModel.RoleName.ToArray<string>();

                HttpContext.Current.User = principal;
            }

        }
    }
}
