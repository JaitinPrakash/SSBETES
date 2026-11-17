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

namespace SSBETES.Buffs.Transformations
{
    internal class SSJRGBuff : Transformation
    {
        public override AuraData AuraData() => new AuraData("SSBETES/Assets/Aura/SSJRGAura", 4, BlendState.AlphaBlend, new Color(255, 255, 255)); //255. 248. 0

        public override bool CanTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<BUPPlayer>();

            return !player.HasBuff<SSJRGBuff>() && modPlayer.SSJRGAchieved;
        }

        public override string FormName() => "Super Saiyan Rage";

        public override string HairTexturePath() => "DBZMODPORT/Hairs/SSJ/SSJ2Hair";

        public override Gradient KiBarGradient() => new Gradient(new Color(255, 248, 0)).AddStop(1f, new Color(71, 164, 219));

        public override void OnTransform(Player player)
        {
            player.GetModPlayer<BUPPlayer>().SSJRGActive = true;
        }

        public override void PostTransform(Player player)
        {
            player.GetModPlayer<BUPPlayer>().SSJRGActive = false;
        }

        public override bool SaiyanSparks() => true;

        public override SoundData SoundData() => new SoundData("DBZMODPORT/Sounds/SSJAscension", "DBZMODPORT/Sounds/SSB", 260);

        public override bool Stackable() => false;

        public override Color TextColor() => Color.Yellow;

        public override void SetStaticDefaults()
        {
            kiDrainRate = 2.5f;
            kiDrainRateWithMastery = 0.7f;
            attackDrainMulti = 1.65f;

            if (ConfigServer.Instance.SSJTweaks)
            {
                baseDefenceBonus = 19;
                damageMulti = 1.50f;
                speedMulti = 1.10f;
            }
            else
            {
                baseDefenceBonus = 14;
                damageMulti = 3.3f;
                speedMulti = 3.3f;
            }

            base.SetStaticDefaults(); 
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.HasBuff<SSJ2Buff>())
            {
                player.ClearBuff(ModContent.BuffType<SSJ2Buff>());
            }
            base.Update(player, ref buffIndex);
            base.SetStaticDefaults();
        }
    }
}
