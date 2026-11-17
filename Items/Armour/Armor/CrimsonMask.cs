using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using DBZMODPORT;
using SSBETES.Assets;

namespace SSBETES.Items.Armour.Armor
{
    [AutoloadEquip(EquipType.Head)]
    internal class CrimsonMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            //Item.CloneDefaults(ItemID.MimeMask);
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(gold: 35);
            Item.rare = ItemRarityID.Red;
            Item.defense = 10;
        }

        public override void UpdateEquip(Player player)
        {
            var BUPPlayer = player.GetModPlayer<BUPPlayer>();
            BUPPlayer.CrimsonMaskEqpd = true;
            player.statLifeMax2 += 100;
            player.GetModPlayer<MyPlayer>().kiMax2 -= 2500;
            player.GetModPlayer<MyPlayer>().kiChargeRate += 3;
        }
    }
}