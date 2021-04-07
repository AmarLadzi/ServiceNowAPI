using System;
using System.Threading.Tasks;
using Application.Events.Entity.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using WebApi.Controllers;
using WebApi.Models;

namespace WebApi.Handlers
{
    public class IncidentConsumer: IConsumer<IncidentChanged>
    {
        public IncidentConsumer() { }
        public async Task Consume(ConsumeContext<IncidentChanged> context)
        {
            Console.WriteLine(context.Message);
        }
    }
}