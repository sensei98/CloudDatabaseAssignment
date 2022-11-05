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
using OnlineStore.DTO;
using OnlineStore.Model;
using System.Collections.Generic;

namespace OnlineStore.Functions.Reviews
{
    public class GetReviews
    {
        private readonly ILogger<GetReviews> _logger;
        private readonly IReviewService _reviewService;

        public GetReviews(ILogger<GetReviews> logger, IReviewService reviewService)
        {
            _logger = logger;
            _reviewService = reviewService;
        }

        [FunctionName("GetReviews")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Reviews" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Review>), Description = "The OK response", Example = typeof(ReviewDTOExampleGenerator))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "Bad request body")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "reviews")] HttpRequest req)
        {
            try
            {
                var reviews = await _reviewService.GetReviews();
                return new OkObjectResult(reviews);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
            
        }
    }
}
