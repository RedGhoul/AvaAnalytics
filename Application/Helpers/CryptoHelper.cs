using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Application.Helpers
{
    public static class CryptoHelper
    {
        public static string SHA512(string text)
        {
            string result = default;

            using (SHA512Managed algo = new SHA512Managed())
            {
                result = GenerateHashString(algo, text);
            }

            return result;
        }

        private static string GenerateHashString(HashAlgorithm algo, string text)
        {
            algo.ComputeHash(Encoding.UTF8.GetBytes(text));
            byte[] result = algo.Hash;
            return string.Join(
                string.Empty,
                result.Select(x => x.ToString("x2")));
        }
    }
}
