using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/ticket")]
    [ApiController]
    public class ServiceNow : ControllerBase
    {
        private readonly IMapper _mapper;

        public ServiceNow(IMapper mapper)
        {
            _mapper = mapper;
        }

        // GET
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


        [HttpPost]
        public IActionResult CreateOneIncidentFakeData()
        {
            string URL = "https://dev37028.service-now.com/api/now/v1/table/incident";
            string USERNAME = "addvals2";
            string PASSWORD = "00214521";
            string SHORT_DESCRIPTION = "Samyyyyyyyy";
            string COMMENTS = "Il n'existe pas de commentaire sur celle ci";

            // return CreateIncidentTicket(URL, USERNAME, PASSWORD, SHORT_DESCRIPTION, COMMENTS);
            return Ok();
        }

        [HttpGet("rabbitmq/")]
        public IActionResult CreateOneIncidentFromQueue()
        {
            return Ok();
        }

        public IActionResult CreateIncidentTicket(string URL)
        {
            List<Incident> eList = new List<Incident>();
            Incident e = new Incident();
            // e.number = bosObject.id.ToString();
            // e.short_description = bosObject.description;
            // e.description = bosObject.name;

            eList.Add(e);

            string DATA = JsonConvert.SerializeObject(eList, Formatting.Indented);

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
            string description = string.Empty;
            Console.WriteLine("Result code is {0} with message {1}", messge.IsSuccessStatusCode, messge.StatusCode);
            if (messge.IsSuccessStatusCode)
            {
                string result = messge.Content.ReadAsStringAsync().Result;
                return Ok(result);
            }

            return Content("erreur");
        }
    }
}