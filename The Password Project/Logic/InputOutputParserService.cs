using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Transactions;
using Newtonsoft.Json;

namespace The_Password_Project.Model
{
    public class InputOutputParserService
    {
        public IList<JsonThing> Input { get; set; }
        public IList<JsonThing> AfterProcessing { get; set; }
        public IList<JsonThing> Output { get; set; }

        public InputOutputParserService(IList<JsonThing> input)
        {
            Input = input;
            AfterProcessing = new List<JsonThing>();
        }

        public void Show()
        {
            Console.WriteLine($"Your Password is: {Output.Last()?.Data}");
            Console.WriteLine($"Copy this link for later access to password: {GenerateURL()}");
            Console.WriteLine(Constants.Divider);
        }

        private string GenerateURL()
        {
            var seed = AfterProcessing.GetJsonThing(Constants.IDKey).Data;
            var characterSet = AfterProcessing.GetJsonThing(Constants.CharacterSetKey).Data;
            var forbiddenCharacters = AfterProcessing.GetJsonThing(Constants.ForbiddenCharactersKey).Data;
            var passwordLength = AfterProcessing.GetJsonThing(Constants.PasswordLengthKey).Data;
            return $"{seed}/{characterSet}/{forbiddenCharacters}/{passwordLength}";
        }

        public void GetInput()
        {
            foreach (var prompt in Input)
            {
                if (prompt.ShouldPromptAtStart)
                {
                    Console.WriteLine(prompt.Value);
                    prompt.Data = Console.ReadLine();
                }
            }
        }

        public void ForcePrompt(string key)
        {
            var prompt = Input.GetJsonThing(key);
            Console.WriteLine(prompt.Value);
            prompt.Data = Console.ReadLine();
        }

        /// <summary>
        /// Has to be in the form of: {seed}/{CharacterSet}/{ForbiddenCharacters}/{PasswordLength}
        /// </summary>
        public void PopulatePromptsFromUrl()
        {
            var fields = Input.GetJsonThing(Constants.PasswordURLKey).Data.Split("/");
            if (fields.Length != 4) return;

            AfterProcessing.Add(new JsonThing(Constants.IDKey, fields[^4]));
            AfterProcessing.Add(new JsonThing(Constants.CharacterSetKey, fields[^3]));
            AfterProcessing.Add(new JsonThing(Constants.ForbiddenCharactersKey, fields[^2]));
            AfterProcessing.Add(new JsonThing(Constants.PasswordLengthKey, fields[^1]));
        }

        public void ConvertInput()
        {
            var link = Input.GetJsonThing(Constants.PasswordURLKey);
            if (!link.IsEmpty())
            {
                PopulatePromptsFromUrl();
            }
            if (!IsValidInput())
            {
                throw new ArgumentNullException();
            }
            // BlazorApp Addition
            AfterProcessing.Add(new JsonThing(Constants.PasswordAccessKey, Input.GetJsonThing(Constants.PasswordAccessKey).Data));

            if (AfterProcessing.GetJsonThing(Constants.IDKey).Data == Constants.Default)
            {
                AfterProcessing.GetJsonThing(Constants.IDKey).Data = StaticMethods.GenerateSeed();
            }

            //if (AfterProcessing.GetJsonThing(Constants.IDKey).Data == Constants.Default)
            //{
            //    Console.WriteLine("Creating a new ID. Please enter your access key twice to generate your URL.");
            //    AfterProcessing.GetJsonThing(Constants.IDKey).Data = PassGenService.GenerateSeed();
            //    while (true)
            //    {
            //        ForcePrompt(Constants.PasswordAccessKey);
            //        ForcePrompt(Constants.PasswordConfirmationKey);
            //        if (Input.GetJsonThing(Constants.PasswordAccessKey).Data ==
            //            Input.GetJsonThing(Constants.PasswordConfirmationKey).Data)
            //        {
            //            AfterProcessing.Add(new JsonThing(Constants.PasswordAccessKey, Input.GetJsonThing(Constants.PasswordAccessKey).Data));
            //            break;
            //        }
            //        Console.WriteLine("Passwords don't match, try again\n");
            //    }
            //}
            //else
            //{
            //    ForcePrompt(Constants.PasswordAccessKey);
            //    AfterProcessing.Add(new JsonThing(Constants.PasswordAccessKey, Input.GetJsonThing(Constants.PasswordAccessKey).Data));
            //}
        }

        public bool IsValidInput()
        {
            if (AfterProcessing.Count == 0)
            {
                return false;
            }

            
            foreach (var jsonThing in AfterProcessing)
            {
                if (jsonThing.Data.IsNullOrWhiteSpace())
                {
                    return false;
                }
            }

            var characterSet = AfterProcessing.GetJsonThing(Constants.CharacterSetKey).Data;
            var passwordLength = AfterProcessing.GetJsonThing(Constants.PasswordLengthKey).Data;
            var forbiddenCharacters = AfterProcessing.GetJsonThing(Constants.ForbiddenCharactersKey).Data;

            if (characterSet.Length > Constants.CharacterSetDict.Count)
            {
                return false;
            }
            foreach (var chr in characterSet)
            {
                if (!Constants.CharacterSetDict.ContainsKey(chr.ToString()))
                {
                    return false;
                }
            }

            if (IsDuplicateChars(characterSet))
            {
                return false;
            }

            if (IsDuplicateChars(forbiddenCharacters))
            {
                return false;
            }

            if (!int.TryParse(passwordLength, out var num))
            {
                return false;
            }
            if (num <= 0)
            {
                return false;
            }

            // All validation passed
            return true;
        }

        public static bool IsDuplicateChars(string key)
        {
            // ... Store encountered letters in this string.
            string result = "";

            foreach (char value in key)
            {
                // See if character is in the result already.
                if (result.IndexOf(value) == -1)
                {
                    // Append to the result.
                    result = $"{result}{value}";
                }
            }

            if (result.Length != key.Length)
            {
                return true;
            }

            return false;
        }

        public void ResetInput()
        {
            AfterProcessing = new List<JsonThing>();
            Output = new List<JsonThing>();
        }
    }
}
