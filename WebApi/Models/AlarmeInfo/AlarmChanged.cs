using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService.Triggers.AlarmInfo
{
    class AlarmChanged
    {
        public string Name { get; set; }
        public AlarmInfoDto Payload { get; set; }
    }
}
