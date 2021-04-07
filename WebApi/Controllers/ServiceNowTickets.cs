using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WindowsService.Triggers;
using WindowsService.Triggers.AlarmInfo;
using Application.Events.Entity.Events;
using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/")]
    [ApiController]
    public class ServiceNowTicket : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBus _ibus;

        public ServiceNowTicket(IMapper mapper, IBus ibus)
        {
            _mapper = mapper;
            _ibus = ibus;
        }

        //faire une requête vers servicenow pour recevoir une liste de ticket déjà créer
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Incident incident;
            using (HttpClient client = new HttpClient())
            {
                var plainTextBytes = Encoding.UTF8.GetBytes("addvals2:00214521");
                string val = Convert.ToBase64String(plainTextBytes);
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + val);
                using (HttpResponseMessage res =
                    await client.GetAsync("https://dev37028.service-now.com/api/now/table/incident?sysparm_limit=1"))
                using (HttpContent content = res.Content)
                {
                    JObject unautre = await content.ReadAsAsync<JObject>();
                    incident = JsonConvert.DeserializeObject<Incident>(unautre["result"][0].ToString());
                }
            }

            IncidentDTO salut = _mapper.Map<IncidentDTO>(incident);
            return Ok(JsonConvert.SerializeObject(salut));
        }

        //requête pour envoyer un ticker sur la queue lorsque servicenow créer un ticket
        // [HttpPost("fromservicenow/sendTicket")]
        // public async Task  sendIncident(JsonElement jobject)
        // {
        //     IncidentDTO incident = JsonConvert.DeserializeObject<IncidentDTO>(jobject.ToString());
        //     await _ibus.Publish(incident);
        // }
        [HttpPost("fromservicenow/sendTicketUpdated")]
        public async Task sendIncidentUpdated(JsonElement jobject)
        {
            var incident = JsonConvert.DeserializeObject<IncidentDTO>(jobject.ToString());
            // var array = incident.description.Split("\n").Select(x => x.Split("->")).ToList();
            // incident.VariableRowDto = new VariableRowDto()
            // {
            //     id = Guid.Parse(array[1][1]),
            //     Name = array[0][1],
            //     EntityTypeId = Guid.Parse(array[3][1]),
            //     EntityTypeName = array[2][1]
            // };
            IncidentChanged incidentChanged = new IncidentChanged() {Name = "IncidentChanged", Payload = incident};
            await _ibus.Publish(incidentChanged);
        }

        [HttpPost("fromservicenow/sendTicket")]
        public async Task sendIncident(JsonElement jobject)
        {
            var incident = JsonConvert.DeserializeObject<IncidentDTO>(jobject.ToString());
            IncidentChanged incidentChanged = new IncidentChanged() {Name = "IncidentChanged", Payload = incident};
            await _ibus.Publish(incidentChanged);
        }

        //créer un ticket depuis une requête externe vers servicenow 
        [HttpPost("create/ticket/")]
        public IActionResult CreateIncidentTicket(string DATA)
        {
            var URL = "https://dev37028.service-now.com/api/now/v1/table/incident";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            // byte[] cred = UTF8Encoding.UTF8.GetBytes($"{USERNAME}:{PASSWORD}");
            byte[] cred = UTF8Encoding.UTF8.GetBytes($"addvals2:00214521");
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(cred));
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
            HttpResponseMessage messge = client.PostAsync(URL, content).Result;
            Console.WriteLine("Result code is {0} with message {1}", messge.IsSuccessStatusCode, messge.StatusCode);
            if (messge.IsSuccessStatusCode)
            {
                string result = messge.Content.ReadAsStringAsync().Result;
                return Ok(result);
            }

            return Content("Oh oui");
        }
    }
}