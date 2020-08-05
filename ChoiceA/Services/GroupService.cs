using System.Collections.Generic;
using System.Text.Json;
using System.IO;

namespace ChoiceA.Services
{
    public class GroupService : IGroupService
    {
        public IEnumerable<string> Groups { get; private set; }
        private readonly string GroupsFileName = "groups.json";

        public GroupService()
        {
            string jsonString = File.ReadAllText(GroupsFileName);
            var desJson = JsonSerializer.Deserialize<IDictionary<string, IEnumerable<string>>>(jsonString);
            Groups = desJson["Groups"];
        }
    }
}
