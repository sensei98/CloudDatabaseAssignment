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
    public class UpdateReview
    {
        private readonly ILogger<UpdateReview> _logger;
        private readonly IReviewService _reviewService;

        public UpdateReview(ILogger<UpdateReview> logger, IReviewService reviewService)
        {
            _logger = logger;
            _reviewService = reviewService;
        }

        [FunctionName("UpdateReview")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Reviews" })]
        [OpenApiParameter(name: "reviewId", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Description = "The reviewId parameter")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(Review), Required = true, Description = "The review to be updated", Example = typeof(ReviewDTOExampleGenerator))]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Review not found")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NoContent, Description = "Review updated")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "reviews/{reviewId}")] HttpRequest req,string reviewId)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                Review review = JsonConvert.DeserializeObject<Review>(requestBody);

                await _reviewService.UpdateReviewById(Guid.Parse(reviewId), review);
                return new OkObjectResult(new { message = "Review has been updated" });
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
         
        }
    }
}
