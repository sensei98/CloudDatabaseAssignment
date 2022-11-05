using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using HttpMultipartParser;
using OnlineStore.Interface;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System.Net;
using Microsoft.OpenApi.Models;


namespace OnlineStore.Functions.Products
{
    public class ImageUrlGenerator
    {
        private readonly ILogger<ImageUrlGenerator> _logger;
        private readonly IProductService _productService;
        public ImageUrlGenerator(ILogger<ImageUrlGenerator> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [FunctionName("ImageUrlGenerator")]
        //using postman for this request, to generate a URL for product image
        //formdata key = 'image'
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "products/image")] HttpRequest req)
        {
            var parsedFormBody = MultipartFormDataParser.ParseAsync(req.Body);
            FilePart file = parsedFormBody.Result.Files[0];
            if (file == null)
            {
                throw new Exception("Add image as form data type");
            }

            try
            {
                var ImageUrl = await _productService.AddImageToProduct(file);
                return new OkObjectResult(ImageUrl);
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
            
        }
    }
}
