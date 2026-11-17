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
    internal class KiEssenceS : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Skilled Ki Scroll");
            // Tooltip.SetDefault("Increases your ki charge rate.");
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
            return BUPPlayer.SuperScroll == false;
        }

        public override bool? UseItem(Player player)
        {
            var BUPPlayer = player.GetModPlayer<BUPPlayer>();
            BUPPlayer.SuperScroll = true;
            MyPlayer.ModPlayer(player).kiChargeRate += 3;
            return new bool?(true);
        }

        public override void OnConsumeItem(Player player)
        {
            var BUPPlayer = player.GetModPlayer<BUPPlayer>();
                BUPPlayer.SuperScroll = true;
        }
        
        public override void AddRecipes()
        {
            Mod DBT = ModLoader.GetMod("DBZMODPORT");
            Recipe recipeSCR = CreateRecipe(1);
            recipeSCR.AddIngredient(DBT, "AngerKiCrystal", 30);
            recipeSCR.AddIngredient(ItemID.SoulofSight, 10);
            recipeSCR.AddIngredient(ItemID.AntlionMandible, 3);
            recipeSCR.Register();
        }
        
    }
}
