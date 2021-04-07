using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService.Triggers.EventRow
{
    class EventChanged
    {
        public string Name { get; set; }
        public EventRowDto Payload { get; set; }
    }
}
