using ChoiceA.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChoiceA.Extensions
{
    public static class GroupServiceExtension
    {
        public static IServiceCollection AddGroupService(this IServiceCollection services)
        {
            return services.AddSingleton<IGroupService, GroupService>();
        }
    }
}
