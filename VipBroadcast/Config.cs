using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VipBroadcast
{
    public class Config : IConfig
    {
        [Description("Whether or not the plugin is enabled.")]
        public bool IsEnabled { get; set; } = true;
        [Description("Whether or not debug messages should be shown.")]
        public bool Debug { get; set; } = false;
        [Description("How long the cooldown for making broadcasts is.")]
        public int Cooldown { get; set; } = 90;
        [Description("The duration of the broadcast sent.")]
        public ushort Duration { get; set; } = 10;
        [Description("How the broadcast should be formatted. Use %message% for their message, and %player% for their nickname.")]
        public string Message { get; set; } = "<b><color=yellow>[VIP Broadcast]</color></b> \"%message%\"\n<i>- %player%</i>";
    }
}
