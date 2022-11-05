using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Newtonsoft.Json.Serialization;
using OnlineStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DTO
{
    public class DeleteProductDTOExampleGenerator : OpenApiExample<Product>
    {
        public override IOpenApiExample<Product> Build(NamingStrategy namingStrategy = null)
        {
            return this;
        }
    }
    public class DeleteReviewDTOExampleGenerator : OpenApiExample<Review>
    {
        public override IOpenApiExample<Review> Build(NamingStrategy namingStrategy = null)
        {
            return this;
        }
    }
    public class DeleteUserDTOExampleGenerator : OpenApiExample<User>
    {
        public override IOpenApiExample<User> Build(NamingStrategy namingStrategy = null)
        {
            return this;
        }
    }
    public class DeleteOrderDTOExampleGenerator : OpenApiExample<Order>
    {
        public override IOpenApiExample<Order> Build(NamingStrategy namingStrategy = null)
        {
            return this;

        }
    }
}
