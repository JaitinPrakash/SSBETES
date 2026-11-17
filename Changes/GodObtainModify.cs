using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using DBZMODPORT;
using DBZMODPORT.Items.Misc;
using DBZGoatLib;

namespace SSBETES.Changes
{
    internal class GodObtainModify : GlobalItem
    {
        public override bool CanPickup(Item item, Player player)
        {
            if (item.type == ModContent.ItemType<SSJGUnlockItem>())
            {
                if (!player.GetModPlayer<MyPlayer>().ssjgAchieved)
                {
                    if (player.GetModPlayer<GPlayer>().Trait == "Legendary" || player.GetModPlayer<GPlayer>().Trait == "Ancient" || player.GetModPlayer<GPlayer>().Trait == "Primal")
                    {
                        return false;
                    }
                    else if (player.GetModPlayer<MyPlayer>().masteryLevel3 >= 1 || player.GetModPlayer<GPlayer>().Trait == "GOD")
                    {
                        return true;
                    }                    
                    else { return false; }
                }
                else { return false; }
            }
            else { return true; }
        }
    }
}