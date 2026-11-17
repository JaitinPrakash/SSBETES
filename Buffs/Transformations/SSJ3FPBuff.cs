using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;
using DBZGoatLib.Model;
using DBZGoatLib.Handlers;
using DBZGoatLib;
using SSBETES.Assets;
using SSBETES.Changes;
using DBZMODPORT.Buffs.SSJBuffs;
using DBZMODPORT;

namespace SSBETES.Buffs.Transformations
{
    internal class SSJ3FPBuff : Transformation
    {
        public override AuraData AuraData() => new AuraData("DBZMODPORT/Effects/Animations/Aura/SSJ3Aura", 4, BlendState.AlphaBlend, new Color(255, 255, 255)); //255. 248. 0

        public override bool CanTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<BUPPlayer>();

            return !player.HasBuff<SSJ3FPBuff>() && modPlayer.SSJ3FPAchieved;
        }

        public override string FormName() => "Super Saiyan 3 Full Power";

        public override string HairTexturePath() => "SSBETES/Assets/Hair/SSJ3FPHair";

        public override Gradient KiBarGradient() => new Gradient(Color.Gold).AddStop(1f, new Color(110, 0, 0));

        public override void OnTransform(Player player)
        {
            player.GetModPlayer<BUPPlayer>().SSJ3FPActive = true;
            player.GetModPlayer<BUPPlayer>().isTransformationAnimationPlaying = true;
        }

        public override void PostTransform(Player player)
        {
            player.GetModPlayer<BUPPlayer>().SSJ3FPActive = false;
        }

        public override bool SaiyanSparks() => true;

        public override SoundData SoundData() => new SoundData("DBZMODPORT/Sounds/SSJAscension", "DBZMODPORT/Sounds/SSJAura", 260);

        public override bool Stackable() => false;

        public override Color TextColor() => Color.Gold;

        public override void SetStaticDefaults()
        {
            kiDrainRate = 3.15f;
            kiDrainRateWithMastery = 1.7f;
            attackDrainMulti = 2.1f;

            base.SetStaticDefaults(); 
        }

        public override void Update(Player player, ref int buffIndex)
        {
            BUPPlayer modPlayer = Main.LocalPlayer.GetModPlayer<BUPPlayer>();
            MyPlayer DBTPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();

            float kiPercent = DBTPlayer.GetKi() / (float)DBTPlayer.OverallKiMax();

            if (kiPercent <= 0.35f)
            {
                if (player.GetModPlayer<GPlayer>().GetMastery(ModContent.BuffType<SSJ3FPBuff>()) < 1f)
                {
                    player.lifeRegen -= 25;
                }
                else
                {
                    player.lifeRegen -= 12;
                }
            }

            if (ConfigServer.Instance.SSJTweaks)
            {
                // Extreme Scaling: base 45x -> 60x at full EvilPower (EvilPower caps at 1.0);
                // defense 15 x damage: 675 -> 900
                baseDefenceBonus = (int)(675f + 225f * modPlayer.GetEvilPower());
                damageMulti = 45.0f + 15.0f * modPlayer.GetEvilPower();
                speedMulti = 1.10f;
            }
            else
            {
                baseDefenceBonus = 15;
                damageMulti = 3.3f + (0.5f * modPlayer.GetEvilPower());
                speedMulti = 3.3f;
            }

            if (player.HasBuff<SSJ3Buff>())
            {
                player.ClearBuff(ModContent.BuffType<SSJ3Buff>());
            }
            base.Update(player, ref buffIndex);
            base.SetStaticDefaults();
        }

        public override void ModifyBuffText(ref string buffName, ref string tip, ref int raree)
        {
            tip = ((Transformation)this).BuildTooltip() + $"\nIncreases power and restores Ki when landing Ki attacks\n(Life drains when below 35% Max Ki)";
        }
    }
}
