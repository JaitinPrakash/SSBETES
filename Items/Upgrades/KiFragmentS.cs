using Microsoft.Xna.Framework;
using Terraria;
using DBZGoatLib;
using DBZGoatLib.Handlers;
using Terraria.ModLoader;
using Terraria.ID;
using SSBETES.Assets;
using DBZMODPORT;
//using TraitsPlus;

namespace SSBETES.Items.Upgrades
{
    internal class KiFragmentS : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Skilled Ki Fragment");
            // Tooltip.SetDefault("Increases your max ki by 1000.");
        }

        public override void SetDefaults()
        {
            Item.consumable = true;
            Item.potion = false;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.noMelee = true;
            Item.scale = 1f;
            Item.rare = ItemRarityID.LightPurple;
        }
        public override bool CanUseItem(Player player)
        {
            var BUPPlayer = player.GetModPlayer<BUPPlayer>();
            return BUPPlayer.SuperFragment == false;
        }

        public override bool? UseItem(Player player)
        {
            var BUPPlayer = player.GetModPlayer<BUPPlayer>();
            BUPPlayer.SuperFragment = true;
            return new bool?(true);
        }

        public override void OnConsumeItem(Player player)
        {
            var BUPPlayer = player.GetModPlayer<BUPPlayer>();
            BUPPlayer.SuperFragment = true;
        }
    }
}
