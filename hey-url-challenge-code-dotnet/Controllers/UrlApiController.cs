using hey_url_challenge_code_dotnet.Models;
using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Services;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Mvc;

namespace hey_url_challenge_code_dotnet.Controllers
{
    public class UrlApiController : JsonApiController<Url, Guid>
    {
        public UrlApiController(IJsonApiOptions options, IResourceGraph resourceGraph, ILoggerFactory loggerFactory,
            IResourceService<Url, Guid> resourceService)
      : base(options, loggerFactory, resourceService)
        { }

       
    }
}
