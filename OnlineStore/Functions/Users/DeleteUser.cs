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


namespace OnlineStore.Functions.Users
{
    public class DeleteUser
    {
        private readonly ILogger<DeleteUser> _logger;
        private readonly IUserService _userService;
        public DeleteUser(ILogger<DeleteUser> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [FunctionName(nameof(DeleteUser))]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Users" })]
        [OpenApiParameter(name: "userId", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Description = "The userId parameter")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NoContent, Description = "The user was deleted")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "users/{userId}")] HttpRequest req,string userId)
        {
            try
            {
                await _userService.DeleteSingleUserById(Guid.Parse(userId));
                return new OkObjectResult(new {message = "User has been deleted"});
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }

            
        }
    }
}
