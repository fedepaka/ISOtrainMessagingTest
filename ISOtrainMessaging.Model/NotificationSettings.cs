using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISOtrainMessaging.Model
{
    public class NotificationSettings
    {
        public SmtpSettings Smtp { get; set; } = new();
        public PushSettings Push { get; set; } = new();
    }
}
