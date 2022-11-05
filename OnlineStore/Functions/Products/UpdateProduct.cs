using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OnlineStore.Interface;
using OnlineStore.Model;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System.Net;
using Microsoft.OpenApi.Models;
using OnlineStore.DTO;

namespace OnlineStore.Functions.Products
{
    public class UpdateProduct
    {

        private readonly ILogger<UpdateProduct> _logger;
        private readonly IProductService _productService;
        public UpdateProduct(ILogger<UpdateProduct> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [FunctionName("UpdateProduct")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Products" })]
        [OpenApiParameter(name: "productId", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Description = "The productId parameter")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(Review), Required = true, Description = "The product to be updated", Example = typeof(CreateProductDTOExampleGenerator))]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Product not found")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NoContent, Description = "Product updated")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "products/{productId}")] HttpRequest req,string productId)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                Product product = JsonConvert.DeserializeObject<Product>(requestBody);

                await _productService.UpdateProductById(Guid.Parse(productId), product);
                return new OkObjectResult(new { message = "Product has been updated" });

            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
           
        }
    }
}
