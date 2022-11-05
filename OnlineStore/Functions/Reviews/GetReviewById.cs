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

namespace OnlineStore.Functions.Reviews
{
    public class GetReviewById
    {
        private readonly ILogger<GetReviewById> _logger;
        private readonly IReviewService _reviewService;

        public GetReviewById(ILogger<GetReviewById> logger, IReviewService reviewService)
        {
            _logger = logger;
            _reviewService = reviewService;
        }

        [FunctionName("GetReviewById")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Reviews" })]
        [OpenApiParameter(name: "reviewId", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Description = "The reviewId parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Review>), Description = "The requested review info", Example = typeof(ReviewDTOExampleGenerator))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "reviews/{reviewId}")] HttpRequest req,string reviewId)
        {
            try
            {
                var review = await _reviewService.GetSingleReviewById(Guid.Parse(reviewId));
                return new OkObjectResult(review);
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
           
        }
    }
}
