using System;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;
using Application.Events.Entity;
using Application.Events.Entity.Events;
using MassTransit;
using Microsoft.AspNetCore.Routing.Tree;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebApi.Controllers;
using WebApi.Models;
using ControllerContext = Microsoft.AspNetCore.Mvc.ControllerContext;

namespace WebApi.Handlers
{
    internal class MassTransitQueueConsomer : IConsumer<EntityCreated>
    {
        private readonly ILogger<MassTransitQueueConsomer> logger;
        private readonly ServiceNowTelevision _serviceNowTelevision;

        public MassTransitQueueConsomer(ILogger<MassTransitQueueConsomer> logger, ServiceNowTelevision serviceNowTelevision)
        {
            this.logger = logger;
            _serviceNowTelevision = serviceNowTelevision;
        }

        public async Task Consume(ConsumeContext<EntityCreated> context)
        {
            string URL = "https://dev37028.service-now.com/api/now/v1/table/alm_hardware";
            var television = context.Message.Payload;
            _serviceNowTelevision.CreateTelevisionOnService(URL,television);
        }
    }
}
           
