using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService.Triggers.EventRow
{
    class EventRowDto
    {
        public DateTimeOffset Date { get; set; }
        public DateTimeOffset DateReceive { get; set; }
        public string Description { get; set; }
        public long Id { get; set; }
        public string Info { get; set; }
        public bool IsUserMessage { get; set; }
        public string Report { get; set; }
        public string StateDesc { get; set; }
        public EventSubTypeDto SubType { get; set; }
        public EventTypeDto Type { get; set; }
        public int Value { get; set; }
        public string ValueString { get; set; }

    }
}
