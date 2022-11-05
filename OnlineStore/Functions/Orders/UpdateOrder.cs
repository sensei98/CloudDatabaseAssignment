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
using OnlineStore.Model;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System.Net;
using Microsoft.OpenApi.Models;
using OnlineStore.DTO;


namespace OnlineStore.Functions.Orders
{
    public class UpdateOrder
    {
        private readonly ILogger<UpdateOrder> _logger;
        private readonly IOrderService _orderService;
        public UpdateOrder(IOrderService orderService, ILogger<UpdateOrder> logger)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [FunctionName("UpdateOrder")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Orders" })]
        [OpenApiParameter(name: "orderId", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Description = "The orderId parameter")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(Order), Required = true, Description = "The order to be updated", Example = typeof(OrderDTOExampleGenerator))]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Order not found")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NoContent, Description = "Order updated")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "orders/{orderId}")] HttpRequest req,string orderId)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                Order order = JsonConvert.DeserializeObject<Order>(requestBody);

                await _orderService.UpdateOrderById(Guid.Parse(orderId), order);
                return new OkObjectResult(new { message = "Order has been updated" });
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
           
        }
    }
}
