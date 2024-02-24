using Exiled.API.Features;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utf8Json.Internal.DoubleConversion;

namespace VipBroadcast
{
    public class VipBroadcast : Plugin<Config>
    {
        public override string Author => "noahxo";
        public override string Name => "VipBroadcast";
        public override string Prefix => Name;
        public override Version Version => new Version(1, 1, 0);

        public static VipBroadcast Instance;

        public static Dictionary<Player, DateTime> playerCooldowns;

        public List<Player> keysToRemove;

        public override void OnEnabled()
        {
            Instance = this;

            playerCooldowns = new Dictionary<Player, DateTime>();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = null;

            playerCooldowns = null;

            base.OnDisabled();
        }

        public bool IsOnCooldown(Player sender, out double remainingSeconds)
        {
            if (playerCooldowns.TryGetValue(sender, out var expiration) && expiration > DateTime.UtcNow) 
            {
                remainingSeconds = (expiration - DateTime.UtcNow).TotalSeconds;
                return true;
            }

            remainingSeconds = 0;
            return false;
        }

        public void PutOnCooldown(Player key, TimeSpan duration)
        {
            playerCooldowns[key] = DateTime.UtcNow + duration;
        }
    }
}
