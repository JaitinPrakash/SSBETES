using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using SSBETES.Assets;
using SSBETES.Buffs.Transformations;
using DBZGoatLib.Handlers;
using DBZMODPORT;
using SSBETES.Changes;

namespace SSBETES.Items.Accessories
{
    public class EnchancedDB : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Enchanted Dragon Ball");
            // Tooltip.SetDefault("Powers you up\nIncreasing ki damage\nIncreasing maximum ki by 250\nIncreasing maximum hp by 20" );
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 38;
            Item.value = 240000;
            Item.rare = ItemRarityID.Red;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<BUPPlayer>().DragonBallEquipped = true;
            if (!Config.Instance.DBAura)
            {
                if (!hideVisual && TransformationHandler.TransformKey.JustPressed)
                {
                    TransformationHandler.Transform(player, TransformationHandler.GetTransformation("DBEnchancedBuff").Value);
                }
                player.statLifeMax2 += 20;
                MyPlayer.ModPlayer(player).kiDamage += 0.12f;
                MyPlayer.ModPlayer(player).kiKbAddition += 0.14f;
                MyPlayer.ModPlayer(player).kiMax2 += 250;
            }
            else
            {
                player.statLifeMax2 += 20;
                MyPlayer.ModPlayer(player).kiDamage += 0.12f;
                MyPlayer.ModPlayer(player).kiKbAddition += 0.14f;
                MyPlayer.ModPlayer(player).kiMax2 += 250;
                player.moveSpeed *= 1.10f;
                if (ConfigServer.Instance.SSJTweaks)
                {
                    player.statDefense += 5;
                    player.GetDamage(DamageClass.Generic) *= 1.20f;
                }
                else
                {
                    player.statDefense += 7;
                    player.GetDamage(DamageClass.Generic) *= 1.30f;
                }
            }

        }

        public override void AddRecipes()
        {
            Mod DBT = ModLoader.GetMod("DBZMODPORT");
            Mod SB = ModLoader.GetMod("SSBETES");
            Recipe recipeEDB1 = CreateRecipe(1);
            recipeEDB1.AddIngredient(DBT, "FourStarDB", 1);
            recipeEDB1.AddIngredient(SB, "EvilSaiyanItem", 1);
            recipeEDB1.AddIngredient(ItemID.VilePowder, 5);
            recipeEDB1.Register();

            Recipe recipeEDB2 = CreateRecipe(1);
            recipeEDB2.AddIngredient(DBT, "FourStarDB", 1);
            recipeEDB2.AddIngredient(SB, "EvilSaiyanItem", 1);
            recipeEDB2.AddIngredient(ItemID.ViciousPowder, 5);
            recipeEDB2.Register();
        }
    }
}
