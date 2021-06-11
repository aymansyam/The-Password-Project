using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace The_Password_Project.Model
{
    public class PassGenService
    {
        public IList<JsonThing> Results { get; set; }

        public PasswordItem PassItem { get; set; }

        public PassGenService()
        {
            Results = new List<JsonThing>();
            PassItem = new PasswordItem();
        }

        public void Init(IList<JsonThing> list)
        {
            PassItem.AccessKey = list.GetJsonThing(Constants.PasswordAccessKey).Data;
            PassItem.Url.Id = list.GetJsonThing(Constants.IDKey).Data;
            PassItem.Url.CharacterSet = list.GetJsonThing(Constants.CharacterSetKey).Data;
            PassItem.Url.ForbiddenCharacters = list.GetJsonThing(Constants.ForbiddenCharactersKey).Data;
            PassItem.Url.PasswordLength = Int32.Parse(list.GetJsonThing(Constants.PasswordLengthKey).Data);
        }

        public void Produce()
        {
            JsonThing result = new JsonThing
            {
                Data = PassItem.CreatePasswordV1(),
                Value = PassItem.GetUrl()
            };
            Results.Add(result);
        }

        public void ProduceV2()
        {
            JsonThing result = new JsonThing
            {
                Data = PassItem.CreatePasswordV2(),
                Value = PassItem.GetUrl()
            };
            Results.Add(result);
        }

    }
}
