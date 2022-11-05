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

namespace OnlineStore.Functions.Orders
{
    public class DeleteOrder
    {
        private readonly ILogger<DeleteOrder> _logger;
        private readonly IOrderService _orderService;
        public DeleteOrder(IOrderService orderService, ILogger<DeleteOrder> logger)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [FunctionName("DeleteOrder")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Orders" })]
        [OpenApiParameter(name: "orderId", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Description = "The deleteId parameter")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NoContent, Description = "The order was deleted")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "orders/{orderId}")] HttpRequest req, string orderId)
        {
            try
            {
                await _orderService.DeleteSingleOrderById(Guid.Parse(orderId));
                return new OkObjectResult(new { message = "Order has been deleted" });
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
           
        }
    }
}
