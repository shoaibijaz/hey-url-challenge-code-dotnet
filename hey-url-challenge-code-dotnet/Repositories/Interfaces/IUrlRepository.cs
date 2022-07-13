using hey_url_challenge_code_dotnet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hey_url_challenge_code_dotnet.Repositories.Interfaces
{
    public interface IUrlRepository
    {
       Task<IEnumerable<Url>> GetUrls();
       Task<Url> FindByShortUrl(string shortUrl);
       Task<Url> CreateUrl(Url url);
       
        Task AddClick(string shortUrl, string OS, string browser);
    }
}
