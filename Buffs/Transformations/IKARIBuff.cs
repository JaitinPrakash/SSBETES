using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;
using DBZGoatLib.Model;
using DBZGoatLib.Handlers;
using DBZGoatLib;
using SSBETES.Assets;
using SSBETES.Changes;
using System.Runtime.CompilerServices;

namespace SSBETES.Buffs.Transformations
{
    internal class IKARIBuff : Transformation
    {
        public override AuraData AuraData() => new AuraData("SSBETES/Assets/Aura/IKARIAura", 4, BlendState.Additive, new Color(79, 234, 32));

        public override bool CanTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<BUPPlayer>();

            return !player.HasBuff<IKARIBuff>() && modPlayer.IKARIAchieved;
        }

        public override string FormName() => "Wrathful";

        public override string HairTexturePath()
        {
            return string.Empty;
        }

        public override Gradient KiBarGradient() => new Gradient(new Color(50, 205, 50)).AddStop(1f, new Color(173, 255, 47));

        public override void OnTransform(Player player)
        {
            player.GetModPlayer<BUPPlayer>().IKARIActive = true;
        }

        public override void PostTransform(Player player)
        {
            player.GetModPlayer<BUPPlayer>().IKARIActive = false;
        }

        public override bool SaiyanSparks() => false;

        public override SoundData SoundData() => new SoundData("DBZMODPORT/Sounds/Explosion", "DBZMODPORT/Sounds/SSJAura", 260);

        public override bool Stackable() => false;

        public override Color TextColor() => Color.LimeGreen;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            //base.Description.SetDefault("\nThe more you are in this form, the more dangerous you become.");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            BUPPlayer modPlayer = Main.LocalPlayer.GetModPlayer<BUPPlayer>();

            kiDrainRate = 1.25f * modPlayer.GetIkariTimer();
            kiDrainRateWithMastery = 0.3f * modPlayer.GetIkariTimer();
            attackDrainMulti = 1.7f;

            if (ConfigServer.Instance.SSJTweaks)
            {
                baseDefenceBonus = 7;
                damageMulti = 1.10f * modPlayer.GetIkariTimer();
                speedMulti = 1.70f * modPlayer.GetIkariTimer() / 2;
            }
            else
            {
                baseDefenceBonus = 7;
                damageMulti = 1.25f * modPlayer.GetIkariTimer();
                speedMulti = 2.05f * modPlayer.GetIkariTimer() / 2;
            }
            base.Update(player, ref buffIndex);
        }

        public override void ModifyBuffText(ref string buffName, ref string tip, ref int raree)
        {
            tip = ((Transformation)this).BuildTooltip() + $"\nThe more you are in this form, the more dangerous you become.";
        }
    }
}
