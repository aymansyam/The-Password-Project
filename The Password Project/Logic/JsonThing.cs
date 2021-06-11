using System;
using System.Collections.Generic;
using System.Text;

namespace The_Password_Project.Model
{
    public class JsonThing
    {
        public string Key { get; set; }
        public string Data { get; set; }
        public string Value { get; set; }
        public bool ShouldPromptAtStart { get; set; }


        public JsonThing()
        {
        }

        public JsonThing(string key, string data)
        {
            Key = key;
            Data = data;
        }

        public bool IsEmpty()
        {
            return Data.IsNullOrWhiteSpace();
        }
    }


}