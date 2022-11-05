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
    public class CreateProductDTOExampleGenerator : OpenApiExample<CreateProductDTO>
    {
        public override IOpenApiExample<CreateProductDTO> Build(NamingStrategy namingStrategy = null)
        {
            Examples.Add(OpenApiExampleResolver.Resolve("Product Creation", new CreateProductDTO()
            {
                Description = "this is the description",
                Name = "Product Name",
                Price = "€500",
                Stock = 60,
                ImageURL = "https://imageurl.com"
            }, namingStrategy));

            return this;
        }
    }
    public class OrderDTOExampleGenerator : OpenApiExample<CreateOrderDTO>
    {
        public override IOpenApiExample<CreateOrderDTO> Build(NamingStrategy namingStrategy = null)
        {
            Examples.Add(OpenApiExampleResolver.Resolve("Order Creation", new CreateOrderDTO()
            {
                Address = new Address() { City = "Amsterdam", Country = "Netherlands", Email = "email@email.com", FirstName = "Anton", LastName = "Tib", PostalCode = "2342AE", Street = "Bijloplaan" },
                OrderDate = DateTime.UtcNow,
                Products = new List<Product> { new Product() { Description = "These are fresh oranges", ImageURL = "https://usepostmanforimageurlendpoint.com", Name = "Oranges", Price = "€500", Stock = 50 } },
               ShippingDate = DateTime.UtcNow.AddDays(2),
                UserId = Guid.NewGuid(),
            }, namingStrategy));
            return this;
        }
    }
    public class ReviewDTOExampleGenerator : OpenApiExample<CreateReviewDTO>
    {
        public override IOpenApiExample<CreateReviewDTO> Build(NamingStrategy namingStrategy = null)
        {
            Examples.Add(OpenApiExampleResolver.Resolve("Review Creation", new CreateReviewDTO()
            {
                 Message = "This product is amazing",
                 CreatedAt = DateTime.UtcNow,
                 Name = "Anonymous", 
                 Rating = 6.0, 
                 ProductId = Guid.NewGuid()
            }, namingStrategy));
            return this;
        }
    }
    public class UserDTOExampleGenerator : OpenApiExample<CreateUserDTO>
    {
        public override IOpenApiExample<CreateUserDTO> Build(NamingStrategy namingStrategy = null)
        {
            Examples.Add(OpenApiExampleResolver.Resolve("Product Creation", new CreateUserDTO() 
            { 
                Email = "email@email.com",
                Firstname = "Tim",
                Lastname = "Iker"
            },namingStrategy));
            return this;
        }
    }
    public class ImageToUrlDTOExampleGenerator : OpenApiExample<String>
    {
        public override IOpenApiExample<String> Build(NamingStrategy namingStrategy = null)
        {
            Examples.Add(OpenApiExampleResolver.Resolve("Product Creation", "https://usepostmanforimageurlendpoint.com", namingStrategy));
            return this;
        }
    }

}
