using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OnlineStore.Service;
using OnlineStore.Model;
using OnlineStore.Interface;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System.Net;
using Microsoft.OpenApi.Models;
using OnlineStore.DTO;

namespace OnlineStore.Functions.Users
{
    public class AddUser
    {
        private readonly ILogger<AddUser> _logger;
        private readonly IUserService _userService;
        public AddUser(ILogger<AddUser> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [FunctionName(nameof(AddUser))]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Users" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(UserDTOExampleGenerator), Required = true, Description = "The JSON of the User", Example = typeof(UserDTOExampleGenerator))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CreateUserDTO), Description = "User created", Example = typeof(UserResponseCreatedExampleGenerator))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.NotFound, contentType: "application/json", bodyType: typeof(NotFoundMessage), Description = "User cannot be created", Example = typeof(UserNotFoundExampleGenerator))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "users")] HttpRequest req)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                CreateUserDTO user = JsonConvert.DeserializeObject<CreateUserDTO>(requestBody);

                await _userService.AddUser(user);

                return new OkObjectResult(new { message = "User has been added" });
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
           
        }
    }
}
