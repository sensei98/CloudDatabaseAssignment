using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;

namespace OnlineStore
{
    public class OpenApiConfiguration : DefaultOpenApiConfigurationOptions
    {
        public override OpenApiInfo Info { get; set; } = new OpenApiInfo
        {
            Version = "1.0.0",
            Title = "Online store",
            Description = "The API for the Online store",
            //License = new OpenApiLicense
            //{
            //    Name = "MIT",
            //    Url = new Uri("http://opensource.org/licenses/MIT"),
            //}
        };

        public override OpenApiVersionType OpenApiVersion { get; set; } = OpenApiVersionType.V3;
    }
}
