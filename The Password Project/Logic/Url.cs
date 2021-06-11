using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace The_Password_Project.Model
{
    public class Url
    {
        public string Id { get; set; }
        public string CharacterSet { get; set; }
        public string ForbiddenCharacters { get; set; }
        public int PasswordLength { get; set; }

        public string GetString()
        {
            return $"{Id}/{CharacterSet}/{ForbiddenCharacters}/{PasswordLength}";
        }
    }
}
