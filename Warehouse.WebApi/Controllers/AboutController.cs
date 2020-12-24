using Warehouse.Protocol;
using Warehouse.Protocol.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Warehouse.WebApi.Controllers
{
    [Route(Uris.About)]
    public class AboutController
    {
        public IWebHostEnvironment Environment { get; }

        public AboutController(IWebHostEnvironment environment)
        {
            this.Environment = environment;
        }

        [HttpGet]
        [ProducesResponseType(typeof(About), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), 500)]
        public About Get()
        {
            return new About
            {
                Description = "Warehouse - Sorting center api",
                Environment = this.Environment.EnvironmentName,
                Version = this.GetType().Assembly.GetName().Version.ToString()
            };
        }
    }
}