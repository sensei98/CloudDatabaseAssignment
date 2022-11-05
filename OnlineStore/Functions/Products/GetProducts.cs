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
using OnlineStore.Model;
using OnlineStore.DTO;
using System.Collections.Generic;

namespace OnlineStore.Functions.Products
{
    public class GetProducts
    {
        private readonly ILogger<GetProducts> _logger;
        private readonly IProductService _productService;
        public GetProducts(ILogger<GetProducts> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }
        [FunctionName("GetProducts")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Products" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Product>), Description = "The OK response", Example = typeof(CreateProductDTOExampleGenerator))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "Bad request body")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "products")] HttpRequest req)
        {
            try
            {
                var products = await _productService.GetProducts();
                return new OkObjectResult(products);

            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }

        }
    }
}
