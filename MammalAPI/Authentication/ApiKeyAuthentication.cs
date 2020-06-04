using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration.Binder;
using Microsoft.Extensions.Configuration;
using MammalAPI.Context;

namespace MammalAPI.Authentication
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAuthentication : Attribute, IAsyncActionFilter
    {
        private const string Username = "APIUsername";

        private const string Password = "APIPassword";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(Username, out var potentialUsername))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!context.HttpContext.Request.Headers.TryGetValue(Password, out var potentialPassword))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            using (var dBContext = new DBContext())
            {
                var user = dBContext.UserAccounts.Where(u => u.Username == potentialUsername.ToString()
                            && u.Password == potentialPassword.ToString()).FirstOrDefault();
                
                if (user == null)
                {
                        context.Result = new UnauthorizedResult();
                        return;
                }              
            } 
            await next();
        }
    }
}
