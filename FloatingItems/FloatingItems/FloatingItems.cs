using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using Harmony;

namespace FloatingItems
{
    public class FloatingItems : Plugin
    {
        public override string getName => "FloatingItems";
        public HarmonyInstance inst;
        public FIEvents PLEV;
        public static float minForce;
        public static float maxForce;
        public static float maxOffset;
        public static float forceShoot;
        public static float rangeShoot;
        public static List<string> allowFloatDrops;

        public override void OnDisable()
        {
            if (!Config.GetBool("floating_items", true))
                    return;
            inst.UnpatchAll();
            inst = null;
            Events.ItemDroppedEvent -= PLEV.ItemDropped;
            Events.RoundStartEvent -= PLEV.RoundStart;
            Events.ConsoleCommandEvent -= PLEV.PlayerCmd;
            Events.ShootEvent -= PLEV.PlayerShoot;
            PLEV = null;
        }

        public override void OnEnable()
        {
            if (!Config.GetBool("floating_items", true))
                    return;
            ReloadConfig();
            inst = HarmonyInstance.Create("virtualbrightplayz.exiledfloatingitems");
            inst.PatchAll();
            PLEV = new FIEvents();
            Events.ItemDroppedEvent += PLEV.ItemDropped;
            Events.RoundStartEvent += PLEV.RoundStart;
            Events.ConsoleCommandEvent += PLEV.PlayerCmd;
            Events.ShootEvent += PLEV.PlayerShoot;
        }

        public void ReloadConfig()
        {
            minForce = Config.GetFloat("fitem_force_min", 50.0f);
            maxForce = Config.GetFloat("fitem_force_max", 100.0f);
            maxOffset = Config.GetFloat("fitem_offset_max", 2.0f);
            forceShoot = Config.GetFloat("sitem_force", 100.0f);
            rangeShoot = Config.GetFloat("sitem_range", 3.0f);
            try
            {
                allowFloatDrops = Config.GetStringList("fitem_userid_whitelist");
            }
            catch (Exception)
            { }
            if (allowFloatDrops == null)
            {
                allowFloatDrops = new List<string>();
            }
        }

        public override void OnReload()
        {
            if (!Config.GetBool("floating_items", true))
                return;
            ReloadConfig();
        }
    }
}
