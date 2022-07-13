using System;
using System.Linq;

namespace hey_url_challenge_code_dotnet.Helpers
{
    public class Utils
    {
        private static Random random = new Random();

        public static string GenerateShortUrl(int length = 5)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
