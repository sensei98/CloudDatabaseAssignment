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


namespace OnlineStore.Functions.Products
{
    public class DeleteProduct
    {
        private readonly ILogger<DeleteProduct> _logger;
        private readonly IProductService _productService;
        public DeleteProduct(ILogger<DeleteProduct> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }


        [FunctionName("DeleteProduct")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Products" })]
        [OpenApiParameter(name: "productId", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Description = "The productId parameter")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NoContent, Description = "The product was deleted")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "products/{productId}")] HttpRequest req,string productId)
        {
            try
            {
                await _productService.DeleteSingleProductById(Guid.Parse(productId));
                return new OkObjectResult(new {message = "product has been deleted"});
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
           
        }
    }
}
