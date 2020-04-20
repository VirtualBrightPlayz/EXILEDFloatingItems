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
        public static float minForce;
        public static float maxForce;
        public static float maxOffset;

        public override void OnDisable()
        {
            if (!Config.GetBool("floating_items"))
                return;
            inst.UnpatchAll();
        }

        public override void OnEnable()
        {
            if (!Config.GetBool("floating_items"))
                return;
            minForce = Config.GetFloat("fitem_force_min", 5.0f);
            maxForce = Config.GetFloat("fitem_force_max", 10.0f);
            maxOffset = Config.GetFloat("fitem_offset_max", 2.0f);
            inst = HarmonyInstance.Create("virtualbrightplayz.exiledfloatingitems");
            inst.PatchAll();
        }

        public override void OnReload()
        {
            if (!Config.GetBool("floating_items"))
                return;
            minForce = Config.GetFloat("fitem_force_min", 5.0f);
            maxForce = Config.GetFloat("fitem_force_max", 10.0f);
            maxOffset = Config.GetFloat("fitem_offset_max", 2.0f);
            inst = HarmonyInstance.Create("virtualbrightplayz.exiledfloatingitems");
            inst.PatchAll();
        }
    }
}
