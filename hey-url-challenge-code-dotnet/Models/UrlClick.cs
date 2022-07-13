using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;
using System;

namespace hey_url_challenge_code_dotnet.Models
{
    public class UrlClick : Identifiable<Guid>
    {
        //public Guid Id { get; set; }
        public Guid UrlId { get; set; }
        public string OS { get; set; }
        public string Browser { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [HasOne(CanInclude =true)]
        public Url Url { get; set; }
    }
}
