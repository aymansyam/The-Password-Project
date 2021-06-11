using System;
using System.Collections.Generic;

namespace The_Password_Project.Model
{
    public static class Constants
    {
        public const string PromptsFileNameKey = "Input.json";
        public const string Divider = "----------------------------------";
        public const string PasswordAccessKey = "Password Access";
        public const string IDKey = "ID";
        public const string CharacterSetKey = "Character Set";
        public const string ForbiddenCharactersKey = "Forbidden Characters";
        public const string PasswordLengthKey = "Password Length";
        public const string PasswordURLKey = "Password URL";

        public const string Default = "default";

        public const string Prompts = "[\r\n  {\r\n    \"Key\": \"Password URL\",\r\n    \"Value\": \"Enter password URL here in the form of PasswordID/CharacterSet/ForbiddenCharacters/PasswordLength.\",\r\n    \"ShouldPromptAtStart\": true\r\n  },\r\n  {\r\n    \"Key\": \"Password Access\",\r\n    \"Value\": \"Enter your password access key\",\r\n    \"ShouldPromptAtStart\": false\r\n  },\r\n  {\r\n    \"Key\": \"Password Confirmation\",\r\n    \"Value\": \"Confirm your password access key\",\r\n    \"ShouldPromptAtStart\": false\r\n  }\r\n]";
        public const string PasswordConfirmationKey = "Password Confirmation";

        public static readonly Dictionary<string, (string, string)> CharacterSetDict = new Dictionary<string, (string, string)>()
        {
            { "l", ("abcdefghijklmnopqrstuvwxyz", "Small Letters") },
            { "L", ("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "Capital Letters") },
            { "n", ("0123456789", "Numbers")},
            { "s", ("!\"#$%&'()*+,-.:;<=>?@[\\]^_`{|}~", "Symbols") },
            { "f", ("/ ", "Special Characters (\"/\" and space)")}
        };
    }
}