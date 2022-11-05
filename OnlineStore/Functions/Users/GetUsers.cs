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
    public class GetUsers
    {
        private readonly ILogger<GetUsers> _logger;
        private readonly IUserService _userService;
        public GetUsers(ILogger<GetUsers> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [FunctionName(nameof(GetUsers))]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Users" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<User>), Description = "The OK response", Example = typeof(UserDTOExampleGenerator))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "Bad request body")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users")] HttpRequest req)
        {
            try
            {
                var users = await _userService.GetUsers();
                return new OkObjectResult(users);
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }

          
        }
    }
}
