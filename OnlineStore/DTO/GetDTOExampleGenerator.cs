using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Newtonsoft.Json.Serialization;
using OnlineStore.Model;

namespace OnlineStore.DTO
{

    public class GetProductDTOExampleGenerator : OpenApiExample<Product>
    {
        public override IOpenApiExample<Product> Build(NamingStrategy namingStrategy = null)
        {
            return this;
        }
    }
    public class GetReviewDTOExampleGenerator : OpenApiExample<Review>
    {
        public override IOpenApiExample<Review> Build(NamingStrategy namingStrategy = null)
        {
            return this;
        }
    }
    public class GetUserDTOExampleGenerator : OpenApiExample<User>
    {
        public override IOpenApiExample<User> Build(NamingStrategy namingStrategy = null)
        {
            return this;
        }
    }
    public class GetOrderDTOExampleGenerator : OpenApiExample<Order>
    {
        public override IOpenApiExample<Order> Build(NamingStrategy namingStrategy = null)
        {
            return this;

        }
    }
}
