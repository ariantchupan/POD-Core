using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class SendVerifyEvent : IntegrationBaseEvent
    {
        public string MobileNumber { get; set; }
        public string Code { get; set; }
    }
}
