using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework;
using DBZGoatLib;
using DBZMODPORT;

namespace SSBETES.Buffs.Buff
{
	public class XenoRegen : ModBuff
	{
		public override void SetStaticDefaults() 
        {
            Main.debuff[base.Type] = true;
        }

		public override void Update(Player player, ref int buffIndex)
		{
			player.statLifeMax2 = 300;
            player.statLifeMax2 -= 100;
            player.lifeRegen = 15;
            player.GetDamage(DamageClass.Generic) *= 1.12f;
            player.moveSpeed += 0.4f;
            player.statDefense -= 10;
            player.GetModPlayer<MyPlayer>().kiMax2 -= 1000;
            player.GetModPlayer<MyPlayer>().kiChargeRate += 10;

            if (player.statLife < 275)
            {
                player.lifeRegen = 65;
                Vector2 position = Main.LocalPlayer.TopLeft;
                Dust dust;

                dust = Main.dust[Terraria.Dust.NewDust(position, 11, 20, 5, 0f, -3.9534883f, 32, new Color(177, 0, 255), 1f)];
                dust.noGravity = true;
                dust.shader = GameShaders.Armor.GetSecondaryShader(43, Main.LocalPlayer);
                dust.fadeIn = 0.13953489f;

                Dust dust2;
                dust2 = Main.dust[Terraria.Dust.NewDust(position, 11, 20, 5, 0f, -3.9534883f, 32, new Color(255, 0, 177), 1f)];
                dust2.noGravity = true;
                dust2.shader = GameShaders.Armor.GetSecondaryShader(108, Main.LocalPlayer);
                dust.fadeIn = 0.13953489f;

                Dust dust3;
                dust3 = Main.dust[Terraria.Dust.NewDust(position, 11, 20, 5, 0f, -3.9534883f, 32, new Color(255, 0, 177), 1f)];
                dust3.noGravity = true;
                dust3.shader = GameShaders.Armor.GetSecondaryShader(114, Main.LocalPlayer);
                dust3.fadeIn = 0.13953489f;
            }

        }
	}
}
