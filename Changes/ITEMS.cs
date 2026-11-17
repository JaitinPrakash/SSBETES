using System;
using System.Linq;
using System.Reflection;
using Terraria.ID;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Terraria;
using DBZMODPORT;
using SSBETES.Items.Upgrades;
using SSBETES.Assets;
using SSBETES.Changes;

namespace SSBETES.Changes
{
    internal class ITEMS : GlobalItem
    {
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            switch (item.type)
            {
                case Terraria.ID.ItemID.GolemBossBag:
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<KiFragmentS>(), 4, 1, 1));
                    break;
                default:
                    return;
            }
        }

    }
}
