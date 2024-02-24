using CommandSystem;
using Exiled.API.Features;
using NorthwoodLib.Pools;
using Exiled.Permissions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEC;

namespace VipBroadcast
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class VipBroadcastCommand : ICommand
    {
        public string Command => "vipBroadcast";
        public string[] Aliases => ["vbc"];
        public string Description => "Allows certain people to broadcast to everyone with a cooldown.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("vb.vipbroadcast"))
            {
                response = "You are missing the permissions to VIP Broadcast.";
                return false;
            }

            if (arguments.Count == 0)
            {
                response = "You didn't specify a broadcast to send. Use .vbc <text>";
                return false;
            }

            if (VipBroadcast.Instance.IsOnCooldown(Player.Get(sender), out double remainingSeconds))
            {
                response = "You are still on cooldown for this command. Time left: " + (int)remainingSeconds;
                return false;
            }

            VipBroadcast.Instance.PutOnCooldown(Player.Get(sender), TimeSpan.FromSeconds(VipBroadcast.Instance.Config.Cooldown));

            Map.Broadcast(VipBroadcast.Instance.Config.Duration, VipBroadcast.Instance.Config.Message.Replace("%message%", FormatArguments(arguments, 0)).Replace("%player%", Player.Get(sender).Nickname), Broadcast.BroadcastFlags.Normal, false);

            response = "Broadcast sent!";
            return true;
        }

        private static string FormatArguments(ArraySegment<string> sentence, int index)
        {
            StringBuilder SB = StringBuilderPool.Shared.Rent();
            foreach (string word in sentence.Segment(index))
            {
                SB.Append(word);
                SB.Append(" ");
            }
            string msg = SB.ToString().Trim();
            StringBuilderPool.Shared.Return(SB);
            return msg;
        }

    }
}
