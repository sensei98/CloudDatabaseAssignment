using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OnlineStore.Model;
using OnlineStore.Interface;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using System.Net;
using OnlineStore.DTO;

namespace OnlineStore.Functions.Orders
{
    public class AddOrder
    {
        private readonly ILogger<AddOrder> _logger;
        private readonly IOrderService _orderService;
        public AddOrder(IOrderService orderService, ILogger<AddOrder> logger)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [FunctionName("AddOrder")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Orders" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(OrderDTOExampleGenerator), Required = true, Description = "The JSON of the Order", Example = typeof(OrderDTOExampleGenerator))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CreateOrderDTO), Description = "Order created", Example = typeof(OrderResponseCreatedExampleGenerator))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.NotFound, contentType: "application/json", bodyType: typeof(NotFoundMessage), Description = "Order cannot be created", Example = typeof(OrderNotFoundExampleGenerator))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "orders")] HttpRequest req)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                CreateOrderDTO order = JsonConvert.DeserializeObject<CreateOrderDTO>(requestBody);

                await _orderService.AddOrder(order);
                return new OkObjectResult(new { message = "Order has been added" });
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
          
        }
    }
}
