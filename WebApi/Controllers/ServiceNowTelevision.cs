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
        // private  TvDTO _tvDto; comprendre pourquoi l'injection de dépendance ne fonctionne pas 

        public ServiceNowTelevision()
        {
            // _tvDto = tvDto;
        }

        [HttpPost]
        public IActionResult CreateTelevisionOnService(string URL, EntityDto television)
        {
            var eList = new List<TvDTO>();
            TvDTO _tvDto = new TvDTO();
            LinkObject linkObject = new LinkObject();
            linkObject.link =
                "https://dev37028.service-now.com/api/now/table/cmdb_model_category/81feb9c137101000deeabfc8bcbe5dc4";
            linkObject.value = "81feb9c137101000deeabfc8bcbe5dc4";
            _tvDto.model_category = linkObject;
            _tvDto.cost = 991;
            _tvDto.quantity = 1;
            _tvDto.serial_number = "BQP-854-0000246-GH";
            _tvDto.display_name = "Televiseur";
            //TODO trouver un moyen de mettre la company et la location à partir du guid fournit 
            // _tvDto.company = "QLED";
            // _tvDto.location = "QLED";
            _tvDto.model = "Gateway DX Series";
            string commentaire = "";
            foreach (var element in television.CustomAttributes)
            {
                commentaire += $"{element.Name}: {element.StringValue} \n";
            }

            Console.WriteLine("voici les commentaire" + commentaire);
            _tvDto.comments = commentaire;
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
            HttpResponseMessage messge = client.PostAsync(URL, content).Result;
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