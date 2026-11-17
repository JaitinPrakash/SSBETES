using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;
using DBZGoatLib.Model;
using DBZGoatLib.Handlers;
using DBZGoatLib;
using SSBETES.Buffs.Debuffs;
using SSBETES.Assets;
using SSBETES.Changes;
using DBZMODPORT;

namespace SSBETES.Buffs.Transformations
{
    internal class FPSSJBuff : Transformation
    {
        public int timer;
        public bool timerDone;

        public override AuraData AuraData() => new AuraData("DBZMODPORT/Effects/Animations/Aura/LSSJAura", 4, BlendState.Additive, new Color(66, 225, 58));

        public override bool CanTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<BUPPlayer>();

            return !player.HasBuff<FPSSJBuff>();
        }

        public override string FormName() => "Full Power Super Saiyan";

        public override string HairTexturePath() => "DBZMODPORT/Hairs/LSSJ/LSSJHair";

        public override Gradient KiBarGradient() => new Gradient(new Color(251, 255, 48)).AddStop(1f, new Color(66, 225, 58));

        public override void OnTransform(Player player)
        {
            player.GetModPlayer<BUPPlayer>().FPSSJActive = true;
        }

        public override void PostTransform(Player player)
        {
            player.AddBuff(ModContent.BuffType<FPCooldown>(), 10800, true, false);
            player.GetModPlayer<BUPPlayer>().FPSSJActive = false;
            timer = 0;
        }

        public override bool SaiyanSparks() => false;

        public override SoundData SoundData() => new SoundData("DBZMODPORT/Sounds/SSJAscension", "DBZMODPORT/Sounds/SSJAura", 260);

        public override bool Stackable() => false;

        public override Color TextColor() => Color.Lime;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults(); 
        }

        public override void Update(Player player, ref int buffIndex)
        {
            BUPPlayer modPlayer = Main.LocalPlayer.GetModPlayer<BUPPlayer>();

            kiDrainRate = 1.5f * modPlayer.RandomFormDrain();
            kiDrainRateWithMastery = 1f * modPlayer.RandomMasteredFormDrain();
            attackDrainMulti = 2f * modPlayer.RandomAttackDrain();
            player.GetModPlayer<MyPlayer>().kiMax2 += 3000;
            player.statLifeMax2 += 100;

            if (ConfigServer.Instance.SSJTweaks)
            {
                baseDefenceBonus = 13;
                damageMulti = 1.6f + modPlayer.RandomAttackBonus();
                speedMulti = 1.35f;
            }
            else
            {
                baseDefenceBonus = 9;
                damageMulti = 2.15f + modPlayer.RandomAttackBonus();
                speedMulti = 2.10f;
            }

            if (!timerDone)
            {
                timer++;
            }
            if (timer == 2400)
            {
                TransformationHandler.ClearTransformations(player);
            }

            base.Update(player, ref buffIndex);
        }

        public override void ModifyBuffText(ref string buffName, ref string tip, ref int raree)
        {
            tip = ((Transformation)this).BuildTooltip() + $"\nYour ki is unstable.";
        }
    }
}
