using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;
using DBZGoatLib.Model;
using DBZGoatLib.Handlers;
using DBZGoatLib;
using SSBETES.Assets;
using SSBETES.Changes;
using SSBETES.Buffs.Debuffs;
using DBZMODPORT;
using DBZMODPORT.Buffs.SSJBuffs;
using System;
using Terraria.ID;

namespace SSBETES.Buffs.Transformations
{
    internal class LimitBreakerBuff : Transformation
    {
        private static float Mastery;
        public int timer;
        public bool timerDone;

        public override AuraData AuraData()
        {
            return new AuraData("DBZGoatLib/Assets/BaseAura", 4, BlendState.AlphaBlend, new Color(87, 249, 43, 200));
        }

        public override bool CanTransform(Player player)
        {
            return !player.HasBuff<LimitBreakerBuff>() && !player.HasBuff<LimitBreakCooldown>() /*&& player.GetModPlayer<BUPPlayer>().LimitBreakAchieved*/ && player.GetModPlayer<GPlayer>().Trait == "Legendary";
        }
        
        public override string FormName()
        {
            return "Limit Breaker";
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
            timerDone = false;
            player.GetModPlayer<BUPPlayer>().LimitBreakActive = true;
            for (int i = 0; i < 250; i++)
            {
                Dust.NewDust(player.Center + new Vector2((float)Random.Shared.NextDouble() * 6f - 3f, (float)Random.Shared.NextDouble() * 8f - 4f), 1, 1, 278, player.velocity.X + (float)(Random.Shared.NextDouble() * 2.0 - 1.0), player.velocity.Y + (float)(Random.Shared.NextDouble() * 4.0 - 3.0), 0, Color.LawnGreen, 1f);
            }
            SoundHandler.PlayVanillaSound(SoundID.Roar, player, 1f, 0f);
            LimitBreakerBuff.Mastery = player.GetModPlayer<GPlayer>().GetMastery(Type);
        }

        public override void PostTransform(Player player)
        {
            player.AddBuff(ModContent.BuffType<LimitBreakCooldown>(), 3000, false, false);
            player.GetModPlayer<BUPPlayer>().LimitBreakActive = false;
            timer = 0;
        }

        public override bool SaiyanSparks() => false;

        public override SoundData SoundData()
        {
            return default(SoundData);
        }

        public override bool Stackable() => true;

        public override Color TextColor() => new Color(124, 252, 0);
        
        public override void SetStaticDefaults()
        {
            kiDrainRate = 0f;
            kiDrainRateWithMastery = 0f;

            base.SetStaticDefaults(); 
        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlayer myPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();
            if (ConfigServer.Instance.SSJTweaks)
            {
                baseDefenceBonus = 6;
                damageMulti = 0.00002f * myPlayer.GetKi() + 1.1f;
                speedMulti = 1.05f;
            }
            else
            {
                baseDefenceBonus = 15;
                damageMulti = 0.00002f * myPlayer.GetKi() + 1.25f;
                speedMulti = 1.2f;
            }

            if (!timerDone)
            {
                timer++;
            }
            if (timer >= 600+((int)(900*Mastery)))
            {
                timerDone = true;
                timer = 0;
                int index = player.FindBuffIndex(ModContent.BuffType<LimitBreakerBuff>());
                player.DelBuff(index);
                player.AddBuff(ModContent.BuffType<LimitBreakCooldown>(), 3000-(int)(1200*Mastery), true, false);
            }
            base.Update(player, ref buffIndex);

            base.SetStaticDefaults();
        }
    }
}
