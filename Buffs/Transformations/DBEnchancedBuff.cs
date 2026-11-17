using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;
using DBZGoatLib.Model;
using DBZGoatLib.Handlers;
using DBZGoatLib;
using SSBETES.Assets;
using SSBETES.Changes;

namespace SSBETES.Buffs.Transformations
{
    internal class DBEnchancedBuff : Transformation
    {
        public override AuraData AuraData()
        {
            return new AuraData("DBZGoatLib/Assets/BaseAura", 4, BlendState.AlphaBlend, new Color(212, 31, 31));
        }

        public override bool CanTransform(Player player)
        {
            return !player.HasBuff<DBEnchancedBuff>() && player.GetModPlayer<BUPPlayer>().DragonBallEquipped;
        }

        public override string FormName() => "Dragon Ball Еnchantment";

        public override string HairTexturePath()
        {
            return string.Empty;
        }

        public override Gradient KiBarGradient()
        {
            return null;
        }

        public override void OnTransform(Player player)
        {
            player.GetModPlayer<BUPPlayer>().DBEnchancedActive = true;
        }

        public override void PostTransform(Player player)
        {
            player.GetModPlayer<BUPPlayer>().DBEnchancedActive = false;
        }

        public override bool SaiyanSparks() => false;

        public override SoundData SoundData()
        {
            return default(SoundData);
        }

        public override bool Stackable() => true;

        public override Color TextColor() => Color.DarkRed;

        public override void SetStaticDefaults()
        {
            kiDrainRate = 0f;
            kiDrainRateWithMastery = 0f;
            attackDrainMulti = 0f;

            if (ConfigServer.Instance.SSJTweaks)
            {
                baseDefenceBonus = 5;
                damageMulti = 1.20f;
                speedMulti = 1.10f;
            }
            else
            {
                baseDefenceBonus = 7;
                damageMulti = 1.30f;
                speedMulti = 1.10f;
            }

            base.SetStaticDefaults(); 
        }
        public override void Update(Player player, ref int buffIndex)
        {
            if (Config.Instance.DBAura)
            {
                int index = player.FindBuffIndex(ModContent.BuffType<DBEnchancedBuff>());
                player.DelBuff(index);
            }
            base.Update(player, ref buffIndex);
            base.SetStaticDefaults();
        }
    }
}
