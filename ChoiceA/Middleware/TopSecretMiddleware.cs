using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.Extensions.FileProviders.Physical;
using Microsoft.Extensions.FileProviders;

namespace ChoiceA.Middleware
{
    public class TopSecretMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostingEnvironment _env;
        private readonly string _path;
        public TopSecretMiddleware(RequestDelegate next, IHostingEnvironment env,  string path)
        {
            _next = next;
            _env = env;
            _path = path;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var fileName = context.Request.Path.Value.Split("/").Last();
                if (fileName == _path.Split("/").Last())
                {
                    var fProvider = new PhysicalFileProvider(_env.WebRootPath);
                    var fInfo = fProvider.GetFileInfo(_path);
                    await context.Response.SendFileAsync(fInfo);
                }
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
