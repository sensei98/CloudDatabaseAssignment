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

namespace OnlineStore.Functions.Orders
{
    public class GetOrders
    {
        private readonly ILogger<GetOrders> _logger;
        private readonly IOrderService _orderService;
        public GetOrders(IOrderService orderService, ILogger<GetOrders> logger)
        {
            _logger = logger;
            _orderService = orderService;
        }
        [FunctionName("GetOrders")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Orders" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Order>), Description = "The OK response", Example = typeof(OrderDTOExampleGenerator))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "Bad request body")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "orders")] HttpRequest req)
        {
            try
            {
                var orders = await _orderService.GetOrders();
                return new OkObjectResult(orders);

            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
           
        }
    }
}
