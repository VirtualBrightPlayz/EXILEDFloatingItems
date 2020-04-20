using EXILED;
using EXILED.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FloatingItems
{
    public class FIEvents
    {
        public static List<string> activeUsersFitems = new List<string>();
        public static List<string> activeUsersSitems = new List<string>();

        internal void ItemDropped(ItemDroppedEvent ev)
        {
            if (ev.Player.CheckPermission("fitem.dropitem") && activeUsersFitems.Contains(ev.Player.GetUserId()))
            {
                PickupUpdatePatch.canBeFloating.Add(ev.Item);
            }
        }

        internal void RoundStart()
        {
            PickupUpdatePatch.canBeFloating.Clear();
            PickupUpdatePatch.haveBeenMoved.Clear();
        }

        internal void PlayerCmd(ConsoleCommandEvent ev)
        {
            if (ev.Player.CheckPermission("fitem.dropitem") && ev.Command.ToLower().StartsWith("fitems"))
            {
                if (activeUsersFitems.Contains(ev.Player.GetUserId()))
                {
                    activeUsersFitems.Remove(ev.Player.GetUserId());
                    ev.ReturnMessage = "Floating items disabled.";
                    return;
                }
                else
                {
                    activeUsersFitems.Add(ev.Player.GetUserId());
                    ev.ReturnMessage = "Floating items enabled.";
                    return;
                }
            }
            if (ev.Player.CheckPermission("fitem.shootitem") && ev.Command.ToLower().StartsWith("sitems"))
            {
                if (activeUsersSitems.Contains(ev.Player.GetUserId()))
                {
                    activeUsersSitems.Remove(ev.Player.GetUserId());
                    ev.ReturnMessage = "Shootable items disabled.";
                    return;
                }
                else
                {
                    activeUsersSitems.Add(ev.Player.GetUserId());
                    ev.ReturnMessage = "Shootable items enabled.";
                    return;
                }
            }
        }

        internal void PlayerShoot(ref ShootEvent ev)
        {
            if (ev.Shooter.CheckPermission("fitem.shootitem") && activeUsersSitems.Contains(ev.Shooter.GetUserId()))
            {
                RaycastHit info;
                if (Physics.Linecast(ev.Shooter.GetComponent<Scp049PlayerScript>().plyCam.transform.position, ev.TargetPos, out info))
                {
                    Collider[] arr = Physics.OverlapSphere(info.point, FloatingItems.rangeShoot);
                    foreach (Collider collider in arr)
                    {
                        if (collider.GetComponent<Pickup>() != null)
                        {
                            collider.GetComponent<Pickup>().Rb.AddExplosionForce(FloatingItems.forceShoot, info.point, FloatingItems.rangeShoot);
                        }
                    }
                }
                else
                {
                    Collider[] arr = Physics.OverlapSphere(ev.TargetPos, FloatingItems.rangeShoot);
                    foreach (Collider collider in arr)
                    {
                        if (collider.GetComponent<Pickup>() != null)
                        {
                            collider.GetComponent<Pickup>().Rb.AddExplosionForce(FloatingItems.forceShoot, ev.TargetPos, FloatingItems.rangeShoot);
                        }
                    }
                }
            }
        }
    }
}