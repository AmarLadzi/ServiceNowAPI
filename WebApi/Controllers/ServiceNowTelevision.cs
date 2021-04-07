using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Application.Events.Entity;
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
    [Route("api/television")]
    [ApiController]
    public class ServiceNowTelevision : ControllerBase
    {
        public ServiceNowTelevision()
        {
        }

        [HttpPost]
        public IActionResult CreateTelevisionOnService(string URL, EntityDto television)
        {
            string url = "https://dev37028.service-now.com/api/now/v1/table/alm_hardware";
            var eList = new List<TvDTO>();
            TvDTO _tvDto = new TvDTO();
            _tvDto.cost = 23445;
            _tvDto.quantity = 1;
            _tvDto.number = Guid.NewGuid().ToString();
            _tvDto.serial_number = "BQP-854-0000246-GH";
            // _tvDto.model = "Gateway DX Series";
            // string commentaire = "";
            // foreach (var element in television.CustomAttributes)
            // {
            // commentaire += $"{element.Name}: {element.StringValue} \n";
            // }

            // Console.WriteLine("voici les commentaire" + commentaire);
            // _tvDto.comments = commentaire;
            eList.Add(_tvDto);

            string DATA = JsonConvert.SerializeObject(eList, Formatting.Indented);

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            byte[] cred = UTF8Encoding.UTF8.GetBytes($"addvals2:00214521");
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(cred));
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
            HttpResponseMessage messge = client.PostAsync(url, content).Result;
            Console.WriteLine("Result code is {0} with message {1}", messge.IsSuccessStatusCode, messge.StatusCode);
            if (messge.IsSuccessStatusCode)
            {
                string result = messge.Content.ReadAsStringAsync().Result;
                return Ok("ok c'est bon");
            }

            return Content("erreur quelquconque");
        }
    }
}