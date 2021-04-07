using WindowsService.Triggers;
using MassTransit.Contracts.Metadata;

namespace WebApi.Models
{
    public class IncidentDTO
    {
        public string number { get; set; }
        public string category { get; set; }
        public string short_description { get; set; }
        public string description { get; set; }
        public string urgency { get; set; }
        public string priority { get; set; }
        public string impact { get; set; }
        public string state { get; set; }
        // public VariableRowDto VariableRowDto { get; set; }
    }
}