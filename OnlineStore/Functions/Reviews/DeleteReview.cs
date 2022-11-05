using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System.Net;
using Microsoft.OpenApi.Models;

using OnlineStore.Interface;

namespace OnlineStore.Functions.Reviews
{
    public class DeleteReview
    {
        private readonly ILogger<DeleteReview> _logger;
        private readonly IReviewService _reviewService;

        public DeleteReview(ILogger<DeleteReview> logger, IReviewService reviewService)
        {
            _logger = logger;
            _reviewService = reviewService;
        }

        [FunctionName("DeleteReview")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Reviews" })]
        [OpenApiParameter(name: "reviewId", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Description = "The reviewId parameter")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NoContent, Description = "The review was deleted")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "reviews/{reviewId}")] HttpRequest req,string reviewId)
        {
            try
            {
                await _reviewService.DeleteSingleReviewById(Guid.Parse(reviewId));
                return new OkObjectResult(new {message = "Review has been deleted"});
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
            
        }
    }
}
