using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Personal_FirstProject.Models;

namespace Personal_FirstProject.Filters
{
    public class Auth : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userJson = context.HttpContext.Session.GetString("_User");

            if (string.IsNullOrEmpty(userJson))
            {
                context.Result = new RedirectToRouteResult(
                new RouteValueDictionary(new { controller = "Login", action = "Login" }));
            }
            else
            {
                base.OnActionExecuting(context);
            }
        }
    }

    public class StaffAuth : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userJson = context.HttpContext.Session.GetString("_User");
            if (string.IsNullOrEmpty(userJson))
            {
                context.Result = new RedirectToRouteResult(
                new RouteValueDictionary(new { controller = "Login", action = "Login" }));
                return;
            }

            var user = JsonSerializer.Deserialize<User>(userJson);
            if (user.Role.ToLower() != "admin" && user.Role.ToLower() != "hrm")
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Error", action = "errorPage" }));
            }
            else
            {
                base.OnActionExecuting(context);
            }
        }
    }

    public class AdminAuth : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userJson = context.HttpContext.Session.GetString("_User");
            if (string.IsNullOrEmpty(userJson))
            {
                context.Result = new RedirectToRouteResult(
                new RouteValueDictionary(new { controller = "Login", action = "Login" }));
                return;
            }

            var user = JsonSerializer.Deserialize<User>(userJson);
            if (user.Role.ToLower() != "admin")
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Error", action = "errorPage" }));
            }
            else
            {
                base.OnActionExecuting(context);
            }
        }
    }
}