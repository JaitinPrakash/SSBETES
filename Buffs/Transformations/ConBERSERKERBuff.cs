using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;
using DBZGoatLib.Model;
using DBZGoatLib.Handlers;
using DBZGoatLib;
using SSBETES.Assets;
using SSBETES.Changes;
//using DBTBalance;
//using DBTBalance.Buffs;
//using DBTBalance.Helpers;
//using DBTBalance.Model;

namespace SSBETES.Buffs.Transformations
{
    internal class ConBERSERKERBuff : Transformation
    {
        public override AuraData AuraData() => new AuraData("SSBETES/Assets/Aura/ControlledEvilAura", 4, BlendState.AlphaBlend, new Color(255, 255, 255));

        public override bool CanTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<BUPPlayer>();

            return !player.HasBuff<ConBERSERKERBuff>() && modPlayer.ConBERSERKERAchieved && player.GetModPlayer<GPlayer>().Trait != "Ancient";
        }

        public override string FormName() => "Controlled Berserk";

        public override string HairTexturePath()
        {
            return string.Empty;
        }

        public override Gradient KiBarGradient() => new Gradient(new Color(13, 18, 71)).AddStop(1f, new Color(10, 24, 131));

        public override void OnTransform(Player player)
        {
            player.GetModPlayer<BUPPlayer>().ConBERSERKERActive = true;
        }

        public override void PostTransform(Player player)
        {
            player.GetModPlayer<BUPPlayer>().ConBERSERKERActive = false;
        }

        public override bool SaiyanSparks() => false;

        public override SoundData SoundData() => new SoundData("DBZMODPORT/Sounds/SSJAscension", "DBZMODPORT/Sounds/SSJ2", 260);

        public override bool Stackable() => true;

        public override Color TextColor() => Color.Navy;

        public override void SetStaticDefaults()
        {
            if (ConfigServer.Instance.SSJTweaks)
            {
                baseDefenceBonus = 7;
                damageMulti = 1.25f;
                speedMulti = 1.15f;
            }
            else
            {
                baseDefenceBonus = 10;
                damageMulti = 1.55f;
                speedMulti = 1.35f;
            }

            base.SetStaticDefaults(); 
        }
    }
}
