using Microsoft.Xna.Framework;
using Terraria;
using DBZGoatLib;
using DBZGoatLib.Handlers;
using Terraria.ModLoader;
using Terraria.ID;
using SSBETES.Assets;
//using TraitsPlus;

namespace SSBETES.Items.TestItems
{
    internal class EvilSaiyanItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fruit of Might");
            // Tooltip.SetDefault("Increases max hp by 50\nIncreases max ki by 500\nUnlocks Evil Form if you have Ancient Trait");
        }

        public override void SetDefaults()
        {
            Item.consumable = true;
            Item.potion = false;
            Item.width = 28;
            Item.height = 28;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.DrinkOld;
            Item.holdStyle = ItemHoldStyleID.HoldFront;
            Item.UseSound = SoundID.Item2;
            Item.autoReuse = false;
            Item.noMelee = true;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Red;
        }
    public override bool? UseItem(Player player)
        {
            return true;
        }

        public override void OnConsumeItem(Player player)
        {
            var BUPPlayer = player.GetModPlayer<BUPPlayer>();
                BUPPlayer.EvilSaiyanAchieved = true;
        }
    }
}
