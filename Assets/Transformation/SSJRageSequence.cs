using System;
using DBZMODPORT;
using DBZMODPORT.Util;
using DBZGoatLib.Handlers;
using SSBETES.Buffs.Transformations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace SSBETES.Assets.Transformation
{
    public class SSJRageSequence : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 300;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.damage = 0;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Lighting.AddLight(player.Center, 1f, 0.3f, 0f);
            for (int i = 0; i < 30; i++)
            {
                float xPos = (Vector2.UnitX * 5f + Vector2.UnitX * ((float)Main.rand.Next(-10, 10) * 3f)).X;
                float yPos = (Vector2.UnitY * (float)player.height - Vector2.UnitY * (float)Main.rand.Next(0, player.height)).Y - 0.5f;
                Vector2 origin = player.Center;

                Dust tDust = Dust.NewDustDirect(player.position + new Vector2(xPos, yPos), 1, 1, 202, 0f, -2f, 0, new Color(0, 209, 255), 0.7f * (float)Main.rand.Next(1, 2));
                if (Math.Abs((tDust.position - (player.position + Vector2.UnitX * 7f)).X) < 10f)
                {
                    tDust.scale *= 0.75f;
                }
                Dust dust = tDust;
                dust.velocity.Y = dust.velocity.Y + 1f;
                Vector2 direction = -(tDust.position - (player.position + Vector2.UnitX * 5f - Vector2.UnitY * (float)player.height));
                direction.Normalize();
                tDust.velocity = new Vector2(direction.X * 2f, (float)(-1 * Main.rand.Next(1, 5)));
                tDust.noGravity = true;
            }
            for (int i = 0; i < 40; i++)
            {
                float xPos = (Vector2.UnitX * 5f + Vector2.UnitX * ((float)Main.rand.Next(-10, 10) * 3f)).X;
                float yPos = (Vector2.UnitY * (float)player.height - Vector2.UnitY * (float)Main.rand.Next(0, player.height)).Y - 0.5f;
                Vector2 origin = player.Center;

                Dust tDust = Dust.NewDustDirect(player.position + new Vector2(xPos, yPos), 1, 1, 169, 0f, -2f, 0, new Color(255, 255, 35), 0.7f * (float)Main.rand.Next(1, 2));
                if (Math.Abs((tDust.position - (player.position + Vector2.UnitX * 7f)).X) < 10f)
                {
                    tDust.scale *= 0.75f;
                }
                Dust dust = tDust;
                dust.velocity.Y = dust.velocity.Y + 1f;
                Vector2 direction = -(tDust.position - (player.position + Vector2.UnitX * 5f - Vector2.UnitY * (float)player.height));
                direction.Normalize();
                tDust.velocity = new Vector2(direction.X * 2f, (float)(-1 * Main.rand.Next(1, 5)));
                tDust.noGravity = true;
            }
            Projectile.position.X = player.Center.X;
            Projectile.position.Y = player.Center.Y;
            Projectile.Center = player.Center + new Vector2(0f, -25f);
            Projectile.netUpdate = true;
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Main.myPlayer];
            player.GetModPlayer<BUPPlayer>().TransformationProcess = false;
            TransformationHandler.ClearTransformations(player);
            Main.NewText("Super Saiyan Rage Achieved!", Color.DeepSkyBlue);
            TransformationHandler.Transform(player, TransformationHandler.GetTransformation("SSJRGBuff").Value);
        }
    }
}
