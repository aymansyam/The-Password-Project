using System;
using System.Collections.Generic;
using System.Text;

namespace The_Password_Project.Model
{
    public class PasswordItem
    {
        public string AccessKey { get; set; }
        public Url Url { get; set; }
        public ProceduralNumber PrcNum { get; set; }

        public PasswordItem()
        {
            Url = new Url();
        }

        public string GetUrl()
        {
            return Url.GetString();
        }

        public string CreatePasswordV1()
        {
            // {Passwordy.Url.CharacterSet}{Passwordy.Url.ForbiddenCharacters}{Passwordy.Url.PasswordLength}
            var digest = StaticMethods.Hash($"{AccessKey}{Url.Id}");
            List<string> required = new List<string>();
            foreach (var item in Constants.CharacterSetDict)
            {
                if (Url.CharacterSet.Contains(item.Key))
                {
                    string s = StaticMethods.Filter(item.Value.Item1, Url.ForbiddenCharacters);
                    required.Add(s);
                }
            }

            PrcNum = new ProceduralNumber(digest);
            return GeneratePassV1(required, Url.PasswordLength);
        }

        public string CreatePasswordV2()
        {
            // {Passwordy.Url.CharacterSet}{Passwordy.Url.ForbiddenCharacters}{Passwordy.Url.PasswordLength}
            var digest = StaticMethods.Hash($"{AccessKey}{Url.Id}");
            List<string> required = new List<string>();
            foreach (var item in Constants.CharacterSetDict)
            {
                if (Url.CharacterSet.Contains(item.Key))
                {
                    string s = StaticMethods.Filter(item.Value.Item1, Url.ForbiddenCharacters);
                    required.Add(s);
                }
            }

            PrcNum = new ProceduralNumber(digest);
            return GeneratePassV2(required, Url.PasswordLength);
        }


        private string GeneratePassV2(IReadOnlyCollection<string> requiredSets, int passLength)
        {
            var password = "";
            var allowSkips = false;
            var skipCurrent = false;
            while (true)
            {
                foreach (var req in requiredSets)
                {
                    if (allowSkips)
                    {
                        skipCurrent = PrcNum.Generate(passLength) > (passLength / 2);
                        if (skipCurrent)
                        {
                            continue;
                        }
                    }
                    var index = PrcNum.Generate(req.Length);
                    password = $"{password}{req.AtWrapping(index)}";

                    if (password.Length == passLength)
                    {
                        password = ProceduralShuffle(password);
                        return password;
                    }
                }

                allowSkips = true;
            }
        }


        private string GeneratePassV1(IReadOnlyCollection<string> requiredSets, int passLength)
        {
            var password = "";
            while (true)
            {
                foreach (var req in requiredSets)
                {
                    var index = PrcNum.Generate(req.Length);
                    password = $"{password}{req.AtWrapping(index)}";

                    if (password.Length == passLength)
                    {
                        password = ProceduralShuffle(password);
                        return password;
                    }
                }

            }
        }

        private string ProceduralShuffle(string password)
        {
            var newPassword = "";
            var shuffle = new List<string>();
            foreach (var chr in password)
            {
                shuffle.Add(chr.ToString());
            }

            while (shuffle.Count > 0)
            {
                var index = PrcNum.Generate(shuffle.Count);
                newPassword = $"{newPassword}{shuffle.AtWrapping(index)}";
                shuffle.RemoveAt(shuffle.IndexWrap(index));
            }

            return newPassword;
        }
    }
}