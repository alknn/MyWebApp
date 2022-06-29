using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace MyWebApp
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

        protected void Application_AuthenticateRequest()
        {
            //Account controller da login içinde bileti oluşturup attığım cookie çağırıyorum ve kontrol ediyorum var mı yok mu diye.
            var authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                //varsa encrpyt ettiğim FormsAuthenticationTicket nesnesini tekrar FormsAuthenticationTicket nesnesine çeviriyorum.
                var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                //rolleri alıyorum.
                var role = authTicket.UserData.Split(',');
                //kullanıcı prensibi
                var userPrincipal = new GenericPrincipal(new GenericIdentity(authTicket.Name), role);
                //contex user a vermek adına kullanıcı prensibini kullanıyorum.
                Context.User = userPrincipal;
            }
        }
    }
}
