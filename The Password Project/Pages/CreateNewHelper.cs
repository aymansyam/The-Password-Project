using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using The_Password_Project.Model;

namespace The_Password_Project.Pages
{
    public class CreateNewHelper
    {
        public List<JsonThing> args { get; set; }
        public CreateNewHelper(Dictionary<string, (string, string)> sets)
        {
            args = new List<JsonThing>();
            foreach (var set in sets)
            {
                var tmp = new JsonThing(set.Key, set.Value.Item1);
                tmp.Value = set.Value.Item2;
                if (tmp.Key != "f")
                {
                    tmp.ShouldPromptAtStart = true;
                }
                args.Add(tmp);
            }
        }
    }
}
