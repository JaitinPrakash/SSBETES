using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using SSBETES.Assets;
using DBZGoatLib;
using DBZGoatLib.Handlers;
using DBZGoatLib.Model;
using DBZMODPORT;
using System;

namespace SSBETES.Items.TestItems
{
    internal class SSJ3FPUnlockItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 3, false));
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 20;
        }
        public override bool CanPickup(Player player)
        {
            if (player.GetModPlayer<GPlayer>().Trait == "Ancient" && player.GetModPlayer<MyPlayer>().masteryLevel3 >= 1f && !player.GetModPlayer<BUPPlayer>().SSJ3FPAchieved)
            {
                return true;
            }
            else { return false;}
            
        }
        public override bool OnPickup(Player player)
        {
            var BUPPlayer = player.GetModPlayer<BUPPlayer>();
            BUPPlayer.SSJ3FPAchieved = true;
            BUPPlayer.SSJ3FPAnimation();
            BUPPlayer.TransformationProcess = true;
            return false;
        }
        /*
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color?(Color.Crimson);
        }
        */
    }
}
