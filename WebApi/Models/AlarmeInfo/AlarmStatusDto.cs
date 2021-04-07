using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService.Triggers.AlarmInfo
{
    public enum AlarmStatusDto
    {
        None = 0,
        AcknowledgeWaiting = 10,
        Processing = 20,
        ProcessWaiting = 30,
        Closed = 100,
    }
}
