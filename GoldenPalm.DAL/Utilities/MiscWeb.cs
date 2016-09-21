using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace GoldenPalm.DAL
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CheckSessionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
           || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);

            if (!skipAuthorization)
            {
                {
                    HttpSessionStateBase session = filterContext.HttpContext.Session;
                    var user = session["UserInfo"];
                    if (((user == null) && (!session.IsNewSession)) || (session.IsNewSession))
                    {
                        //send them off to the login page
                        var url = new UrlHelper(filterContext.RequestContext);
                        var loginUrl = url.Content("~/Home/Index");
                        filterContext.HttpContext.Response.Redirect(loginUrl, true);
                    }
                }
            }
        }
    }
}
