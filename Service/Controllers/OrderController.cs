using Common;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        //readonly IPublishEndpoint _publishEndpoint;
        IRequestClient<CheckOrderStatus> _client;

        public OrderController(IRequestClient<CheckOrderStatus> client)//IPublishEndpoint publishEndpoint)
        {
            _client = client;
        }

        

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var response = await _client.GetResponse<OrderStatusResult>(new { OrderId = "order-id" });

            return Ok(response.Message);
        }
    }
}
