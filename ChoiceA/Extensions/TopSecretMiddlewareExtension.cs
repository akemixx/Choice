using ChoiceA.Middleware;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChoiceA.Extensions
{
    public static class TopSecretMiddlewareExtension
    {
        public static IApplicationBuilder UseTopSecretMiddleware(this IApplicationBuilder app, string path)
        {
            return app.UseMiddleware<TopSecretMiddleware>(path);
        }
    }
}
