using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;
using DBZGoatLib.Model;
using DBZGoatLib.Handlers;
using DBZGoatLib;
using SSBETES.Assets;
using SSBETES.Changes;
using Terraria.Graphics.Shaders;
using Terraria.DataStructures;

namespace SSBETES.Buffs.Transformations
{
    internal class SSBKKx20Buff : Transformation
    {
        public override AuraData AuraData() => new AuraData("SSBETES/Assets/Aura/SSBKK20Aura", 8, BlendState.AlphaBlend, new Color(255, 255, 255)); //7, 166, 182

        public override bool CanTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<BUPPlayer>();

            return (player.GetModPlayer<GPlayer>().Trait != "Legendary" && player.GetModPlayer<GPlayer>().Trait != "Ancient" && player.GetModPlayer<GPlayer>().Trait != "GOD" && player.GetModPlayer<GPlayer>().Trait != "Primal" && player.GetModPlayer<GPlayer>().Trait != "Majin") && !player.HasBuff<SSBKKx20Buff>() && modPlayer.SSBKKx20Achieved && !modPlayer.FormCD;
        }

        public override string FormName() => "Super Saiyan Blue Kaio-Ken x20";

        public override string HairTexturePath() => "SSBETES/Assets/Hair/SSBKKHair";

        public override Gradient KiBarGradient() => new Gradient(new Color(51, 155, 155)).AddStop(1f, new Color(229, 34, 59));

        public override void OnTransform(Player player)
        {
            player.GetModPlayer<BUPPlayer>().SSBKKx20Active = true;
            player.GetModPlayer<BUPPlayer>().isTransformationAnimationPlaying = true;
        }

        public override void PostTransform(Player player)
        {
            player.GetModPlayer<BUPPlayer>().SSBKKx20Active = false;
        }

        public override bool SaiyanSparks() => false;

        public override SoundData SoundData() => new SoundData("DBZMODPORT/Sounds/KaioAuraAscend", "DBZMODPORT/Sounds/SSJR", 260);

        public override bool Stackable() => false;

        public override Color TextColor() => Color.Turquoise;

        public override void SetStaticDefaults()
        {
            kiDrainRate = 5f;
            kiDrainRateWithMastery = 2.2f;
            attackDrainMulti = 1.9f;
            
            if (ConfigServer.Instance.SSJTweaks)
            {
                // Extreme Scaling: SSJB x KK x20 = 100 x 2.0 = 200x; defense FROZEN at SSJB's 4200
                baseDefenceBonus = 4200;
                damageMulti = 200.0f;
                speedMulti = 1.35f;
            }
            else
            {
                baseDefenceBonus = 39;
                damageMulti = 5.55f;
                speedMulti = 5.45f;
            }
            base.SetStaticDefaults();
            //base.Description.SetDefault("\nConsumes health in exchange for strength.");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SSBKKx20Effect>().SSBKKx20Buff = true;
            player.lifeRegen -= 42;
            base.Update(player, ref buffIndex);
            /*
            Dust dust;
            Vector2 position = Main.LocalPlayer.Left;
            dust = Main.dust[Terraria.Dust.NewDust(position, 45, 37, 10, 0f, -4.1860466f, 0, new Color(240, 28, 28), 0.69767445f)];
            dust.noLight = true;
            dust.fadeIn = 0.24418604f;

            Dust dust2;
            dust2 = Main.dust[Terraria.Dust.NewDust(position, 45, 37, 15, 0f, -2.790698f, 0, new Color(0, 155, 255), 0.87209296f)];
            dust2.shader = GameShaders.Armor.GetSecondaryShader(15, Main.LocalPlayer);
            */
            if (player.GetModPlayer<GPlayer>().GetMastery(ModContent.BuffType<Buffs.Transformations.SSBKKx20Buff>()) <= 1f)
            {
                player.GetModPlayer<GPlayer>().MasteryMultiplier = 5f;
            }
            Lighting.AddLight(player.Center, 2f, 0.5f, 0.5f);
            base.SetStaticDefaults();
        }

        public override void ModifyBuffText(ref string buffName, ref string tip, ref int raree)
        {
            tip = ((Transformation)this).BuildTooltip() + $"\nConsumes even more health.";
        }
    }
    public class SSBKKx20Effect : ModPlayer
    {
        public bool SSBKKx20Buff;

        public override void ResetEffects()
        {
            SSBKKx20Buff = false;
        }

        public override void PostUpdateMiscEffects()
        {
            if (!SSBKKx20Buff)
                return;

            Player.pulley = false;

            if (Player.mount.Active)
            {
                Player.mount.Dismount(Player);
            }
        }

        // DrawEffects is used to provide a simple visual effect, tinting the player blue (by darkening green and red).
        // A more complete implementation would likely use a custom PlayerDrawLayer to draw over the player, like how Frozen draws Ice over the player.
        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (SSBKKx20Buff)
            {
                g *= 0.6f;
                b *= 0.6f;
            }
        }
    }
}
