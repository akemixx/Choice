using ChoiceA.Filters;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChoiceA.Attributes
{
    public class ForStudentAttribute : Attribute, IFilterFactory
    {
        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return new ForStudentFilter();
        }
    }
}
