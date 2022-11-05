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
    public class GetOrderById
    {
        private readonly ILogger<GetOrderById> _logger;
        private readonly IOrderService _orderService;
        public GetOrderById(IOrderService orderService, ILogger<GetOrderById> logger)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [FunctionName("GetOrderById")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Orders" })]
        [OpenApiParameter(name: "orderId", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Description = "The logbookId parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Order>), Description = "The requested order info", Example = typeof(OrderDTOExampleGenerator))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "orders/{orderId}")] HttpRequest req,string orderId)
        {
            try
            {
                var order = await _orderService.GetSingleOrderById(Guid.Parse(orderId));
                return new OkObjectResult(order);
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
           
        }
    }
}
