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

        public static VipBroadcast Instance;

        public static Dictionary<Player, int> playerCooldowns = new Dictionary<Player, int>();

        public List<Player> keysToRemove;

        public override void OnEnabled()
        {
            Instance = this;

            Timing.RunCoroutine(CooldownChecker());

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = null;

            Timing.KillCoroutines();

            base.OnDisabled();
        }

        private IEnumerator<float> CooldownChecker()
        {
            for (;;)
            {
                yield return Timing.WaitForSeconds(1f);

                // to list so we can modify the collection concurrently
                foreach (KeyValuePair<Player, int> playerCooldown in playerCooldowns.ToList())
                {
                    playerCooldowns[playerCooldown.Key]--;

                    if (playerCooldowns[playerCooldown.Key] == 0)
                    {
                        playerCooldowns.Remove(playerCooldown.Key);
                    }
                }
            }
        }
    }
}
