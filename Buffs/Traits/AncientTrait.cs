using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using DBZMODPORT;
using DBZGoatLib.Model;
using DBZGoatLib;
using DBZGoatLib.Handlers;
using SSBETES.Assets;

namespace SSBETES.Buffs.Traits
{
    public class AncientTrait : Trait
    {
        Color originalEyeColor;
        public override Gradient KiBarGradient()
        {
            Gradient gradient = new Gradient(Color.Black);
            gradient.AddStop(0.75f, Color.Maroon);

            return gradient;
        }

        public override string Name() => "Ancient";

        public override void OnTrait(Player player)
        {
            Color originalEyeColor = player.eyeColor;
            player.GetModPlayer<BUPPlayer>().isAncient = true;
        }

        public override void OnLoseTrait(Player player)
        {
            player.GetModPlayer<BUPPlayer>().isAncient = false;
            player.ClearBuff(ModContent.BuffType<UnknownAncient>());
            player.ClearBuff(ModContent.BuffType<AncientBuff>());
            player.GetModPlayer<BUPPlayer>().ChangeEyeColor(originalEyeColor);
        }

        public override float Weight() => 0.08f;

        //TraitInfo ancient => new TraitInfo(Name(), Weight(), KiBarGradient(), OnTrait, OnLoseTrait);
    }
}
