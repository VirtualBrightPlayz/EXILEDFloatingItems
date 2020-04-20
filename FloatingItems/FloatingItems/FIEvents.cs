using EXILED;
using EXILED.Extensions;
using System;

namespace FloatingItems
{
    public class FIEvents
    {
        internal void ItemDropped(ItemDroppedEvent ev)
        {
            if (FloatingItems.allowFloatDrops.Count == 0)
            {
                PickupUpdatePatch.canBeFloating.Add(ev.Item);
            }
            else if (FloatingItems.allowFloatDrops.Contains(ev.Player.GetUserId()))
            {
                PickupUpdatePatch.canBeFloating.Add(ev.Item);
            }
        }

        internal void RoundStart()
        {
            PickupUpdatePatch.canBeFloating.Clear();
            PickupUpdatePatch.haveBeenMoved.Clear();
        }
    }
}