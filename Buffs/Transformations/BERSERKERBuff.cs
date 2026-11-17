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
    internal class BERSERKERBuff : Transformation
    {
        public override AuraData AuraData()
        {
            return new AuraData("DBZGoatLib/Assets/BaseAura", 4, BlendState.AlphaBlend, new Color(22, 14, 24, 200));
        }

        public override bool CanTransform(Player player)
        {
            return !player.HasBuff<BERSERKERBuff>() && !player.GetModPlayer<BUPPlayer>().isAncient;
        }
        
        public override string FormName()
        {
            return "Berserk";
        }

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
            player.GetModPlayer<BUPPlayer>().BERSERKERActive = true;
        }

        public override void PostTransform(Player player)
        {
            player.GetModPlayer<BUPPlayer>().BERSERKERActive = false;
        }

        public override bool SaiyanSparks() => false;

        public override SoundData SoundData()
        {
            return default(SoundData);
        }

        public override bool Stackable() => true;

        public override Color TextColor() => new Color(43, 33, 45);
        
        public override void SetStaticDefaults()
        {
            if (ConfigServer.Instance.SSJTweaks)
            {
                baseDefenceBonus = 3;
                damageMulti = 1.1f;
                speedMulti = 1.05f;
            }
            else
            {
                baseDefenceBonus = 6;
                damageMulti = 1.2f;
                speedMulti = 1.15f;
            }

            base.SetStaticDefaults(); 
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //return player.GetModPlayer<GPlayer>().GetMastery(ModContent.BuffType<Buffs.Transformations.BERSERKERBuff>()) = 0.001f;
            /*
            if (player.whoAmI != Main.myPlayer && player.miscCounter % 10 == 0 && Main.LocalPlayer.team == player.team && player.team != 0)
            {
                TransformationHandler.Transform(player, TransformationHandler.GetTransformation("BERSERKERBuff").Value);
            }
            */
            base.Update(player, ref buffIndex);
            base.SetStaticDefaults();
        }
    }
}
