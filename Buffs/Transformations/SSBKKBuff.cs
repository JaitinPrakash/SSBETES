using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;
using DBZGoatLib.Model;
using DBZGoatLib.Handlers;
using DBZGoatLib;
using SSBETES.Assets;
using SSBETES.Changes;
using Terraria.DataStructures;

namespace SSBETES.Buffs.Transformations
{
    internal class SSBKKBuff : Transformation
    {
        public override AuraData AuraData() => new AuraData("SSBETES/Assets/Aura/SSBKKAura", 8, BlendState.AlphaBlend, new Color(255, 255, 255)); //7, 166, 182

        public override bool CanTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<BUPPlayer>();

            return (player.GetModPlayer<GPlayer>().Trait != "Legendary" && player.GetModPlayer<GPlayer>().Trait != "Ancient" && player.GetModPlayer<GPlayer>().Trait != "GOD" && player.GetModPlayer<GPlayer>().Trait != "Primal" && player.GetModPlayer<GPlayer>().Trait != "Majin") && !player.HasBuff<SSBKKBuff>() && modPlayer.SSBKKAchieved;
        }

        public override string FormName() => "Super Saiyan Blue Kaio-Ken x10";

        public override string HairTexturePath() => "SSBETES/Assets/Hair/SSBKKHair";

        public override Gradient KiBarGradient() => new Gradient(new Color(51, 155, 155)).AddStop(1f, new Color(229, 34, 59));

        public override void OnTransform(Player player)
        {
            player.GetModPlayer<BUPPlayer>().FormCD = true;
            player.GetModPlayer<BUPPlayer>().SSBKKActive = true;
            player.GetModPlayer<BUPPlayer>().isTransformationAnimationPlaying = true;
        }

        public override void PostTransform(Player player)
        {
            player.GetModPlayer<BUPPlayer>().SSBKKActive = false;
        }

        public override bool SaiyanSparks() => false;

        public override SoundData SoundData() => new SoundData("DBZMODPORT/Sounds/KaioAuraAscend", "DBZMODPORT/Sounds/SSJR", 260);

        public override bool Stackable() => false;

        public override Color TextColor() => Color.Turquoise;

        public override void SetStaticDefaults()
        {
            kiDrainRate = 4.7f;
            kiDrainRateWithMastery = 2.6f;
            attackDrainMulti = 1.7f;
            
            if (ConfigServer.Instance.SSJTweaks)
            {
                // Extreme Scaling: SSJB x KK x10 = 100 x 1.75 = 175x; defense FROZEN at SSJB's 4200
                baseDefenceBonus = 4200;
                damageMulti = 175.0f;
                speedMulti = 1.35f;
            }
            else
            {
                baseDefenceBonus = 44;
                damageMulti = 5.35f;
                speedMulti = 5.45f;
            }
            base.SetStaticDefaults();
            //base.Description.SetDefault("\nConsumes health in exchange for strength.");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SSBKKEffect>().SSBKKBuff = true;
            player.GetModPlayer<BUPPlayer>().FormCD = false;
            player.lifeRegen -= 34;
            base.Update(player, ref buffIndex);
            if (player.GetModPlayer<GPlayer>().GetMastery(ModContent.BuffType<SSBKKBuff>()) <= 0.5f)
            {
                player.GetModPlayer<GPlayer>().MasteryMultiplier = 1.2f;
            }
            if (player.GetModPlayer<GPlayer>().GetMastery(ModContent.BuffType<SSBKKBuff>()) < 1f && !player.GetModPlayer<BUPPlayer>().KaioLock)
            {
                player.GetModPlayer<BUPPlayer>().KaioLock = true;
            }
            /*
            if (player.GetModPlayer<GPlayer>().GetMastery(ModContent.BuffType<SSBKKBuff>()) <= 0f)
            {
                Main.NewText("Now you are able to combine your strongest form with old techique!", Color.Aquamarine);
            }
            */
            Lighting.AddLight(player.Center, 1.5f, 0.5f, 0.5f);
            base.SetStaticDefaults();
        }
        public override void ModifyBuffText(ref string buffName, ref string tip, ref int raree)
        {
            tip = ((Transformation)this).BuildTooltip() + $"\nConsumes health in exchange for strength.";
        }
    }
    public class SSBKKEffect : ModPlayer
    {
        public bool SSBKKBuff;

        public override void ResetEffects()
        {
            SSBKKBuff = false;
        }

        public override void PostUpdateMiscEffects()
        {
            if (!SSBKKBuff)
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
            if (SSBKKBuff)
            {
                g *= 0.85f;
                b *= 0.85f;
            }
        }
    }
}
