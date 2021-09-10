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
    public class ValuesController : ControllerBase
    {
        //readonly IPublishEndpoint _publishEndpoint;
        private ISendEndpointProvider _sendEndpointProvider;
        public ValuesController(ISendEndpointProvider sendEndpointProvider)//IPublishEndpoint publishEndpoint)
        {
            //_publishEndpoint = publishEndpoint;
            _sendEndpointProvider = sendEndpointProvider;
        }

        

        [HttpGet]
        public async Task<ActionResult> Get(string value)
        {
            /*await _publishEndpoint.Publish<ValueEntered>(new
            {
                Value = value
            });*/

            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:event-listener"));
            await sendEndpoint.Send<ValueEntered>(new { Value = value });
            return Ok();
        }



    }
}
