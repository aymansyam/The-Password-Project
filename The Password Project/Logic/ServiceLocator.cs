using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace The_Password_Project.Model
{
    public class ServiceLocator
    {
        public InputOutputParserService InputOutputParserService { get; set; }
        public PassGenService PassGenService { get; set; }
        public void InitDefaults()
        {
            InputOutputParserService = new InputOutputParserService(GetConfig<IList<JsonThing>>("filename"));
            PassGenService = new PassGenService();
        }



        // CHANGEME, later
        public static T GetConfig<T>(string configKey)
        {
            return JsonConvert.DeserializeObject<T>(Constants.Prompts);
        }

        public void SharePrompts()
        {
            PassGenService.Init(InputOutputParserService.AfterProcessing);
        }

        public void ShareResults()
        {
            InputOutputParserService.Output = PassGenService.Results;
        }

    }
}