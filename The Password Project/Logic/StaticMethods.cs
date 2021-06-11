using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace The_Password_Project.Model
{
    public class StaticMethods
    {
        /// <summary>
        /// Rainbow table resistant hashing
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Hash(string value)
        {
            var tmp = value;
            for (var i = 0; i < 10; i++)
            {
                tmp = sha256_hash(tmp);
            }

            return tmp;
        }

        private static string sha256_hash(string value)
        {
            StringBuilder Sb = new StringBuilder();
            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }

        public static string GenerateSeed()
        {
            return Path.GetRandomFileName();
        }

        public static string Filter(string chars, string illegalChars)
        {
            if (illegalChars == Constants.Default)
            {
                return chars;
            }
            foreach (var c in illegalChars)
            {
                chars = chars.Replace(c.ToString(), String.Empty);
            }
            return chars;
        }
    }
}
