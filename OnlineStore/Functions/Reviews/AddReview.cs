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

namespace OnlineStore.Functions.Reviews
{
    public class AddReview
    {
        private readonly ILogger<AddReview> _logger;
        private readonly IReviewService _reviewService;

        public AddReview(ILogger<AddReview> logger, IReviewService reviewService)
        {
            _logger = logger;
            _reviewService = reviewService;
        }


        [FunctionName("AddReview")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Reviews" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(ReviewDTOExampleGenerator), Required = true, Description = "The JSON of the User", Example = typeof(ReviewDTOExampleGenerator))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CreateReviewDTO), Description = "Review created", Example = typeof(ReviewResponseCreatedExampleGenerator))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.NotFound, contentType: "application/json", bodyType: typeof(NotFoundMessage), Description = "Review cannot be created", Example = typeof(ReviewNotFoundExampleGenerator))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "reviews")] HttpRequest req)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                CreateReviewDTO review = JsonConvert.DeserializeObject<CreateReviewDTO>(requestBody);

                await _reviewService.AddReview(review);
                return new OkObjectResult(new {message = "A review has been added"});
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
            
        }
    }
}
