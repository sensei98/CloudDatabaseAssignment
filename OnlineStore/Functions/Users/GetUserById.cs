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

namespace OnlineStore.Functions.Users
{
    public class GetUserById
    {
        private readonly ILogger<GetUserById> _logger;
        private readonly IUserService _userService;
        public GetUserById(ILogger<GetUserById> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [FunctionName(nameof(GetUserById))]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Users" })]
        [OpenApiParameter(name: "userId", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Description = "The userId parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<User>), Description = "The requested user info", Example = typeof(UserDTOExampleGenerator))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users/{userId}")] HttpRequest req,string userId)
        {
            try
            {
                var user = await _userService.GetSingleUserById(Guid.Parse(userId));
                return new OkObjectResult(user);
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }

           
        }
    }
}
