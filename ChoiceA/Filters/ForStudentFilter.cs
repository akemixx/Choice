using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChoiceA.Filters
{
    public class ForStudentFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Claims.Any(c => c.Type == "StudentId"))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
