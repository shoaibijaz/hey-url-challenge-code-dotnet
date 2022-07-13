using hey_url_challenge_code_dotnet.Models;
using HeyUrlChallengeCodeDotnet.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hey_url_challenge_code_dotnet
{
    public class Seed
    {
        public static async Task SeedData(ApplicationContext applicationContext)
        {
            if(!applicationContext.Urls.Any())
            {

                var urls = new List<Url>
                {
                    new()
                    {
                        ShortUrl = "ABCDE",
                        OriginalURL = "https://fullstacklabs.notion.site/NET-Coding-Challenge-1fa48d6d7e614294aff3149a347f95b3",
                    },
                    new()
                    {
                        ShortUrl = "ABCDE",
                        OriginalURL = "https://fullstacklabs.notion.site/NET-Coding-Challenge-1fa48d6d7e614294aff3149a347f95b3",
                    },
                    new()
                    {
                        ShortUrl = "ABCDE",
                        OriginalURL = "https://fullstacklabs.notion.site/NET-Coding-Challenge-1fa48d6d7e614294aff3149a347f95b3",
                    },
                };

               await applicationContext.Urls.AddRangeAsync(urls);
               await applicationContext.SaveChangesAsync();
            }
        }
    }
}
