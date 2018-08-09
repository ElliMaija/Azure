using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CrudWebApi.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method,
 Inherited = true, AllowMultiple = false)]
    public class MyTimerFilterAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
        {
            // do something before the action executes
            Stopwatch timer = Stopwatch.StartNew();
            await next();
            // do something after the action executes
            timer.Stop();
            context.HttpContext.Response.Headers.Add(
            "x-timer", $"{timer.ElapsedMilliseconds} ms"
            );
        }
    }
}
