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

namespace OnlineStore.Functions.Users
{
    public class UpdateUser
    {
        private readonly ILogger<UpdateUser> _logger;
        private readonly IUserService _userService;
        public UpdateUser(ILogger<UpdateUser> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [FunctionName(nameof(UpdateUser))]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Users" })]
        [OpenApiParameter(name: "userId", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Description = "The userId parameter")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(User), Required = true, Description = "The user to be updated", Example = typeof(UserDTOExampleGenerator))]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "User not found")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NoContent, Description = "User updated")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "users/{userId}")] HttpRequest req,string userId)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                User user = JsonConvert.DeserializeObject<User>(requestBody);

                await _userService.UpdateUserById(Guid.Parse(userId), user);
                return new OkObjectResult(new { message = "User has been updated"});
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }

        }
    }
}
