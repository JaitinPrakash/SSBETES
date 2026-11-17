using Microsoft.Xna.Framework;
using Terraria;
using DBZGoatLib.Model;
using SSBETES.Assets;
using SSBETES.Changes;
using DBZGoatLib;
using DBZMODPORT.Buffs.SSJBuffs;
using Terraria.ModLoader;

namespace SSBETES.Buffs.Transformations
{
    internal class SSJFPBuff : Transformation
    {
        public override AuraData AuraData()
        {
            return default(AuraData);
        }

        public override bool CanTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<BUPPlayer>();

            return !player.HasBuff<SSJFPBuff>() && modPlayer.SSJFPAchieved;
        }

        public override string FormName() => "Super Saiyan Full Power";

        public override string HairTexturePath() => "SSBETES/Assets/Hair/SSJFPHair";

        public override Gradient KiBarGradient() => new Gradient(Color.Gold).AddStop(1f, Color.Gold);

        public override void OnTransform(Player player)
        {
            player.GetModPlayer<BUPPlayer>().FormCD = true;
            player.GetModPlayer<BUPPlayer>().SSJFPActive = true;
        }

        public override void PostTransform(Player player)
        {
            player.GetModPlayer<BUPPlayer>().SSJFPActive = false;
        }

        public override bool SaiyanSparks() => false;

        public override SoundData SoundData() => new SoundData("DBZMODPORT/Sounds/PowerDown", "SSBETES/Sounds/BLANK", 260);

        public override bool Stackable() => false;

        public override Color TextColor() => Color.BlanchedAlmond;

        public override void SetStaticDefaults()
        {
            kiDrainRate = 0f;
            kiDrainRateWithMastery = 0f;
            attackDrainMulti = 1.15f;

            if (!ConfigServer.Instance.SSJTweaks)
            {
                baseDefenceBonus = 5;
                damageMulti = 1.60f;
                speedMulti = 1.60f;
            }
            else
            {
                // Extreme Scaling: 8x; defense 5 x 8 = 40
                baseDefenceBonus = 40;
                damageMulti = 8.0f;
                speedMulti = 1.15f;
            }

            base.SetStaticDefaults(); 
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<BUPPlayer>().FormCD = false;
            if (player.HasBuff<SSJ2Buff>())
            {
                player.ClearBuff(ModContent.BuffType<SSJ2Buff>());
            }
            Lighting.AddLight(player.Center, 1.5f, 1.5f, 0f);
            base.Update(player, ref buffIndex);
            base.SetStaticDefaults();
        }
    }
}
