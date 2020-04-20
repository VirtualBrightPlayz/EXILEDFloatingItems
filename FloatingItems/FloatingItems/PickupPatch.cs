using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony;
using UnityEngine;

namespace FloatingItems
{
    [HarmonyPatch(typeof(Pickup), nameof(Pickup.LateUpdate))]
    public class PickupUpdatePatch
    {
        public static List<Pickup> haveBeenMoved = new List<Pickup>();
        public static List<Pickup> canBeFloating = new List<Pickup>();

        public static void Postfix(Pickup __instance)
        {
            if (!canBeFloating.Contains(__instance))
                return;
            __instance.Rb.useGravity = false;
            __instance.GetComponent<Collider>().material = new PhysicMaterial() { bounciness = 100f, bounceCombine = PhysicMaterialCombine.Maximum };
            if (haveBeenMoved.Contains(__instance))
                return;
            float p1 = UnityEngine.Random.Range(-FloatingItems.maxOffset, FloatingItems.maxOffset);
            float p2 = UnityEngine.Random.Range(-FloatingItems.maxOffset, FloatingItems.maxOffset);
            float p3 = UnityEngine.Random.Range(-FloatingItems.maxOffset, FloatingItems.maxOffset);
            Vector3 v3 = new Vector3(p1, p2, p3);
            float rng = UnityEngine.Random.Range(FloatingItems.minForce, FloatingItems.maxForce);
            __instance.Rb.AddExplosionForce(rng, __instance.Rb.position + v3, 10f);
            haveBeenMoved.Add(__instance);
        }
    }

    /*[HarmonyPatch(typeof(Pickup), nameof(Pickup.Start))]
    public class PickupStartPatch
    {
        public static void Postfix(Pickup __instance)
        {
            //__instance.Rb.useGravity = false;
            float p1 = UnityEngine.Random.Range(-0.5f, 0.5f);
            float p2 = UnityEngine.Random.Range(-0.5f, 0.5f);
            float p3 = UnityEngine.Random.Range(-0.5f, 0.5f);
            Vector3 v3 = new Vector3(p1, p2, p3);
            float rng = UnityEngine.Random.Range(0.1f, 0.5f);
            __instance.Rb.AddExplosionForce(rng, __instance.Rb.position + v3, 10f);
        }
    }*/
}
