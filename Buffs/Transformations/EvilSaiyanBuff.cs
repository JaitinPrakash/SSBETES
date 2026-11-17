using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;
using DBZGoatLib.Model;
using DBZGoatLib.Handlers;
using DBZGoatLib;
using SSBETES.Assets;
using SSBETES.Changes;
using DBZMODPORT;

namespace SSBETES.Buffs.Transformations
{
    internal class EvilSaiyanBuff : Transformation
    {
        public override AuraData AuraData() => new AuraData("SSBETES/Assets/Aura/EvilAura", 4, BlendState.AlphaBlend, new Color(122, 0, 7));

        public override bool CanTransform(Player player)
        {
            var modPlayer = player.GetModPlayer<BUPPlayer>();

            return !player.HasBuff<EvilSaiyanBuff>() && modPlayer.EvilSaiyanAchieved;
        }

        public override string FormName() => "Evil Saiyan";

        public override string HairTexturePath() => "SSBETES/Assets/Hair/EvilSaiyanHair";

        public override Gradient KiBarGradient() => new Gradient(new Color(50, 3, 3)).AddStop(1f, new Color(110, 0, 0));

        public override void OnTransform(Player player)
        {
            player.GetModPlayer<BUPPlayer>().EvilSaiyanActive = true;
        }

        public override void PostTransform(Player player)
        {
            player.GetModPlayer<BUPPlayer>().EvilSaiyanActive = false;
        }

        public override bool SaiyanSparks() => false;

        public override SoundData SoundData() => new SoundData("DBZMODPORT/Sounds/SSJAscension", "DBZMODPORT/Sounds/SSJ2", 260);

        public override bool Stackable() => true;

        public override Color TextColor() => Color.DarkRed;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults(); 
        }

        public override void Update(Player player, ref int buffIndex)
        {
            BUPPlayer modPlayer = Main.LocalPlayer.GetModPlayer<BUPPlayer>();

            kiDrainRate = 0.5f;
            kiDrainRateWithMastery = 0f;
            player.GetModPlayer<MyPlayer>().kiMax2 += 1500;

            if (ConfigServer.Instance.SSJTweaks)
            {
                baseDefenceBonus = (int)(2f * (1 + modPlayer.GetEvilAmount()));
                damageMulti = 1.15f + (0.15f * modPlayer.GetEvilAmount());
                speedMulti = 1.05f + (0.05f * modPlayer.GetEvilAmount());
            }
            else
            {
                baseDefenceBonus = (int)(4f * (1 + modPlayer.GetEvilAmount()));
                damageMulti = 1.2f + (0.2f * modPlayer.GetEvilAmount());
                speedMulti = 1.1f + (0.1f * modPlayer.GetEvilAmount());
            }
            /*
            if (player.whoAmI != Main.myPlayer && player.miscCounter % 10 == 0)
            {
                if (Main.LocalPlayer.team == player.team && player.team != 0)
                {
                    TransformationHandler.Transform(player, TransformationHandler.GetTransformation("BERSERKERBuff").Value);
                }
                
            }
            */
            base.Update(player, ref buffIndex);
        }
    }
}
