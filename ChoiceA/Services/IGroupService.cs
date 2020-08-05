using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChoiceA.Services
{
    public interface IGroupService
    {
        IEnumerable<string> Groups { get; }
    }
}
