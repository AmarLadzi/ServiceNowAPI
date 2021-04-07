using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using WindowsService.Triggers.AlarmInfo;
using Application.Events.Entity.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebApi.Controllers;
using WebApi.Models;

namespace WebApi.Handlers
{
    class AlarmsConsumer : IConsumer<AlarmChanged>
    {
        private readonly ServiceNowTicket _serviceNowTicket;

        public AlarmsConsumer(ServiceNowTicket serviceNowTicket)
        {
            _serviceNowTicket = serviceNowTicket;
        }

        public async Task Consume(ConsumeContext<AlarmChanged> context)
        {
            Console.WriteLine(context.Message.Payload);
            createTicket(context.Message);
        }

        void createTicket(AlarmChanged alarmChanged)
        {
            List<Incident> eList = new List<Incident>();
            Incident e = new Incident();
            Type type = alarmChanged.Payload.Alarm.GetType();
            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            PropertyInfo[] properties = type.GetProperties(flags);
            string description = "name->" + alarmChanged.Payload.Variable.Name + 
                                 "\nid->" + alarmChanged.Payload.Variable.id + 
                                 "\nentityName->" + alarmChanged.Payload.Variable.EntityTypeName +
                                 "\nentityTypeId->" + alarmChanged.Payload.Variable.EntityTypeId+"\n";
            string short_description = alarmChanged.Name;
            e.state = alarmChanged.Payload.Event.ToString();
            foreach (PropertyInfo property in properties)
            {
                description += property.Name + "->" +
                               property.GetValue(alarmChanged.Payload.Alarm, null) + "\n";
            }

            // e.number = alarmChanged.Payload.Variable.id.ToString();
            e.short_description = short_description;
            e.description = description;
            eList.Add(e);
            string DATA = JsonConvert.SerializeObject(eList, Formatting.Indented);

            _serviceNowTicket.CreateIncidentTicket(DATA);
        }
    }
}