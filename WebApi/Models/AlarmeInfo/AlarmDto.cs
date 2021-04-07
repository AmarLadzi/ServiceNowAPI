using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsService.Triggers.AlarmInfo;

namespace WindowsService.Triggers
{
    public class AlarmDto
    {
        public int Count { get; set; }
        public DateTimeOffset DateAck { get; set; }
        public DateTimeOffset DateEnd { get; set; }
        public DateTimeOffset DateReceive { get; set; }
        public DateTimeOffset DateReport { get; set; }
        public DateTimeOffset DateStart { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public bool HasWorkflow { get; set; }
        public long Id { get; set; }
        public bool IsAlarmState { get; set; }
        public string OriginDesc { get; set; }
        public string Report { get; set; }
        public int Severity { get; set; }
        public string SourceName { get; set; }
        public int SourceId { get; set; }
        public string StateDesc { get; set; }
        public AlarmStatusDto Status { get; set; }
        public AlarmTypeDto Type { get; set; }
        public int? UserIdAck { get; set; }
        public int? UserIdReport { get; set; }
        public string UserNameAck { get; set; }
        public string UserNameReport { get; set; }
        public bool WaitAcknowledge { get; set; }
        public bool WaitEnd { get; set; }
        public bool? WaitProcess { get; set; }
        public bool WaitReport { get; set; }

    }
}
