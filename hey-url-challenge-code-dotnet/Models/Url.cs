using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace hey_url_challenge_code_dotnet.Models
{
    public class Url : Identifiable<Guid>
    {
        //public Guid Id { get; set; }

        [Attr(PublicName = "url")]
        [MaxLength(5)]
        public string ShortUrl { get; set; }

        [Attr(PublicName = "original-url")]
        [Url(ErrorMessage = "Please provide the valid URL")]
        [Required(ErrorMessage = "Please provide the URL")]
        public string OriginalURL { get; set; }

        [Attr(PublicName = "created-at")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [Attr(PublicName = "count")]
        public int Count => Clicks.Count;


        [HasMany(CanInclude =true)]
        public ICollection<UrlClick> Clicks { get; set; } = new List<UrlClick>();
    }
}
