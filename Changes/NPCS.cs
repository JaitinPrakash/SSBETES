using System;
using Terraria.ID;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Terraria;
using DBZMODPORT;
using SSBETES.Items;
using SSBETES.Assets;
using DBZMODPORT.Items.Materials;

namespace SSBETES.Changes
{
    internal class NPCS : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.EyeofCthulhu)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.TestItems.EvilSaiyanItem>(), 1, 0, 1));
            }
            if (npc.type == NPCID.Plantera)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Armour.Armor.CrimsonMask>(), 1, 0, 1));
            }
            if (npc.type == NPCID.LunarTowerSolar || npc.type == NPCID.LunarTowerNebula || npc.type == NPCID.LunarTowerStardust || npc.type == NPCID.LunarTowerVortex)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.TestItems.SSJ3FPUnlockItem>(), 3, 0, 1));
            }
        }
        public override void ModifyShop(NPCShop shop)
        {
            Condition rand3 = new Condition("Random.NextBool3", () => Main.rand.NextBool(3));
            int npcType = shop.NpcType;
            if (npcType <= 54)
            {
                if (npcType == 17)
                {
                    //shop.Add<Items.Upgrades.EScroll>(Condition.DownedMoonLord);
                    //shop.Add<Items.Upgrades.KScroll>(Condition.DownedMoonLord);
                    shop.Add<Items.Upgrades.ExchangeScroll>(Condition.DownedMoonLord);
                    return;
                }
            }
        }
    }
}
