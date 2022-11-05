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
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System.Net;
using Microsoft.OpenApi.Models;
using OnlineStore.DTO;
using OnlineStore.Model;
using System.Collections.Generic;

namespace OnlineStore.Functions.Products
{
    public class GetProductById
    {
        private readonly ILogger<GetProductById> _logger;
        private readonly IProductService _productService;
        public GetProductById(ILogger<GetProductById> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [FunctionName("GetProductById")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Products" })]
        [OpenApiParameter(name: "productId", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Description = "The productId parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Product>), Description = "The requested product info", Example = typeof(CreateProductDTOExampleGenerator))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "products/{productId}")] HttpRequest req,string productId)
        {
            try
            {
                var product = await _productService.GetSingleProductById(Guid.Parse(productId));
                return new OkObjectResult(product);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
           
        }
    }
}
