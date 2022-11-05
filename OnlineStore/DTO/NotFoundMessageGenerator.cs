using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DTO
{
    public class ProductNotFoundExampleGenerator : OpenApiExample<NotFoundMessage>
    {
        public override IOpenApiExample<NotFoundMessage> Build(NamingStrategy namingStrategy = null)
        {
            return this;
        }
    }
    public class ReviewNotFoundExampleGenerator : OpenApiExample<NotFoundMessage>
    {
        public override IOpenApiExample<NotFoundMessage> Build(NamingStrategy namingStrategy = null)
        {
            return this;
        }
    }
    public class OrderNotFoundExampleGenerator : OpenApiExample<NotFoundMessage>
    {
        public override IOpenApiExample<NotFoundMessage> Build(NamingStrategy namingStrategy = null)
        {
            return this;
        }
    }
    public class UserNotFoundExampleGenerator : OpenApiExample<NotFoundMessage>
    {
        public override IOpenApiExample<NotFoundMessage> Build(NamingStrategy namingStrategy = null)
        {
            return this;
        }
    }
}
