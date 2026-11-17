using Microsoft.Xna.Framework;
using Terraria;
using DBZGoatLib;
using DBZGoatLib.Handlers;
using Terraria.ModLoader;
using Terraria.ID;
using SSBETES.Assets;
using Terraria.Audio;
using Terraria.Graphics.Shaders;

namespace SSBETES.Items.TestItems
{
    internal class PARitem : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("particle test");
            // Tooltip.SetDefault("Unleashes new power");
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
            Item.rare = ItemRarityID.Orange;
        }

        public override bool? UseItem(Player player)
        {
            var BUPPlayer = player.GetModPlayer<BUPPlayer>();
            return true;
        }

        public override void OnConsumeItem(Player player)
        {
            var BUPPlayer = player.GetModPlayer<BUPPlayer>();
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDustPerfect(player.Center + new Vector2(Utils.NextFloat(Main.rand, (float)(-(float)player.width - 10), (float)(player.width + 10)), Utils.NextFloat(Main.rand, (float)(-(float)player.height - 10), (float)(player.height + 10))), 137,
                new Vector2?(Utils.RotatedByRandom(new Vector2(0f, -1.5f), (double)MathHelper.ToRadians(100f))), 0, default(Color), 1f).noGravity = true;

                Dust.NewDustPerfect(player.Center + new Vector2(Utils.NextFloat(Main.rand, (float)(-(float)player.width - 10), (float)(player.width + 10)), Utils.NextFloat(Main.rand, (float)(-(float)player.height - 10), (float)(player.height + 10))), 142,
                new Vector2?(Utils.RotatedByRandom(new Vector2(0f, -1.5f), (double)MathHelper.ToRadians(100f))), 0, default(Color), 1f).noGravity = true;
            }
            SoundEngine.PlaySound(new SoundStyle("DBZMODPORT/Sounds/Awakening").WithVolumeScale(2f).WithPitchOffset(0f), player.position);


            for (int i = 0; i < 250; i++)
            {
                Vector2 position = Main.LocalPlayer.TopLeft;
                Dust dust;
                dust = Terraria.Dust.NewDustDirect(position, 1200, 269, 27, 0f, -10f, 192, new Color(255, 244, 0), 1.6860466f);
                dust.noGravity = true;
                dust.shader = GameShaders.Armor.GetSecondaryShader(66, Main.LocalPlayer);
                dust.fadeIn = 0.83720934f;
                
                Dust dust2;
                dust2 = Terraria.Dust.NewDustDirect(position, 1200, 269, 27, 0f, -10f, 213, new Color(0, 111, 255), 1.6860466f);
                dust2.noGravity = true;
                dust2.shader = GameShaders.Armor.GetSecondaryShader(25, Main.LocalPlayer);
                dust2.fadeIn = 1.7790697f;
            }
        }
    }
}
