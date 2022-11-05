using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;
using OnlineStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DTO
{
    public class ResponseCreatedExampleGenerator
    {

    }
    public class ProductResponseCreatedExampleGenerator : OpenApiExample<Product>
    {
        public override IOpenApiExample<Product> Build(NamingStrategy namingStrategy = null)
        {
            Examples.Add(OpenApiExampleResolver.Resolve("Product Creation", "Product has been Created",namingStrategy));
            return this;
        }
    }

    public class OrderResponseCreatedExampleGenerator : OpenApiExample<Order>
    {
        public override IOpenApiExample<Order> Build(NamingStrategy namingStrategy = null)
        {
            Examples.Add(OpenApiExampleResolver.Resolve("Order Creation", "Order has been Created", namingStrategy));
            return this;
        }
       
    }
    public class ReviewResponseCreatedExampleGenerator : OpenApiExample<Review>
    {
        public override IOpenApiExample<Review> Build(NamingStrategy namingStrategy = null)
        {
            Examples.Add(OpenApiExampleResolver.Resolve("Review Creation", "Review has been Created", namingStrategy));
            return this;
        }
    }

    public class UserResponseCreatedExampleGenerator : OpenApiExample<User>
    {
        public override IOpenApiExample<User> Build(NamingStrategy namingStrategy = null)
        {
            Examples.Add(OpenApiExampleResolver.Resolve("User Creation", "User has been Created", namingStrategy));
            return this;
        }
    }
}
