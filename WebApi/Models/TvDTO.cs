using Newtonsoft.Json.Linq;
using WebApi.Interfaces;

namespace WebApi.Models
{
    public class TvDTO:ITv
    {
        public int quantity;
        public string location;
        public string company;
        public string comments;
        public LinkObject model_category;
        public string display_name;
        public string model;
        public string serial_number;
        public int cost;
    }

}