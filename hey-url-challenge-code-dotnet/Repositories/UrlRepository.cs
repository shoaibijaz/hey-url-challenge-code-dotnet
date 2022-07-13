using hey_url_challenge_code_dotnet.Helpers;
using hey_url_challenge_code_dotnet.Models;
using hey_url_challenge_code_dotnet.Repositories.Interfaces;
using HeyUrlChallengeCodeDotnet.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hey_url_challenge_code_dotnet.Repositories
{
    public class UrlRepository : IUrlRepository
    {
        private readonly ApplicationContext applicationContext;

        public UrlRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task AddClick(string shortUrl, string OS, string browser)
        {
            var url = await this.FindByShortUrl(shortUrl);
            url.Clicks.Add(new UrlClick()
            {
                Browser = browser,
                OS = OS
            });

            await applicationContext.SaveChangesAsync();
        }

        public async Task<Url> CreateUrl(Url url)
        {
          
            url.ShortUrl = Utils.GenerateShortUrl();

            while(await this.FindByShortUrl(url.ShortUrl) != null)
            {
                url.ShortUrl = Utils.GenerateShortUrl();
            }

            applicationContext.Urls.Add(url);
            await applicationContext.SaveChangesAsync();
            return url;
        }

        public async Task<Url> FindByShortUrl(string shortUrl)
        {
            return await applicationContext.Urls.Include(a=>a.Clicks).FirstOrDefaultAsync(a => a.ShortUrl == shortUrl);
        }

        public async Task<IEnumerable<Url>> GetUrls()
        {
            return await applicationContext.Urls.OrderByDescending(a => a.CreatedOn).ToListAsync();
        }
    }
}
