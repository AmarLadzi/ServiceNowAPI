using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService.Triggers
{
    public class AlarmInfoDto
    {
        public AlarmInfoDto()
        {
            Alarm = new AlarmDto();
            Variable = new VariableRowDto();
        }

        public AlarmDto Alarm { get; set; }
        public VariableRowDto Variable { get; set; }
        public EventEnumDto Event { get; set; }
    }
}
