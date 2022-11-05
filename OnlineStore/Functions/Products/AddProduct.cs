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
    public class AddProduct
    {
        private readonly ILogger<AddProduct> _logger;
        private readonly IProductService _productService;
        public AddProduct(ILogger<AddProduct> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [FunctionName("AddProduct")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Products" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CreateProductDTOExampleGenerator), Required = true, Description = "The JSON of the Product", Example = typeof(CreateProductDTOExampleGenerator))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CreateProductDTO), Description = "Product created", Example = typeof(ProductResponseCreatedExampleGenerator))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.NotFound, contentType: "application/json", bodyType: typeof(NotFoundMessage), Description = "Product cannot be created", Example = typeof(ProductNotFoundExampleGenerator))] 
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "products")] HttpRequest req)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                CreateProductDTO product = JsonConvert.DeserializeObject<CreateProductDTO>(requestBody);

                await _productService.AddProduct(product);

                return new OkObjectResult(new { message ="Product has been added"});
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
          


        }
    }
}
