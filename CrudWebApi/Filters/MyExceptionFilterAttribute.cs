using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CrudWebApi.Filters
{
    public class MyExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public MyExceptionFilterAttribute(
        IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public override void OnException(ExceptionContext context)
        {
            if (_hostingEnvironment.IsDevelopment())
            {
                context.Result = new JsonResult(context.Exception.Message);
            }
            else
            {
                context.Result = new JsonResult("bad request");
            }
            var exception = context.Exception;
            context.Result = new JsonResult(exception.Message);
            context.HttpContext.Response.StatusCode =
             (int)HttpStatusCode.BadRequest;
        }
    }
}
